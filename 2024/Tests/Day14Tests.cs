using Code;

namespace Tests;

[TestClass]
public sealed class Day14Tests
{
    [TestMethod]
    public void CalculateSafetyFactor_Example()
    {
        var robots = Day14.LoadRobots("Data/day14-test.txt");

        var result = Day14.CalculateSafetyFactor(11, 7, robots, 100);

        Assert.AreEqual(12, result);
    }
}
