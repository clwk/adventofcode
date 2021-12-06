public class Aoc2021_Day06 : BaseDay
{
    public Aoc2021_Day06(string inputFileName) : base(inputFileName)
    { }

    public override void RunA()
    {
        var initialPopulation = Input[0].Split(',').Select(x => int.Parse(x)).ToList();
        List<List<int>> PopulationsByDay = new List<List<int>>();
        PopulationsByDay.Add(initialPopulation);

        for (int day = 1; day <= 80; day++)
        {
            List<int> nextDay = GetNextPopulation(PopulationsByDay[day - 1]);
            // System.Console.WriteLine(string.Join(",", nextDay.ToArray()));
            PopulationsByDay.Add(nextDay);
        }

        System.Console.WriteLine($"Fish count: {PopulationsByDay.Last().Count()}");
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

    }
}