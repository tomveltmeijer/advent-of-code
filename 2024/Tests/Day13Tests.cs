using Code;

namespace Tests;

[TestClass]
public sealed class Day13Tests
{
    [TestMethod]
    public void CalculateRequiredTokens_Example()
    {
        var machines = Day13.LoadMachines("Data/day13-test.txt");
        
        var result = Day13.CalculateRequiredTokens(machines);
        
        Assert.AreEqual(480, result);
    }
    
    [TestMethod]
    public void CalculateRequiredTokensPartTwo_Example()
    {
        var machines = Day13.LoadMachines("Data/day13-test.txt");
        
        var result = Day13.CalculateRequiredTokensPartTwo(machines);
        
        Assert.AreEqual(875318608908, result);
    }
}
