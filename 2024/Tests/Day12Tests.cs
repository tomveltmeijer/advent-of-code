using Code;

namespace Tests;

[TestClass]
public sealed class Day12Tests
{
    [TestMethod]
    [DataRow("Data/day12-test1.txt", 140)]
    [DataRow("Data/day12-test2.txt", 772)]
    [DataRow("Data/day12-test3.txt", 1930)]
    public void CalculateFencePrice_Example(string testFile, int expectedPrice)
    {
        var map = Day12.LoadMap(testFile);

        var result = Day12.CalculateFencePrice(map);

        Assert.AreEqual(expectedPrice, result);
    }

    [TestMethod]
    [DataRow("Data/day12-test1.txt", 80)]
    [DataRow("Data/day12-test2.txt", 436)]
    [DataRow("Data/day12-test3.txt", 1206)]
    [DataRow("Data/day12-test4.txt", 236)]
    [DataRow("Data/day12-test5.txt", 368)]
    public void CalculateFencePriceWithDiscount_Example(string testFile, int expectedPrice)
    {
        var map = Day12.LoadMap(testFile);

        var result = Day12.CalculateFencePriceWithDiscount(map);

        Assert.AreEqual(expectedPrice, result);
    }
}
