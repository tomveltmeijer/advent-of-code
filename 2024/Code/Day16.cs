namespace Code;

public static class Day16
{
    public record struct Vec2i(int X, int Y)
    {
        public static Vec2i operator +(Vec2i a, Vec2i b) => new(a.X + b.X, a.Y + b.Y);
        public static Vec2i operator -(Vec2i a, Vec2i b) => new(a.X - b.X, a.Y - b.Y);
    }
    
    private static Vec2i[] Directions =
    [
        new(1, 0),
        new(0, 1),
        new(-1, 0),
        new(0, -1),
    ];

    public static char[][] LoadMaze(string path)
    {
        return File.ReadAllLines(path)
            .Select(l => l.ToCharArray())
            .ToArray();
    }

    public static int CalculateLowestScore(char[][] maze)
    {
        var startPos = FindPosition(maze, 'S');
        var costMap = Enumerable.Range(0, maze.Length).Select(_ => new int[maze[0].Length]).ToArray();
        FillCostMap(maze, costMap, startPos, new(1, 0), 0);

        var endPos = FindPosition(maze, 'E');
        return costMap[endPos.Y][endPos.X];
    }

    public static int CalculateNumberOfBestSeats(char[][] maze)
    {
        var startPos = FindPosition(maze, 'S');
        var costMap = Enumerable.Range(0, maze.Length).Select(_ => new int[maze[0].Length]).ToArray();
        FillCostMap(maze, costMap, startPos, new(1, 0), 0);
        
        var endPos = FindPosition(maze, 'E');
        var costBudget = costMap[endPos.Y][endPos.X];
        
        foreach (var direction in Directions)
        {
            MarkBestSeats(maze, costMap, endPos + direction, direction, costBudget - 1);
        }
        Visualize(maze);
        
        return CountBestSeats(maze);
    }

    private static void FillCostMap(char[][] maze, int[][] costMap, Vec2i position, Vec2i direction, int cost)
    {
        if (maze[position.Y][position.X] == '#') return;
        if (costMap[position.Y][position.X] != 0 && cost >= costMap[position.Y][position.X]) return;

        costMap[position.Y][position.X] = cost;

        foreach (var d in Directions)
        {
            var turnCost = GetNum90DegreeTurnsBetween(d, direction) * 1000;
            FillCostMap(maze, costMap, position + d, d, cost + turnCost + 1);
        }
    }

    private static int GetNum90DegreeTurnsBetween(Vec2i a, Vec2i b)
    {
        if (a.X == 0)
        {
            if (b.Y == 0) return 1;
            return a.Y == b.Y ? 0 : 1;
        }
        else
        {
            if (b.X == 0) return 1;
            return a.X == b.X ? 0 : 1;
        }
    }

    private static Vec2i FindPosition(char[][] maze, char type)
    {
        for (int y = 0; y < maze.Length; y++)
        {
            for (int x = 0; x < maze[0].Length; x++)
            {
                if (maze[y][x] == type)
                {
                    return new(x, y);
                }
            }
        }

        return new(-1, -1);
    }

    private static void MarkBestSeats(char[][] maze, int[][] costMap, Vec2i position, Vec2i direction, int costBudget)
    {
        if (maze[position.Y][position.X] == '#') return;
        if (maze[position.Y][position.X] == 'S') return;
        if (costMap[position.Y][position.X] > costBudget) return;
        if (IsMarkedAsBestSeat(maze, position)) return;

        maze[position.Y][position.X] = 'O';
        foreach (var d in Directions)
        {
            var turnCost = GetNum90DegreeTurnsBetween(d, direction) * 1000;
            MarkBestSeats(maze, costMap, position + d, d, costBudget - turnCost - 1);
        }
    }

    private static void Visualize(char[][] maze)
    {
        foreach (var line in maze)
        {
            Console.WriteLine(line);
        }
    }

    private static int CountBestSeats(char[][] maze)
    {
        var count = 0;
        for (int y = 0; y < maze.Length; y++)
        {
            for (int x = 0; x < maze[0].Length; x++)
            {
                if (IsMarkedAsBestSeat(maze, new Vec2i(x, y)))
                {
                    count++;
                }
            }
        }
        return count;
    }

    private static bool IsMarkedAsBestSeat(char[][] maze, Vec2i position)
    {
        return maze[position.Y][position.X] == 'O'
            || maze[position.Y][position.X] == 'S'
            || maze[position.Y][position.X] == 'E';
    }
}