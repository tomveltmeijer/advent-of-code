using Code;

namespace Tests;

[TestClass]
public sealed class Day12Tests
{
    [TestMethod]
    public void CalculateFencePrice_SimpleExample()
    {
        var map = Day12.LoadMap("Data/day12-test1.txt");

        var result = Day12.CalculateFencePrice(map);

        Assert.AreEqual(140, result);
    }

    [TestMethod]
    public void CalculateFencePrice_RegionsInRegionsExample()
    {
        var map = Day12.LoadMap("Data/day12-test2.txt");

        var result = Day12.CalculateFencePrice(map);

        Assert.AreEqual(772, result);
    }

    [TestMethod]
    public void CalculateFencePrice_LargerExample()
    {
        var map = Day12.LoadMap("Data/day12-test3.txt");

        var result = Day12.CalculateFencePrice(map);

        Assert.AreEqual(1930, result);
    }
}
