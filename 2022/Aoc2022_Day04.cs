public class Aoc2022_Day04 : BaseDay
{
    private IEnumerable<(IEnumerable<int>, IEnumerable<int>)> _rangesAsTuples;

    public Aoc2022_Day04(string inputFileName) : base(inputFileName)
    { }

    public override void RunA()
    {
        char[] splitChars = { ',', '-' };
        var startEndStartEndAsInts =
            Input.Select(l => (l.Split(splitChars)).Select(s => int.Parse(s)).ToList()).ToList();
        _rangesAsTuples = startEndStartEndAsInts.Select(l =>
            (Enumerable.Range(l[0], l[1] - l[0] + 1), Enumerable.Range(l[2], l[3] - l[2] + 1)));
        var rangesCommons = _rangesAsTuples
            .Where(s => (s.Item1.Intersect(s.Item2)).Count() == Math.Min(s.Item1.Count(), s.Item2.Count())).ToList();
        System.Console.WriteLine($"Number of pairs where one range contains the other: {rangesCommons.Count()}");
    }

    public override void RunB()
    {
        var overlappingRanges = _rangesAsTuples.Where(l => l.Item1.Intersect(l.Item2).Any());
        System.Console.WriteLine($"Overlapping ranges at all: {overlappingRanges.Count()}");
    }
}