using System.Collections.Concurrent;

namespace Code;

public static class Day11
{
    public static long[] LoadStones(string path)
    {
        return File.ReadAllText(path).Split(" ").Select(long.Parse).ToArray();
    }

    public static long StoneCount(IEnumerable<long> stones, int numBlinks)
    {
        return stones.Sum(s => StoneCount(s, numBlinks));
    }

    private static readonly ConcurrentDictionary<(long, int), long> StoneCountCache = [];

    private static long StoneCount(long stone, int numBlinks)
    {
        if (numBlinks == 0)
        {
            return 1;
        }

        if (StoneCountCache.ContainsKey((stone, numBlinks)))
        {
            return StoneCountCache[(stone, numBlinks)];
        }

        long count;
        if (stone == 0)
        {
            count = StoneCount(1, numBlinks - 1);
        }
        else
        {
            var chars = stone.ToString();
            var numChars = chars.Length;

            if (numChars % 2 == 0)
            {
                count = StoneCount(long.Parse(chars[..(numChars / 2)]), numBlinks - 1)
                    + StoneCount(long.Parse(chars[(numChars / 2)..]), numBlinks - 1);
            }
            else
            {
                count = StoneCount(stone * 2024, numBlinks - 1);
            }
        }

        StoneCountCache[(stone, numBlinks)] = count;
        return count;
    }
}
