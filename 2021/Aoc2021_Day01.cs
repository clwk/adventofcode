public class Aoc2021_Day01 : BaseDay
{
    public Aoc2021_Day01(string inputFileName) : base(inputFileName)
    { }
    
    public override void RunA()
    {
        var diffs = Input.Select((val, idx) =>
            int.Parse(Input[idx < Input.Count - 1 ? idx + 1 : idx]) - int.Parse(val));

        var positive = diffs.Where(x => x > 0).Count();
        System.Console.WriteLine($"Positive {positive}");
    }

    public override void RunB()
    {
        var windows = Input
            .Select((val, idx) =>
                int.Parse(Input[idx < Input.Count - 2 ? idx + 2 : idx]) +
                int.Parse(Input[idx < Input.Count - 2 ? idx + 1 : idx]) +
                int.Parse(val))
            .Take(..^2).ToList();

        var diffs = windows.Select((val, idx) =>
            windows[idx < windows.Count - 1 ? idx + 1 : idx] - val);

        var positive = diffs.Where(x => x > 0).Count();

        System.Console.WriteLine($"Positive {positive}");
    }
}