using System.Text;

namespace Code;

public static class Day15
{
    public record struct Vec2i(int X, int Y)
    {
        public static Vec2i operator +(Vec2i a, Vec2i b) => new(a.X + b.X, a.Y + b.Y);
        public static Vec2i operator -(Vec2i a, Vec2i b) => new(a.X - b.X, a.Y - b.Y);
    }

    public class WarehouseInfo
    {
        public char[][] Map { get; set; }
        public Vec2i RobotPosition { get; set; }
        public IList<Vec2i> RobotMoves { get; set; }
    }

    public static WarehouseInfo LoadWarehouse(string path)
    {
        var lines = File.ReadAllLines(path);
        var map = new List<char[]>();
        var robotPosition = new Vec2i(-1, -1);

        var i = 0;
        for (; lines[i] != string.Empty; i++)
        {
            var line = lines[i];
            map.Add(line.ToCharArray());
            for (int x = 0; x < line.Length; x++)
            {
                if (line[x] == '@')
                {
                    robotPosition = new Vec2i(x, i);
                }
            }
        }

        i++;
        var moves = new List<Vec2i>();
        for (; i < lines.Length; i++)
        {
            var line = lines[i];
            for (int x = 0; x < line.Length; x++)
            {
                if (line[x] == '^') moves.Add(new Vec2i(0, -1));
                if (line[x] == '>') moves.Add(new Vec2i(1, 0));
                if (line[x] == 'v') moves.Add(new Vec2i(0, 1));
                if (line[x] == '<') moves.Add(new Vec2i(-1, 0));
            }
        }

        return new WarehouseInfo
        {
            Map = map.ToArray(),
            RobotPosition = robotPosition,
            RobotMoves = moves.ToArray()
        };
    }

    public static WarehouseInfo LoadBigWarehouse(string path)
    {
        var lines = File.ReadAllLines(path);
        var map = new List<char[]>();
        var robotPosition = new Vec2i(-1, -1);

        var i = 0;
        for (; lines[i] != string.Empty; i++)
        {
            var line = lines[i];
            var sb = new StringBuilder();
            for (int x = 0; x < line.Length; x++)
            {
                if (line[x] == '#')
                {
                    sb.Append("##");
                }
                else if (line[x] == 'O')
                {
                    sb.Append("[]");
                }
                else if (line[x] == '.')
                {
                    sb.Append("..");
                }
                else if (line[x] == '@')
                {
                    sb.Append("@.");
                    robotPosition = new Vec2i(x * 2, i);
                }
            }

            map.Add(sb.ToString().ToCharArray());
        }

        i++;
        var moves = new List<Vec2i>();
        for (; i < lines.Length; i++)
        {
            var line = lines[i];
            for (int x = 0; x < line.Length; x++)
            {
                if (line[x] == '^') moves.Add(new Vec2i(0, -1));
                if (line[x] == '>') moves.Add(new Vec2i(1, 0));
                if (line[x] == 'v') moves.Add(new Vec2i(0, 1));
                if (line[x] == '<') moves.Add(new Vec2i(-1, 0));
            }
        }

        return new WarehouseInfo
        {
            Map = map.ToArray(),
            RobotPosition = robotPosition,
            RobotMoves = moves.ToArray()
        };
    }

    public static long CalculateBoxPositions(WarehouseInfo warehouse)
    {
        foreach (var move in warehouse.RobotMoves)
        {
            RunSimulation(warehouse, move);
        }

        Visualize(warehouse);
        return CalculateScore(warehouse.Map);
    }

    public static long CalculateBoxPositionsPartTwo(WarehouseInfo warehouse)
    {
        foreach (var move in warehouse.RobotMoves)
        {
            RunSimulationPartTwo(warehouse, move);
        }

        Visualize(warehouse);
        return CalculateScore(warehouse.Map);
    }

    private static void Visualize(WarehouseInfo warehouse)
    {
        foreach (var line in warehouse.Map)
        {
            Console.WriteLine(line);
        }

        Console.WriteLine("");
    }

