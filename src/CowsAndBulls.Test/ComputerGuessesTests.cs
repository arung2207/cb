using Bunit;
using CowsAndBulls.Client.Pages;

namespace CowsAndBulls.Test;

[TestClass]
public class ComputerGuessesTests
{
    private Bunit.TestContext ctx;

    //Component Under Test
    private IRenderedComponent<ComputerGuesses> cut;

    [TestInitialize] // Runs before each test.
    public void Setup()
    {
        ctx = new Bunit.TestContext();
        cut = ctx.RenderComponent<ComputerGuesses>();
    }

    [TestCleanup] // Runs after each test.
    public void Cleanup()
    {
        cut.Dispose();
        ctx.Dispose();
    }

    [TestMethod]
    public void ComponentRendersCorrectly()
    {
        //Verify the presence of heading caption
        Assert.IsTrue(cut.Markup.Contains("<h3>Cows and Bulls - You think and computer guesses</h3>"));
    }

    [TestMethod]
    public void GameBeginsCorrectly()
    {
        var buttonStartNewGame = cut.Find("button.btn-start-new-game");
        buttonStartNewGame.Click();

        //Verify the presence of labels at start of game
        Assert.IsTrue(cut.Markup.Contains("Let us begin..."));
        Assert.IsTrue(cut.Markup.Contains("Out of 4536 possibilities"));
    }

    [TestMethod]
    public void GamePlayedCorrectly()
    {
        //Input button clicked twice with 0 Cows and 0 Bulls
        var buttonInput = cut.Find("button.btn-input");
        buttonInput.Click();
        buttonInput.Click();

        //Verify the presence of labels in case of error
        Assert.IsTrue(cut.Markup.Contains("Give up! I think you provided incorrect clues, please verify the count of cows and bulls."));
        Assert.IsTrue(cut.Markup.Contains("Out of 0 possibilities"));
    }
}
