using Code;

namespace Tests;

[TestClass]
public sealed class Day16Tests
{
    [TestMethod]
    [DataRow("Data/day16-test1.txt", 7036)]
    [DataRow("Data/day16-test2.txt", 11048)]
    public void CalculateBoxPositions_Example(string path, int expected)
    {
        var maze = Day16.LoadMaze(path);

        var result = Day16.CalculateLowestScore(maze);

        Assert.AreEqual(expected, result);
    }
    
    [TestMethod]
    [DataRow("Data/day16-test1.txt", 45)]
    [DataRow("Data/day16-test2.txt", 64)]
    public void CalculateNumberOfBestSeats_Example(string path, int expected)
    {
        var maze = Day16.LoadMaze(path);

        var result = Day16.CalculateNumberOfBestSeats(maze);

        Assert.AreEqual(expected, result);
    }
}
