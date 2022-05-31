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
}