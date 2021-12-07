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
            // System.Console.WriteLine($"Day {day}: " + $". Count {nextDay.Count()}");
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

        List<int> currentFishes = new(initialPopulation);
        List<int> newFishes = new();
        int targetDay = 80;

        // int inInterval = 0;
        int lastCount = 0;

        while (currentFishes.Count() > lastCount)
        {
            lastCount = currentFishes.Count();
            for (int fish = 0; fish < currentFishes.Count(); fish++)
            {
                if (currentFishes[fish] + 9 <= targetDay + 8)
                {
                    newFishes.Add(currentFishes[fish] + 9);
                    currentFishes[fish] += 7;
                }
            }
            currentFishes.AddRange(newFishes);
            newFishes = new();
            var min = currentFishes.Min();
            // inInterval = currentFishes.Count(x => x < targetDay);
            var max = currentFishes.Max();
        }

        System.Console.WriteLine($"Fish count {currentFishes.Count()}");
    }
}