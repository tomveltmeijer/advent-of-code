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
    case "day12-2":
        {
            var map = Day12.LoadMap("Data/day12.txt");
            result = Day12.CalculateFencePriceWithDiscount(map);
            break;
        }
    case "day13-1":
        {
            var machines = Day13.LoadMachines("Data/day13.txt");
            result = Day13.CalculateRequiredTokens(machines);
            break;
        }
    case "day13-2":
        {
            var machines = Day13.LoadMachines("Data/day13.txt");
            result = Day13.CalculateRequiredTokensPartTwo(machines);
            break;
        }
    case "day14-1":
        {
            var robots = Day14.LoadRobots("Data/day14.txt");
            result = Day14.CalculateSafetyFactor(101, 103, robots, 100);
            break;
        }
    case "day14-2":
        {
            var robots = Day14.LoadRobots("Data/day14.txt");
            Day14.CalculateSafetyFactor(101, 103, robots, 7572);
            result = 7572; // Based on visualization
            break;
        }
    case "day15-1":
        {
            var warehouse = Day15.LoadWarehouse("Data/day15.txt");
            result = Day15.CalculateBoxPositions(warehouse);
            break;
        }
    case "day15-2":
        {
            var warehouse = Day15.LoadBigWarehouse("Data/day15.txt");
            result = Day15.CalculateBoxPositionsPartTwo(warehouse);
            break;
        }
    case "day16-1":
        {
            var maze = Day16.LoadMaze("Data/day16.txt");
            result = Day16.CalculateLowestScore(maze);
            break;
        }
    case "day16-2":
        {
            var maze = Day16.LoadMaze("Data/day16.txt");
            result = Day16.CalculateNumberOfBestSeats(maze);
            break;
        }
    case "day17-1":
        {
            var computer = Day17.LoadComputer("Data/day17.txt");
            result = Day17.RunProgram(computer);
            break;
        }
    default:
        return -1;
}

Console.WriteLine($"Result: {result}");
return 0;
