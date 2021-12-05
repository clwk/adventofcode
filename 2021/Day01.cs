public class Day01
{
    public void Run()
    {
        var input = File.ReadAllLines(@".\day01.input.txt").ToList();
        
        var diffs = input.Select((val, idx) =>
            int.Parse(input[idx < input.Count - 1 ? idx + 1 : idx]) - int.Parse(val));

        var positive = diffs.Where(x => x > 0).Count();
        System.Console.WriteLine($"Positive {positive}");
    }

    public void RunB()
    {
        var input = File.ReadAllLines(@".\day01.input.txt").ToList();

        var windows = input
            .Select((val, idx) =>
                int.Parse(input[idx < input.Count - 2 ? idx + 2 : idx]) +
                int.Parse(input[idx < input.Count - 2 ? idx + 1 : idx]) +
                int.Parse(val))
            .Take(..^2).ToList();

        var diffs = windows.Select((val, idx) =>
            windows[idx < windows.Count - 1 ? idx + 1 : idx] - val);

        var positive = diffs.Where(x => x > 0).Count();

        System.Console.WriteLine($"Positive {positive}");
    }
}