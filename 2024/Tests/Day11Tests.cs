using Code;

namespace Tests;

[TestClass]
public sealed class Day11Tests
{
    [TestMethod]
    [DataRow(0, 2)]
    [DataRow(1, 3)]
    [DataRow(2, 4)]
    [DataRow(3, 5)]
    [DataRow(4, 9)]
    [DataRow(5, 13)]
    [DataRow(6, 22)]
    [DataRow(25, 55312)]
    public void StoneCount_Example(int numBlinks, long expectedCount)
    {
        long[] stones = [125, 17];

        var result = Day11.StoneCount(stones, numBlinks);

        Assert.AreEqual(expectedCount, result);
    }
}
