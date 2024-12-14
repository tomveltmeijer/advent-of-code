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
    default:
        return -1;
}

Console.WriteLine($"Result: {result}");
return 0;
