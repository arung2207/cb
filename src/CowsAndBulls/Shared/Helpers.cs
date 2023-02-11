namespace CowsAndBulls.Shared;
public static class Helpers
{
    /// <summary>
    /// Generates a numeric code string not starting with 0 and with non-repeating digits
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string? GetRandomCode(int length = 4)
    {
        string? code = null;

        var digits = new List<string> { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

        for (var i = 0; i < length; i++)
        {
            //generate random number between 0 and count of unused digits
            var randomIndexRandom = new Random().Next(i == 0 ? 1 : 0, digits.Count - 1);
            code += digits[randomIndexRandom];
            digits.RemoveAt(randomIndexRandom); //remove the digit since it is used
        }

        return code;
    }

    /// <summary>
    /// Compares the code with guessed code and finds the number of cows and bulls
    /// </summary>
    /// <param name="code"></param>
    /// <param name="guess"></param>
    /// <returns></returns>
    public static Attempt GetCowsAndBulls(string? code, string? guess)
    {
        var cows = 0;
        var bulls = 0;

        if (code != null && guess != null && code.Length == guess.Length)
        {
            for (var i = 0; i < code.Length; i++)
            {
                for (var j = 0; j < guess.Length; j++)
                {
                    if (code[i] == guess[j])
                    {
                        if (i == j)
                            bulls++; //increment count of bulls if the position also matches
                        else
                            cows++; //increment count of cows if the position does not match
                    }
                }
            }
        }

        return new Attempt { Guess = guess, Bulls = bulls, Cows = cows };
    }

    /// <summary>
    /// Generates all possible codes i.e. 4 digit numbers having non-repeating digits
    /// </summary>
    /// <returns></returns>
    public static List<string> GetAllCodes()
    {
        var codes = new List<string>();
        for (var i = 1; i <= 9; i++)
        {
            for (var j = 0; j <= 9; j++)
            {
                if (j == i) continue;
                for (var k = 0; k <= 9; k++)
                {
                    if (k == j || k == i) continue;
                    for (var l = 0; l <= 9; l++)
                    {
                        if (l == k || l == j || l == i) continue;
                        codes.Add($"{i}{j}{k}{l}");
                    }
                }
            }
        }
        return codes;
    }

    /// <summary>
    /// Selects a string randomly from the source list of strings
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static string? SelectRandomCode(List<string> source)
    {
        string? code = null;
        if (source != null && source.Any())
            code = source.Count == 1 ? source[0] : source[new Random().Next(0, source.Count - 1)];

        return code;
    }

    /// <summary>
    /// Creates list of possible codes from the source list of codes, 
    /// which can have the same number of cows of bulls as in the passed attempt.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="attempt"></param>
    /// <returns></returns>
    public static List<string> GetPossibleCodes(List<string> source, Attempt attempt)
    {
        var possibleCodes = new List<string>();

        foreach (var code in source)
        {
            var possibleAttempt = GetCowsAndBulls(code, attempt.Guess);
            if (possibleAttempt.Cows == attempt.Cows && possibleAttempt.Bulls == attempt.Bulls)
                possibleCodes.Add(code);
        }

        return possibleCodes;
    }

    /// <summary>
    /// Verifies if the passed string has non-repeating characters
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool HasUniqueCharacters(string? str)
    {
        if (str != null && str.Length > 1)
        {
            for (int i = 0; i < str.Length; i++)
                for (int j = i + 1; j < str.Length; j++)
                    if (str[i] == str[j])
                        return false;
        }
        return true;
    }
}