    private static void RunSimulation(WarehouseInfo warehouse, Vec2i move)
    {
        var tileCheck = warehouse.RobotPosition + move;
        bool isBlocked;
        while (true)
        {
            if (IsFree(warehouse.Map, tileCheck))
            {
                isBlocked = false;
                break;
            }
            else if (IsWall(warehouse.Map, tileCheck))
            {
                isBlocked = true;
                break;
            }

            tileCheck += move;
        }

        if (isBlocked) return;

        var current = tileCheck;
        var prev = current - move;
        while (current != warehouse.RobotPosition)
        {
            (warehouse.Map[current.Y][current.X], warehouse.Map[prev.Y][prev.X]) =
                (warehouse.Map[prev.Y][prev.X], warehouse.Map[current.Y][current.X]);

            current -= move;
            prev -= move;
        }

        warehouse.RobotPosition += move;
    }

    private static void RunSimulationPartTwo(WarehouseInfo warehouse, Vec2i move)
    {
        var nextPos = warehouse.RobotPosition + move;

        if (IsWall(warehouse.Map, nextPos))
        {
            return;
        }

        if (IsFree(warehouse.Map, nextPos))
        {
            MoveRobot(warehouse, move);
            return;
        }

        List<Vec2i> boxesToMove = [];
        if (CanMoveBox(warehouse.Map, nextPos, move, ref boxesToMove))
        {
            MoveBoxes(warehouse.Map, boxesToMove, move);
            MoveRobot(warehouse, move);
        }
    }

    private static bool CanMoveBox(char[][] map, Vec2i position, Vec2i move, ref List<Vec2i> boxesToMove)
    {
        if (IsFree(map, position)) return true;
        if (IsWall(map, position)) return false;

        if (map[position.Y][position.X] == ']')
        {
            return CanMoveBox(map, position + new Vec2i(-1, 0), move, ref boxesToMove);
        }

        if (map[position.Y][position.X] == '[')
        {
            boxesToMove.Add(position);
            if (move.X == -1)
            {
                return CanMoveBox(map, position + move, move, ref boxesToMove);
            }
            else if (move.X == 1)
            {
                return CanMoveBox(map, position + new Vec2i(1, 0) + move, move, ref boxesToMove);
            }
            else
            {
                return CanMoveBox(map, position + move, move, ref boxesToMove)
                    && CanMoveBox(map, position + new Vec2i(1, 0) + move, move, ref boxesToMove);
            }
        }

        return false;
    }

    private static void MoveBoxes(char[][] map, IList<Vec2i> boxesToMove, Vec2i move)
    {
        foreach (var box in boxesToMove)
        {
            SetTile(map, box + move, '[');
            SetTile(map, box + move + new Vec2i(1, 0), ']');
        }

        var oldOccupiedTiles = boxesToMove
            .SelectMany(b => new[] { b, b + new Vec2i(1, 0) })
            .Distinct()
            .ToArray();
        var newOccupiedTiles = oldOccupiedTiles
            .Select(t => t + move)
            .Distinct();
        var newEmtpyTiles = oldOccupiedTiles
            .Where(t => !newOccupiedTiles.Contains(t))
            .Distinct();

        foreach (var newEmptyTile in newEmtpyTiles)
        {
            SetTile(map, newEmptyTile, '.');
        }
    }

    private static void MoveRobot(WarehouseInfo warehouse, Vec2i move)
    {
        SetTile(warehouse.Map, warehouse.RobotPosition, '.');
        warehouse.RobotPosition += move;
        SetTile(warehouse.Map, warehouse.RobotPosition, '@');
    }

    private static void SetTile(char[][] map, Vec2i position, char value)
    {
        map[position.Y][position.X] = value;
    }

    private static bool IsFree(char[][] map, Vec2i position)
    {
        return IsInBounds(map, position) && map[position.Y][position.X] == '.';
    }

    private static bool IsWall(char[][] map, Vec2i position)
    {
        return !IsInBounds(map, position) || map[position.Y][position.X] == '#';
    }

    private static bool IsInBounds(char[][] map, Vec2i position)
    {
        return position.X >= 0 && position.X < map[0].Length
            && position.Y >= 0 && position.Y < map.Length;
    }

    private static long CalculateScore(char[][] map)
    {
        var score = 0L;
        for (int y = 0; y < map.Length; y++)
        {
            for (int x = 0; x < map[0].Length; x++)
            {
                if (map[y][x] == 'O' || map[y][x] == '[')
                {
                    score += y * 100 + x;
                }
            }
        }

        return score;
    }
}