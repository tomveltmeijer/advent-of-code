using Code;

namespace Tests;

[TestClass]
public sealed class Day01Tests
{
    [TestMethod]
    public void CalculateListDistance_Example()
    {
        var (left, right) = Day01.LoadLists("Data/day01-test.txt");

        var result = Day01.CalculateListDistance(left, right);

        Assert.AreEqual(11, result);
    }

    [TestMethod]
    public void CalculateListSimilarity_Example()
    {
        var (left, right) = Day01.LoadLists("Data/day01-test.txt");

        var result = Day01.CalculateListSimilarity(left, right);

        Assert.AreEqual(31, result);
    }
}
