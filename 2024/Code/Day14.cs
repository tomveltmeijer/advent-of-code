using System.Text;
using System.Text.RegularExpressions;

namespace Code;

using Vec2i = (int x, int y);

public static class Day14
{
    public class Robot
    {
        public Vec2i Position { get; set; }
        public Vec2i Velocity { get; set; }
    }

    public static IList<Robot> LoadRobots(string path)
    {
        return File.ReadAllLines(path)
            .Select(l => Regex.Match(l, @"p=(\-?\d+),(\-?\d+) v=(\-?\d+),(\-?\d+)"))
            .Select(m => new Robot
            {
                Position = (int.Parse(m.Groups[1].Value), int.Parse(m.Groups[2].Value)),
                Velocity = (int.Parse(m.Groups[3].Value), int.Parse(m.Groups[4].Value))
            })
            .ToList();
    }

    public static long CalculateSafetyFactor(int width, int height, IList<Robot> robots, int numSeconds)
    {
        for (int i = 0; i < numSeconds; i++)
        {
            Simulate(width, height, robots);

            if (IsTreeishArrangement(i))
            {
                Visualize(width, height, robots, i);
            }
        }

        return CalculateSafetyFactor(width, height, robots);
    }

    // Every 101 seconds, I noticed a vague vertical alignment
    private static bool IsTreeishArrangement(int i)
    {
        return i > 97 && (i - 97) % 101 == 0;
    }

    private static void Visualize(int width, int height, IList<Robot> robots, int i)
    {
        Console.WriteLine($"Visualizing after {i + 1} seconds");
        var viz = new int[width, height];
        foreach (var robot in robots)
        {
            viz[robot.Position.x, robot.Position.y]++;
        }

        for (int y = 0; y < height; y++)
        {
            var sb = new StringBuilder();
            for (int x = 0; x < width; x++)
            {
                var v = viz[x, y];
                sb.Append(v > 0 ? v : " ");
            }
            Console.WriteLine(sb.ToString());
        }
    }

    private static void Simulate(int width, int height, IList<Robot> robots)
    {
        foreach (var robot in robots)
        {
            var x = (robot.Position.x + robot.Velocity.x) % width;
            var y = (robot.Position.y + robot.Velocity.y) % height;
            if (x < 0) x += width;
            if (y < 0) y += height;
            robot.Position = (x, y);
        }
    }

    private static long CalculateSafetyFactor(int width, int height, IList<Robot> robots)
    {
        var q1 = 0;
        var q2 = 0;
        var q3 = 0;
        var q4 = 0;
        var halfWidth = width / 2;
        var halfHeight = height / 2;
        foreach (var robot in robots)
        {
            if (robot.Position.x > halfWidth)
            {
                if (robot.Position.y < halfHeight)
                {
                    q1++;
                }
                else if (robot.Position.y > halfHeight)
                {
                    q4++;
                }
            }
            else if (robot.Position.x < halfWidth)
            {
                if (robot.Position.y < halfHeight)
                {
                    q2++;
                }
                else if (robot.Position.y > halfHeight)
                {
                    q3++;
                }
            }
        }

        return q1 * q2 * q3 * q4;
    }
}