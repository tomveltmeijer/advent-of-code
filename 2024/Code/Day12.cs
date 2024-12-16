namespace Code;

using Coordinate = (int x, int y);

public static class Day12
{
    public static char[][] LoadMap(string path)
    {
        return File.ReadAllLines(path)
            .Select(line => line.ToUpper().ToCharArray())
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

    // Calculate price (area * perimeter) of the region that contains the given startCoordinate
    private static int CalculateFencePriceOfRegion(char[][] map, List<Coordinate> visited, Coordinate startCoordinate)
    {
        var queue = new Queue<Coordinate>();

        queue.Enqueue(startCoordinate);
        visited.Add(startCoordinate);

        int area = 0;
        int perimeter = 0;
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            ++area;

            Coordinate[] neighbors =
            [
                current with { x = current.x - 1 },
                current with { x = current.x + 1 },
                current with { y = current.y - 1 },
                current with { y = current.y + 1 },
            ];

            foreach (var neighbor in neighbors)
            {
                if (!IsInBounds(map, neighbor) || !AreInSameRegion(map, current, neighbor))
                {
                    ++perimeter;
                    continue;
                }

                if (!visited.Contains(neighbor))
                {
                    queue.Enqueue(neighbor);
                    visited.Add(neighbor);
                }
            }
        }

        return area * perimeter;
    }

    private static bool IsInBounds(char[][] map, Coordinate coordinate)
    {
        return coordinate.x >= 0 && coordinate.x < map.Length && coordinate.y >= 0 && coordinate.y < map[0].Length;
    }

    private static bool AreInSameRegion(char[][] map, Coordinate a, Coordinate b)
    {
        return map[a.x][a.y] == map[b.x][b.y];
    }
}
