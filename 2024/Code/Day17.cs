namespace Code;

public static class Day17
{
    public class Computer
    {
        public long RegisterA { get; set; }
        public long RegisterB { get; set; }
        public long RegisterC { get; set; }
        public IList<int> Program { get; init; }
    }

    public static Computer LoadComputer(string path)
    {
        var lines = File.ReadAllLines(path);

        var a = lines[0].Substring("Register A: ".Length);
        var b = lines[1].Substring("Register B: ".Length);
        var c = lines[2].Substring("Register C: ".Length);
        var program = lines[4]
            .Substring("Program: ".Length)
            .Split(',')
            .Select(int.Parse)
            .ToList();

        return new Computer
        {
            RegisterA = long.Parse(a),
            RegisterB = long.Parse(b),
            RegisterC = long.Parse(c),
            Program = program
        };
    }

    public static List<int> RunProgram(Computer computer)
    {
        var ptr = 0;
        List<int> output = [];
        while (ptr < computer.Program.Count)
        {
            var opcode = computer.Program[ptr];
            var operand = computer.Program[ptr + 1];
            if (opcode == 0) // adv
            {
                var n = computer.RegisterA;
                var d = GetComboOperand(computer, operand);
                computer.RegisterA = (long)(n / Math.Pow(2, d));
            }
            else if (opcode == 1) // bxl
            {
                computer.RegisterB ^= operand;
            }
            else if (opcode == 2) // bst
            {
                computer.RegisterB = GetComboOperand(computer, operand) % 8;
            }
            else if (opcode == 3) // jnz
            {
                if (computer.RegisterA != 0)
                {
                    ptr = operand;
                    continue;
                }
            }
            else if (opcode == 4) // bxc
            {
                computer.RegisterB ^= computer.RegisterC;
            }
            else if (opcode == 5) // out
            {
                var number = GetComboOperand(computer, operand) % 8;
                output.Add((int)number);
            }
            else if (opcode == 6) // bdv
            {
                var n = computer.RegisterA;
                var d = GetComboOperand(computer, operand);
                computer.RegisterB = (long)(n / Math.Pow(2, d));
            }
            else if (opcode == 7) // cdv
            {
                var n = computer.RegisterA;
                var d = GetComboOperand(computer, operand);
                computer.RegisterC = (long)(n / Math.Pow(2, d));
            }
            ptr += 2;
        }

        return output;
    }

    public static long FindCorruptedRegisterA(Computer computer)
    {
        for (long i = 0; ; i++)
        {
            computer.RegisterA = i;
            computer.RegisterB = 0;
            computer.RegisterC = 0;
            var output = RunProgram(computer);
            var program = computer.Program;

            if (output.SequenceEqual(program))
            {
                return i;
            }
        }
    }

    private static long GetComboOperand(Computer computer, long operand) =>
        operand switch
        {
            4 => computer.RegisterA,
            5 => computer.RegisterB,
            6 => computer.RegisterC,
            _ => operand
        };
}