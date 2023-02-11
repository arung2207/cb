using CowsAndBulls.Shared;

namespace CowsAndBulls.Test;

[TestClass]
public class HelperTests
{
    [TestMethod]
    public void TestRandomCode()
    {
        var n = new List<string>();
        for (var i = 0; i < 1000; i++)
            n.Add(Helpers.GetRandomCode());

        Assert.IsTrue(n.Count(s => s[0] != '0' && Helpers.HasUniqueCharacters(s)) == n.Count);
    }

    [TestMethod]
    [DataRow("1023", "4567", 0, 0)]
    [DataRow("1023", "9870", 1, 0)]
    [DataRow("1023", "9810", 2, 0)]
    [DataRow("1023", "4102", 3, 0)]
    [DataRow("1023", "3201", 4, 0)]
    [DataRow("1023", "4527", 0, 1)]
    [DataRow("1023", "3089", 1, 1)]
    [DataRow("1023", "2183", 2, 1)]
    [DataRow("1023", "1302", 3, 1)]
    [DataRow("1023", "8423", 0, 2)]
    [DataRow("1023", "1038", 1, 2)]
    [DataRow("1023", "2013", 2, 2)]
    [DataRow("1023", "1029", 0, 3)]
    [DataRow("1023", "1023", 0, 4)]
    public void TestCowsAndBulls(string code, string guess, int expectedCows, int expectedBulls)
    {
        var attempt = Helpers.GetCowsAndBulls(code, guess);
        Assert.IsTrue(attempt.Cows == expectedCows && attempt.Bulls == expectedBulls);
    }

    [TestMethod]
    public void TestCodeGeneration()
    {
        var allCodes = Helpers.GetAllCodes();
        Assert.IsTrue(allCodes.Count == 9 * 9 * 8 * 7);
        Assert.IsTrue(allCodes.Count(s => s[0] != '0' && Helpers.HasUniqueCharacters(s)) == allCodes.Count);

        allCodes = Helpers.GetPossibleCodes(allCodes, new Attempt { Guess = "6174", Bulls = 4, Cows = 0 });
        var guess = Helpers.SelectRandomCode(allCodes);
        Assert.AreEqual(guess, "6174");
    }
}