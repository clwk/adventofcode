public class Aoc2021_Day13 : BaseDay
{
    List<(int, int)> Paper = new();
    List<string[]> folds = new();

    public Aoc2021_Day13(string inputFileName) : base(inputFileName)
    { }

    public override void RunA()
    {
        ParseInput();

        var foldAtLine = int.Parse(folds[0][1]);
        List<(int, int)> AfterFold = new();
        if (folds[0][0] == "x")
        {
            AfterFold = FoldX(foldAtLine);
        }
        else
        {
            AfterFold = FoldY(foldAtLine);
        }

        System.Console.WriteLine($"Number of points: {AfterFold.Count()}");

        // int lineNr = 7;
        // FoldY(lineNr);
    }

    private List<(int, int)> FoldY(int lineNr)
    {
        var foldPart = Paper.Where(p => p.Item1 > lineNr).ToList();
        var nonFolded = Paper.Where(p => p.Item1 < lineNr).ToList();

        var maxY = Paper.Max(c => c.Item1);
        var adjust = 2*lineNr-maxY;
        var foldedCoords = foldPart.Select(c => (adjust+maxY - c.Item1, c.Item2)).ToList();

        nonFolded.AddRange(foldedCoords);
        var distinctCoords = nonFolded.Distinct();
        return distinctCoords.ToList();
    }

    private List<(int, int)> FoldX(int lineNr)
    {
        var foldPart = Paper.Where(p => p.Item2 > lineNr).ToList();
        var nonFolded = Paper.Where(p => p.Item2 < lineNr).ToList();

        var maxX = Paper.Max(c => c.Item2);
        var adjust = 2*lineNr-maxX;
        var foldedCoords = foldPart.Select(c => (c.Item1, adjust+maxX - c.Item2)).ToList();

        nonFolded.AddRange(foldedCoords);
        var distinctCoords = nonFolded.Distinct();
        return distinctCoords.ToList();
    }

    private void ParseInput()
    {
        int lineNr = 0;

        while (Input[lineNr] != "")
        {
            var coord = Input[lineNr].Split(',');
            Paper.Add((int.Parse(coord[1]), int.Parse(coord[0])));
            lineNr++;
        }

        lineNr++;

        while (lineNr < Input.Count())
        {
            var line = Input[lineNr];
            folds.Add(line.Split(' ')[2].Split('='));
            lineNr++;
        }
    }

    public override void RunB()
    {

    }
}