using System.Text.RegularExpressions;

namespace Code;

using Vec2l = (long x, long y);

public static class Day13
{
    public class MachineInfo
    {
        public Vec2l A { get; set; }
        public Vec2l B { get; set; }
        public Vec2l Target { get; set; }
    }

    public static IList<MachineInfo> LoadMachines(string path)
    {
        var text = File.ReadAllText(path);
        return Regex.Matches(
                text,
                @"Button A: X\+(\d+), Y\+(\d+)\r\nButton B: X\+(\d+), Y\+(\d+)\r\nPrize: X=(\d+), Y=(\d+)")
            .Select(m => new MachineInfo
            {
                A = (long.Parse(m.Groups[1].Value), long.Parse(m.Groups[2].Value)),
                B = (long.Parse(m.Groups[3].Value), long.Parse(m.Groups[4].Value)),
                Target = (long.Parse(m.Groups[5].Value), long.Parse(m.Groups[6].Value)),
            })
            .ToList();
    }

    public static long CalculateRequiredTokens(IEnumerable<MachineInfo> machines)
    {
        return machines.Sum(m =>
        {
            var buttons = CalculateRequiredPresses(m, limitPresses: true);
            return buttons.a * 3 + buttons.b;
        });
    }
    
    public static long CalculateRequiredTokensPartTwo(IList<MachineInfo> machines)
    {
        foreach (var machine in machines)
        {
            machine.Target = (machine.Target.x + 10000000000000, machine.Target.y + 10000000000000);
        }
        return machines
            .Sum(m =>
        {
            var algoButtons = CalculateRequiredPresses(m, limitPresses: false);
            var buttons = algoButtons;// CalculateRequiredPresses2(m);
            return buttons.a * 3 + buttons.b;
        });
    }
    
    private static (long a, long b) CalculateRequiredPresses(MachineInfo m, bool limitPresses)
    {
        Console.WriteLine($"{m.A}, {m.B}, {m.Target}");
        var (x, y) = SolveEquation(m);
        var (ix, iy) = ((long)x, (long)y);
        (long a, long b)[] guesses = 
            [
                (ix - 1, iy + 1),
                (ix - 1, iy),
                (ix - 1, iy - 1),
                (ix, iy + 1),
                (ix, iy),
                (ix, iy - 1),
                (ix + 1, iy + 1),
                (ix + 1, iy),
                (ix + 1, iy - 1),
            ];
        foreach (var guess in guesses)
        {
            Console.WriteLine(
                $"{guess.a} * {m.A.x} + {guess.b} * {m.B.x} = {guess.a * m.A.x + guess.b * m.B.x} ({m.Target.x}), " +
                $"{guess.a} * {m.A.y} + {guess.b} * {m.B.y} = {guess.a * m.A.y + guess.b * m.B.y} ({m.Target.y}) ");
            if (AttemptSolution(guess.a, guess.b, m, limitPresses))
            {
                Console.WriteLine("Found solution!");
                return (guess.a, guess.b);
            }
        }

        return (0, 0);
    }

    private static (decimal x, decimal y) SolveEquation(MachineInfo m)
    {
        if (m.A.x == 0) return (0, 0);
        
        decimal x = m.A.x - m.A.y;
        decimal y = m.B.y - m.B.x;
        decimal c = m.Target.x - m.Target.y;

        if (x == 0) x = 1;
        
        decimal ny = y / x;
        decimal nc = c / x;

        decimal t = m.A.x * ny + m.B.x;
        decimal tc = -m.A.x * nc + m.Target.x;
        
        if (t == 0) return (0, 0);

        decimal answery = tc / t;
        decimal answerx = (m.Target.x - m.B.x * answery) / m.A.x;
        return (answerx, answery);
    }

    private static bool AttemptSolution(long a, long b, MachineInfo m, bool limitPresses)
    {
        if (a == 0 && b <= 0) return false;
        if (b == 0 && a <= 0) return false;
        if (limitPresses && a > 100) return false;
        if (limitPresses && b > 100) return false;
        
        return (a >= 0 && b >= 0)
            && (a * m.A.x + b * m.B.x == m.Target.x)
            && (a * m.A.y + b * m.B.y == m.Target.y);
    }
}

