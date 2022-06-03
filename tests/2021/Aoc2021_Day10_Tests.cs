using Xunit;

public class Aoc2021_Day10_Tests
{
    Aoc2021_Day10 aoc2021_Day10 = new Aoc2021_Day10("fake");

    [Theory]
    [InlineData("{([(<{}[<>[]}>{[]{[(<()>", "}")]
    [InlineData("[[<[([]))<([[{}[[()]]]", ")")]
    [InlineData("[{[{({}]{}}([{[{{{}}([]", "]")]
    [InlineData("[<(<(<(<{}))><([]([]()", ")")]
    [InlineData("<{([([[(<>()){}]>(<<{{", ">")]
    [InlineData("[({(<(())[]>[[{[]{<()<>>", "")]
    [InlineData("[}", "}")]
    [InlineData("[[}", "}")]
    [InlineData("[[[]}]", "}")]
    [InlineData("[[[]}][", "}")]
    public void Test1(string input, string expectedOutput)
    {
        string firstUnmatched = aoc2021_Day10.FindFirstIllegal(input);

        Assert.Equal(expectedOutput, firstUnmatched);
    }

    [Theory]
    [InlineData("{([(<{}[<>[]}>{[]{[(<()>", 0)]
    [InlineData("[[<[([]))<([[{}[[()]]]", 0)]
    [InlineData("[({(<(())[]>[[{[]{<()<>>", 1)]
    [InlineData("<{([{{}}[<[[[<>{}]]]>[]]", 1)]
    public void Only_Incomplete_Lines_returned(string input, int lineCount)
    {
        var lines = aoc2021_Day10.GetIncompleteLines(new List<string>() { input });

        Assert.Equal(lineCount, lines.Count);
    }

    [Theory]
    [InlineData("[({([[{{", "}}]])})]")]
    public void Autocomplete_completes_line(string input, string output)
    {
        var autocompleted = aoc2021_Day10.GetAutocompletion(input);

        Assert.Equal(output, autocompleted);
    }

    [Theory]
    [InlineData("}}]])})]", 288957)]
    [InlineData(")}>]})", 5566)]
    public void CompletionScore_calculated_correctly(string completion, int score)
    {
        var calculatedScore = aoc2021_Day10.GetCompletionScore(completion);

        Assert.Equal(score, calculatedScore);
    }
}