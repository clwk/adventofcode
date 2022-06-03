public class Aoc2021_Day10 : BaseDay
{
    private Dictionary<char, char> Matching = new(){
        {'(', ')'},
        {'{', '}'},
        {'[', ']'},
        {'<', '>'}
    };

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

    private Dictionary<char, int> CompletionScore = new() {
        {')', 1},
        {']', 2},
        {'}', 3},
        {'>', 4}
    };

    public Aoc2021_Day10(string inputFileName) : base(inputFileName)
    { }

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

    internal string FindFirstIllegal(string line)
    {
        string lineFiltered = FilterClosedExprs(line);

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

    private string FilterClosedExprs(string line)
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

        return lineFiltered;
    }

    public override void RunB()
    {
        var incompleteLines = GetIncompleteLines(Input);
        List<long> CompletionScores = new();
        foreach (var line in incompleteLines)
        {
            var filtered = FilterClosedExprs(line);
            var completeWith = GetAutocompletion(filtered);
            CompletionScores.Add(GetCompletionScore(completeWith));
        }

        CompletionScores.Sort();

        var middleScoreIndex = CompletionScores.Count / 2;
        System.Console.WriteLine($"Score is {CompletionScores[middleScoreIndex]}");

    }

    internal long GetCompletionScore(string completion)
    {
        long score = 0;
        long factor = 5;

        foreach (var character in completion)
        {
            score = score * factor;
            score = score + CompletionScore[character];
        }
        return score;
    }

    internal string GetAutocompletion(string line)
    {
        var completeWith = string.Concat(line.Reverse().Select(x => Matching[x]));
        return completeWith;
    }

    internal List<string> GetIncompleteLines(List<string> inputLines)
    {
        List<string> incompleteLines = new();
        // Discard corrupted lines

        foreach (var line in inputLines)
        {
            string firstIncorrect = FindFirstIllegal(line);
            if (string.IsNullOrEmpty(firstIncorrect))
                incompleteLines.Add(line);
        }
        return incompleteLines;
    }
}