using Code;

var puzzle = args.FirstOrDefault()?.ToLower();
Console.WriteLine($"Running puzzle \"{puzzle}\".");

dynamic result;
switch (puzzle)
{
    case "day01-1":
        {
            var (left, right) = Day01.LoadLists("Data/day01.txt");
            result = Day01.CalculateListDistance(left, right);
            break;
        }
    case "day01-2":
        {
            var (left, right) = Day01.LoadLists("Data/day01.txt");
            result = Day01.CalculateListSimilarity(left, right);
            break;
        }
    case "day10-1":
        {
            var map = Day10.LoadMap("Data/day10.txt");
            result = Day10.SumTrailheadScores(map);
            break;
        }
    case "day10-2":
        {
            var map = Day10.LoadMap("Data/day10.txt");
            result = Day10.SumTrailheadRatings(map);
            break;
        }
    case "day11-1":
        {
            var stones = Day11.LoadStones("Data/day11.txt");
            result = Day11.StoneCount(stones, 25);
            break;
        }
    case "day11-2":
        {
            var stones = Day11.LoadStones("Data/day11.txt");
            result = Day11.StoneCount(stones, 75);
            break;
        }
    case "day12-1":
        {
            var map = Day12.LoadMap("Data/day12.txt");
            result = Day12.CalculateFencePrice(map);
            break;
        }
    default:
        return -1;
}

Console.WriteLine($"Result: {result}");
return 0;
