namespace Code;

using Coordinate = (int x, int y);

public static class Day10
{
    public static int[][] LoadMap(string path)
    {
        var lines = File.ReadAllLines(path);
        var map = new List<int[]>();
        foreach (var line in lines)
        {
            map.Add(line.Select(l => int.Parse(l.ToString())).ToArray());
        }
        return map.ToArray();
    }

    public static int SumTrailheadScores(int[][] map)
    {
        return SumTrailCount(map, onlyUniqueTrails: true);
    }

    public static int SumTrailheadRatings(int[][] map)
    {
        return SumTrailCount(map, onlyUniqueTrails: false);
    }

    private static int SumTrailCount(int[][] map, bool onlyUniqueTrails)
    {
        var total = 0;
        for (int x = 0; x < map.Length; x++)
        {
            for (int y = 0; y < map[0].Length; y++)
            {
                if (map[x][y] == 0)
                {
                    total += CountTrails(map, (x, y), onlyUniqueTrails);
                }
            }
        }
        return total;
    }

    private static int CountTrails(int[][] map, Coordinate trailhead, bool onlyUniqueTrails)
    {
        var peaks = new List<Coordinate>();

        List<Coordinate> visited = [];
        var queue = new Queue<Coordinate>();

        queue.Enqueue(trailhead);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            Coordinate[] neighbors =
            [
                current with { x = current.x - 1 },
                    current with { x = current.x + 1 },
                    current with { y = current.y - 1 },
                    current with { y = current.y + 1 },
                ];

            foreach (var neighbor in neighbors)
            {
                if (IsInBounds(map, neighbor) && IsOneStepUp(map, current, neighbor))
                {
                    if (IsPeak(map, neighbor))
                    {
                        peaks.Add(neighbor);
                    }
                    else
                    {
                        queue.Enqueue(neighbor);
                    }
                }
            }
        }

        if (onlyUniqueTrails)
        {
            return peaks.Distinct().Count();
        }

        return peaks.Count;
    }

    private static bool IsPeak(int[][] map, Coordinate coordinate)
    {
        return map[coordinate.x][coordinate.y] == 9;
    }

    private static bool IsOneStepUp(int[][] map, Coordinate current, Coordinate next)
    {
        return map[next.x][next.y] == map[current.x][current.y] + 1;
    }

    private static bool IsInBounds(int[][] map, Coordinate coordinate)
    {
        return coordinate.x >= 0 && coordinate.x < map.Length && coordinate.y >= 0 && coordinate.y < map[0].Length;
    }
}
