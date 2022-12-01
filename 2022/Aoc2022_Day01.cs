public class Aoc2022_Day01 : BaseDay
{
    public Aoc2022_Day01(string inputFileName) : base(inputFileName)
    { }

    private List<int> ResultFromA { get; set; }

    public override void RunA()
    {
        var caloriesByElf = new List<int>();
        int caloriesSum = 0;
        foreach (var line in Input)
        {
            if (string.IsNullOrEmpty(line))
            {
                caloriesByElf.Add(caloriesSum);
                caloriesSum = 0;
            }
            else
            {
                caloriesSum += int.Parse(line);
            }
        }
        ResultFromA = caloriesByElf;
        System.Console.WriteLine($"Result from A {caloriesByElf.Max()}");

    }

    public override void RunB()
    {
        var resultFromB = ResultFromA.OrderByDescending(x => x).Take(3).Sum();
        System.Console.WriteLine($"Result from B {resultFromB}");
    }
}