public class Aoc2021_Day10 : BaseDay
{
    private Dictionary<char, char> Matching = new();

    private List<string> RemoveThese = new List<string>() {
        "()",
        "{}",
        "[]",
        "<>"
    };

    private Dictionary<char, int> CharacterScore = new() {
        {')', 3},
        {']', 57},
        {'}', 1197},
        {'>', 25137}
    };

    public Aoc2021_Day10(string inputFileName) : base(inputFileName)
    {
        Matching.Add('(', ')');
        Matching.Add('{', '}');
        Matching.Add('[', ']');
        Matching.Add('<', '>');
    }

    public override void RunA()
    {
        List<string> unmatchedCollection = new List<string>();

        foreach (var line in Input)
        {
            string firstIncorrect = FindFirstIllegal(line);
            if (!string.IsNullOrEmpty(firstIncorrect))
                unmatchedCollection.Add(firstIncorrect);
        }

        int totalScore = 0;
        foreach (var unmatchedChar in unmatchedCollection)
        {
            totalScore += CharacterScore[char.Parse(unmatchedChar)];
        }

        System.Console.WriteLine($"Total score is {totalScore}");
    }

    public string FindFirstIllegal(string line)
    {
        string lineFiltered = new string(line);
        int oldLength = 0;
        int newLength = -1;

        while (oldLength != newLength)
        {
            oldLength = lineFiltered.Length;
            foreach (var replacement in RemoveThese)
            {
                lineFiltered = lineFiltered.Replace(replacement, "");
            }
            newLength = lineFiltered.Length;
        }

        for (int i = 0; i < lineFiltered.Length - 1; i++)
        {
            if (Matching.ContainsKey(lineFiltered[i]) &&
                Matching.ContainsValue(lineFiltered[i + 1]) &&
                Matching[lineFiltered[i]] != lineFiltered[i + 1])
            {
                return lineFiltered[i + 1].ToString();
            }
        }

        return "";
    }

    public override void RunB()
    {

    }
}