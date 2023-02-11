using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;

namespace CowsAndBulls.Test;

[TestClass]
public class ComputerGuessesE2ETests : PageTest
{
    private const string BaseUrl = "https://localhost:7017"; //point to the correct base url

    [TestMethod]
    public async Task CanNavigateToHomePage()
    {
        await Page.GotoAsync(BaseUrl);
        await Expect(Page).ToHaveTitleAsync("The Game of Cows and Bulls");
    }

    [TestMethod]
    public async Task ComputerGuessesTests()
    {
        await Page.GotoAsync($"{BaseUrl}/ComputerGuesses");
        await Expect(Page).ToHaveTitleAsync("CowsAndBulls");

        //Get locators on the page
        var locatorButtonStartNewGame = Page.GetByRole(AriaRole.Button, new() { Name = "Start new game" });
        var locatorButtonInput = Page.GetByRole(AriaRole.Button, new() { Name = "Input" });
        var locatorLogMessage = Page.Locator(".log-message");
        var locatorGuessPara = Page.Locator(".guess");

        await locatorButtonStartNewGame.ClickAsync(); //Start new game by clicking on the button

        //Expect the labels on the page at the beginning of the game
        await Expect(locatorLogMessage).ToContainTextAsync("Let us begin...");
        await Expect(locatorGuessPara).ToContainTextAsync("Out of 4536 possibilities");

        //Click Input button twice i.e. input 0 cows and 0 bulls
        await locatorButtonInput.ClickAsync();
        await locatorButtonInput.ClickAsync();

        //Expect the labels on the page for the error case
        await Expect(locatorLogMessage).ToContainTextAsync("Give up! I think you provided incorrect clues, please verify the count of cows and bulls.");
        await Expect(locatorGuessPara).ToContainTextAsync("Out of 0 possibilities");
    }
}