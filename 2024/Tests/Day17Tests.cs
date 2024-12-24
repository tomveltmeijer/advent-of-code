using Code;

namespace Tests;

[TestClass]
public sealed class Day17Tests
{
    [TestMethod]
    [DataRow("Data/day17-test1.txt", "4,6,3,5,6,3,5,2,1,0")]
    public void CalculateBoxPositions_Example(string path, string expected)
    {
        var computer = Day17.LoadComputer(path);

        var result = Day17.RunProgram(computer);

        Assert.AreEqual(expected, result);
    }
    
    [TestMethod]
    [DataRow("Data/day17-test2.txt", 117440)]
    public void CalculateBoxPositions_Example(string path, int expected)
    {
        var computer = Day17.LoadComputer(path);

        var result = Day17.FindCorruptedRegisterA(computer);

        Assert.AreEqual(expected, result);
    }
}