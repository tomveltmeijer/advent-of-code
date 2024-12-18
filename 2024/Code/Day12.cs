namespace Code;

using Coordinate = (int x, int y);
using Direction = (int x, int y);
using CellFenceStatus = (bool top, bool right, bool bottom, bool left);

public static class Day12
{
    public static char[][] LoadMap(string path)
    {
        return File.ReadAllLines(path)
            .Select(line => line.ToCharArray())
            .ToArray();
    }

    public static int CalculateFencePrice(char[][] map)
    {
        List<Coordinate> visited = [];
        int total = 0;
        for (int x = 0; x < map.Length; x++)
        {
            for (int y = 0; y < map[0].Length; y++)
            {
                var startCoordinate = (x, y);
                if (visited.Contains(startCoordinate))
                {
                    continue;
                }

                total += CalculateFencePriceOfRegion(map, visited, startCoordinate);
            }
        }
        return total;
    }

    public static int CalculateFencePriceWithDiscount(char[][] map)
    {
        List<Coordinate> visited = [];
        var fenceStatuses = new CellFenceStatus[map[0].Length, map.Length];
        int total = 0;
        for (int y = 0; y < map.Length; y++)
        {
            for (int x = 0; x < map[0].Length; x++)
            {
                var startCoordinate = (x, y);
                if (visited.Contains(startCoordinate))
                {
                    continue;
                }

                total += CalculateFencePriceOfRegionWithDiscount(map, fenceStatuses, visited, startCoordinate);
            }
        }
        return total;
    }

    // Calculate price (area * perimeter) of the region that contains the given startCoordinate
    private static int CalculateFencePriceOfRegionWithDiscount(char[][] map, CellFenceStatus[,] fenceStatuses, List<Coordinate> visited, Coordinate startCoordinate)
    {
        var queue = new Queue<Coordinate>();

        queue.Enqueue(startCoordinate);

        int area = 0;
        int minX = startCoordinate.x;
        int minY = startCoordinate.y;
        int maxX = startCoordinate.x;
        int maxY = startCoordinate.y;
        while (queue.Count > 0)
        {
            var cell = queue.Dequeue();

            if (visited.Contains(cell))
            {
                continue;
            }
            visited.Add(cell);

            ++area;
            minX = Math.Min(minX, cell.x);
            maxX = Math.Max(maxX, cell.x);
            minY = Math.Min(minY, cell.y);
            maxY = Math.Max(maxY, cell.y);

            Direction[] directions =
            [
                (-1,  0),
                ( 1,  0),
                ( 0, -1),
                ( 0,  1),
            ];

            foreach (var direction in directions)
            {
                if (HasFence(map, cell, direction))
                {
                    if (direction.x == -1)
                    {
                        fenceStatuses[cell.x, cell.y].left = true;
                    }
                    else if (direction.x == 1)
                    {
                        fenceStatuses[cell.x, cell.y].right = true;
                    }
                    else if (direction.y == -1)
                    {
                        fenceStatuses[cell.x, cell.y].top = true;
                    }
                    else if (direction.y == 1)
                    {
                        fenceStatuses[cell.x, cell.y].bottom = true;
                    }
                }
                else
                {
                    var neighbor = GetNeighbor(cell, direction);
                    queue.Enqueue(neighbor);
                }
            }
        }

        int numSides = 0;
        for (int x = minX; x <= maxX; x++)
        {
            bool prevLeft = false;
            bool prevRight = false;
            for (int y = minY; y <= maxY; y++)
            {
                var f = fenceStatuses[x, y];
                if (f.left && !prevLeft)
                {
                    ++numSides;
                }
                if (f.right && !prevRight)
                {
                    ++numSides;
                }
                prevLeft = f.left;
                prevRight = f.right;
            }
        }

        for (int y = minY; y <= maxY; y++)
        {
            bool prevTop = false;
            bool prevBottom = false;
            for (int x = minX; x <= maxX; x++)
            {
                var f = fenceStatuses[x, y];
                if (f.top && !prevTop)
                {
                    ++numSides;
                }
                if (f.bottom && !prevBottom)
                {
                    ++numSides;
                }
                prevTop = f.top;
                prevBottom = f.bottom;
            }
        }

        for (int y = minY; y <= maxY; y++)
        {
            var debug = "";
            for (int x = minX; x <= maxX; x++)
            {
                var s = fenceStatuses[x, y];
                debug += "[";
                debug += s.top ? "T" : " ";
                debug += s.right ? "R" : " ";
                debug += s.bottom ? "B" : " ";
                debug += s.left ? "L" : " ";
                debug += "]";

                fenceStatuses[x, y].top = false;
                fenceStatuses[x, y].right = false;
                fenceStatuses[x, y].bottom = false;
                fenceStatuses[x, y].left = false;
            }
            Console.WriteLine(debug);
        }
        Console.WriteLine("");

        return area * numSides;
    }

    // Calculate price (area * perimeter) of the region that contains the given startCoordinate
    private static int CalculateFencePriceOfRegion(char[][] map, List<Coordinate> visited, Coordinate startCoordinate)
    {
        var queue = new Queue<Coordinate>();

        queue.Enqueue(startCoordinate);

        int area = 0;
        int fenceCount = 0;
        while (queue.Count > 0)
        {
            var cell = queue.Dequeue();

            if (visited.Contains(cell))
            {
                continue;
            }
            visited.Add(cell);

            ++area;

            Direction[] directions =
            [
                (-1,  0),
                ( 1,  0),
                ( 0, -1),
                ( 0,  1),
            ];

            foreach (var direction in directions)
            {
                if (HasFence(map, cell, direction))
                {
                    ++fenceCount;
                }
                else
                {
                    var neighbor = GetNeighbor(cell, direction);
                    queue.Enqueue(neighbor);
                }
            }
        }

        return area * fenceCount;
    }

    private static bool HasFence(char[][] map, Coordinate coordinate, Direction direction)
    {
        var neighbor = GetNeighbor(coordinate, direction);
        return !IsInBounds(map, neighbor) || !AreInSameRegion(map, coordinate, neighbor);
    }

    private static Coordinate GetNeighbor(Coordinate coordinate, Direction direction)
    {
        return coordinate with { x = coordinate.x + direction.x, y = coordinate.y + direction.y };
    }

    private static bool IsInBounds(char[][] map, Coordinate coordinate)
    {
        return coordinate.x >= 0 && coordinate.x < map.Length && coordinate.y >= 0 && coordinate.y < map[0].Length;
    }

    private static bool AreInSameRegion(char[][] map, Coordinate a, Coordinate b)
    {
        return map[a.y][a.x] == map[b.y][b.x];
    }
}
