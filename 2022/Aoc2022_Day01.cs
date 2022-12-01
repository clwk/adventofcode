public class Aoc2022_Day01 : BaseDay
{
    public Aoc2022_Day01(string inputFileName) : base(inputFileName)
    { }

    private List<int> ResultFromA { get; set; }

    public override void RunA()
    {
        var inputGrouped = InputGrouped(InputAsString);
        ResultFromA = ParseInt(inputGrouped).Select(x => x.Sum()).ToList();

        System.Console.WriteLine($"Result from A {ResultFromA.Max()}");
    }

    public override void RunB()
    {
        var resultFromB = ResultFromA.OrderByDescending(x => x).Take(3).Sum();
        System.Console.WriteLine($"Result from B {resultFromB}");
    }

    private static List<List<int>> ParseInt(IEnumerable<IEnumerable<string>> inputGrouped) =>
        inputGrouped
            .Select(x => x.Select(int.Parse)
                .ToList())
            .ToList();

    private static List<List<string>> InputGrouped(string input) =>
        input
            .Split("\n\n")
            .Select(x => x.Split("\n")
                .Where(y => !string.IsNullOrEmpty(y)).ToList()).ToList();
}