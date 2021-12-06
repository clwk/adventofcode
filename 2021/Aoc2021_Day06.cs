public class Aoc2021_Day06 : BaseDay
{
    public Aoc2021_Day06(string inputFileName) : base(inputFileName)
    { }

    public override void RunA()
    {
        var initialPopulation = Input[0].Split(',').Select(x => int.Parse(x)).ToList();

        List<int> nextDay = new List<int>(initialPopulation);
        for (int day = 1; day <= 80; day++)
        {
            nextDay = GetNextPopulation(nextDay);
            // System.Console.WriteLine($"Day {day}: " + string.Join(",", nextDay.ToArray()) + $". Count {nextDay.Count()}");
            System.Console.WriteLine($"Day {day}: " + $". Count {nextDay.Count()}");
        }

        System.Console.WriteLine($"Fish count: {nextDay.Count()}");
    }

    private List<int> GetNextPopulation(List<int> initialPopulation)
    {
        List<int> newFishes = new List<int>();
        List<int> nextGeneration = new List<int>(initialPopulation);
        for (int fish = 0; fish < initialPopulation.Count; fish++)
        {
            if (nextGeneration[fish] > 0)
            {
                nextGeneration[fish]--;
            }
            else
            {
                nextGeneration[fish] = 6;
                newFishes.Add(8);
            }
        }
        nextGeneration.AddRange(newFishes);
        return nextGeneration;
    }

    public override void RunB()
    {
        var initialPopulation = Input[0].Split(',').Select(x => int.Parse(x)).ToList();

    }
}