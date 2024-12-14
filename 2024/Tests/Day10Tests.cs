using Code;

namespace Tests;

[TestClass]
public sealed class Day10Tests
{
    [TestMethod]
    public void SumTrailheadScores_Example()
    {
        var map = Day10.LoadMap("Data/day10-test.txt");

        var result = Day10.SumTrailheadScores(map);

        Assert.AreEqual(36, result);
    }

    [TestMethod]
    public void SumTrailheadRatings_Example()
    {
        var map = Day10.LoadMap("Data/day10-test.txt");

        var result = Day10.SumTrailheadRatings(map);

        Assert.AreEqual(81, result);
    }
}
