namespace Code;

using Coordinate = (int x, int y);
using Direction = (int x, int y);

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
        return CalculateFencePrice(map, hasDiscount: false);
    }

    public static int CalculateFencePriceWithDiscount(char[][] map)
    {
        return CalculateFencePrice(map, hasDiscount: true);
    }

    private static int CalculateFencePrice(char[][] map, bool hasDiscount)
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

                total += CalculateFencePriceOfRegion(map, visited, startCoordinate, hasDiscount);
            }
        }
        return total;
    }

    // Calculate price (area * perimeter) of the region that contains the given startCoordinate
    private static int CalculateFencePriceOfRegion(char[][] map, List<Coordinate> visited, Coordinate startCoordinate, bool hasDiscount)
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
                    if (hasDiscount && IsFenceExtensionOfVisitedFence(map, cell, direction, visited))
                    {
                        continue;
                    }

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
        return map[a.x][a.y] == map[b.x][b.y];
    }
    private static bool IsFenceExtensionOfVisitedFence(char[][] map, Coordinate coordinate, Direction direction, IList<Coordinate> visited)
    {
        if (direction.x == 0)
        {
            var left = GetNeighbor(coordinate, (-1, 0));
            var right = GetNeighbor(coordinate, (1, 0));
            return HasExistingFenceInSameDirection(left) || HasExistingFenceInSameDirection(right);
        }
        else
        {
            var up = GetNeighbor(coordinate, (0, -1));
            var down = GetNeighbor(coordinate, (0, 1));
            return HasExistingFenceInSameDirection(up) || HasExistingFenceInSameDirection(down);
        }

        bool HasExistingFenceInSameDirection(Coordinate neighbor) =>
            IsInBounds(map, neighbor)
            && AreInSameRegion(map, coordinate, neighbor)
            && visited.Contains(neighbor)
            && HasFence(map, neighbor, direction);
    }
}
