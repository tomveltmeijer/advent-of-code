namespace Code;

public static class Day01
{
    public static (IEnumerable<int> left, IEnumerable<int> right) LoadLists(string filename)
    {
        var lines = File.ReadAllLines(filename);
        List<int> left = [];
        List<int> right = [];
        foreach (var line in lines)
        {
            var elements = line.Split("   ");
            left.Add(int.Parse(elements[0]));
            right.Add(int.Parse(elements[1]));
        }
        return (left, right);
    }

    public static int CalculateListDistance(IEnumerable<int> left, IEnumerable<int> right)
    {
        var leftOrdered = left.Order();
        var rightOrdered = right.Order();

        return leftOrdered
            .Zip(rightOrdered, (l, r) => Math.Abs(l - r))
            .Sum();
    }

    public static int CalculateListSimilarity(IEnumerable<int> left, IEnumerable<int> right)
    {
        var total = 0;
        foreach (var l in left)
        {
            var o = right.Count(r => r == l);
            total += l * o;
        }

        return total;
    }
}
