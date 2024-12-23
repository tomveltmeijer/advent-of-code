using Code;

namespace Tests;

[TestClass]
public sealed class Day15Tests
{
    [TestMethod]
    [DataRow("Data/day15-test1.txt", 2028)]
    [DataRow("Data/day15-test2.txt", 10092)]
    public void CalculateBoxPositions_Example(string path, long expected)
    {
        var warehouse = Day15.LoadWarehouse(path);

        var result = Day15.CalculateBoxPositions(warehouse);

        Assert.AreEqual(expected, result);
    }
    
    [TestMethod]
    [DataRow("Data/day15-test2.txt", 9021)]
    [DataRow("Data/day15-test3.txt", 618)]
    public void CalculateBoxPositionsPartTwo_Example(string path, long expected)
    {
        var warehouse = Day15.LoadBigWarehouse(path);

        var result = Day15.CalculateBoxPositionsPartTwo(warehouse);

        Assert.AreEqual(expected, result);
    }
}
