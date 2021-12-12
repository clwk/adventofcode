using System.Diagnostics;

public class Aoc2021_Day11 : BaseDay
{
    private int Rows { get; set; }
    private int Cols { get; set; }
    private List<List<int>> InputInt { get; set; }
    private List<(int, int)> AddOnePoints { get; set; } = new();
    private List<(int, int)> FlashedPointsThisRound { get; set; } = new();
    public Aoc2021_Day11(string inputFileName) : base(inputFileName)
    {
        Rows = Input.Count();
        Cols = Input[0].Length;
        InputInt = Input
           .Select(x => x.ToList()
           .Select(s => int.Parse(s.ToString()))
           .ToList()).ToList();
    }

    public override void RunA()
    {
        int totalFlashCount = 0;
        int nrOfSteps = 100;
        for (int step = 0; step < nrOfSteps; step++)
        {
            totalFlashCount = PerformOctopusStep(totalFlashCount);
        }
        System.Console.WriteLine($"Number of flashes {totalFlashCount} in {nrOfSteps} steps. ");
    }

    private int PerformOctopusStep(int totalFlashCount)
    {
        InputInt = InputInt.Select(x => x.Select(x => x + 1).ToList()).ToList();

        int flashCount = 1;
        while (flashCount > 0)
        {
            var flashes = InputInt.Select((x, r) => x.Select((y, c) => GetFlashedCoordsFromPoint((r, c), y)))
                .SelectMany(f1 => f1)
                .SelectMany(f2 => f2).ToList();

            foreach (var coord in flashes)
            {
                InputInt[coord.Item1][coord.Item2] += 1;
            }
            flashCount = flashes.Count();

        }

        // Reset flashed octopuses to 0
        InputInt = InputInt.Select(x => x.Select(x => x > 9 ? 0 : x).ToList()).ToList();

        totalFlashCount += FlashedPointsThisRound.Count();
        FlashedPointsThisRound.Clear();

        foreach (var line in InputInt)
        {
            Debug.WriteLine(string.Join(' ', line));
        }
        Debug.WriteLine("");
        return totalFlashCount;
    }

    private void ReparseInput()
    {
        InputInt = Input
            .Select(x => x.ToList()
            .Select(s => int.Parse(s.ToString()))
            .ToList()).ToList();
    }

    private bool AreAllZero(List<List<int>> listToCheck)
    {
        foreach (var line in listToCheck)
        {
            var areZero = line.All(x => x == 0);
            if (!areZero) return false;
        }
        return true;
    }

    private List<(int, int)> GetFlashedCoordsFromPoint((int row, int col) point, int value)
    {
        List<(int, int)> points = new();
        if (FlashedPointsThisRound.Contains(point))
            return points;
        if (value < 10)
            return points;

        FlashedPointsThisRound.Add(point);
        if (point.row > 0)
        {
            // 12 o'clock
            points.Add((point.row - 1, point.col));
            // 10:30
            if (point.col > 0)
                points.Add((point.row - 1, point.col - 1));

            // 01:30 
            if (point.col < Cols - 1)
                points.Add((point.row - 1, point.col + 1));

        }
        // 03:00
        if (point.col < Cols - 1)
            points.Add((point.row, point.col + 1));
        // 09:00
        if (point.col > 0)
            points.Add((point.row, point.col - 1));

        if (point.row < Rows - 1)
        {
            // 06:00
            points.Add((point.row + 1, point.col));
            // 07:30
            if (point.col > 0)
                points.Add((point.row + 1, point.col - 1));

            // 04:30 
            if (point.col < Cols - 1)
                points.Add((point.row + 1, point.col + 1));
        }
        // AddOnePoints.AddRange(points);
        return points;
    }

    public override void RunB()
    {
        ReparseInput();

        int totalFlashCount = 0;
        int nrOfSteps = 0;
        while (!AreAllZero(InputInt))
        {
            totalFlashCount = PerformOctopusStep(totalFlashCount);
            nrOfSteps++;
        }
        System.Console.WriteLine($"Number of flashes {totalFlashCount} in {nrOfSteps} steps. ");
    }
}