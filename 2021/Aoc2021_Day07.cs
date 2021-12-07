public class Aoc2021_Day07 : BaseDay
{
    public Aoc2021_Day07(string inputFileName) : base(inputFileName)
    {
        StartPos = Input[0].Split(',').Select(x => int.Parse(x)).ToList();
    }

    private Dictionary<int, int> FuelCost = new Dictionary<int, int>();
    private List<int> StartPos = new List<int>();

    public override void RunA()
    {
        var medianPos = StartPos.OrderBy(x => x).ElementAt((int)Math.Round(StartPos.Count() / 2.0));
        var FuelCost = StartPos.Select(x => Math.Abs(x - medianPos)).Sum();
        System.Console.WriteLine($"Fuel cost for {medianPos} is {FuelCost}");
    }

    public override void RunB()
    {
        var avgPos = (int)Math.Round(StartPos.Average());

        int pos = avgPos;
        StoreFuelCost(pos);
        StoreFuelCost(pos - 1);
        while (FuelCost.GetValueOrDefault(pos) < FuelCost[pos - 1])
        {
            pos++;
            StoreFuelCost(pos);
        }

        pos = avgPos;
        StoreFuelCost(pos + 1);
        while (FuelCost[pos] < FuelCost[pos + 1])
        {
            pos--;
            StoreFuelCost(pos);
        }

        var minCost = FuelCost.Min(x => x.Value);
        var minPos = FuelCost.Single(x => x.Value == minCost);

        System.Console.WriteLine($"Minimum fuel cost {minCost} for {minPos.Key}");
    }

    private void StoreFuelCost(int startingGuess)
    {
        FuelCost.TryAdd(startingGuess, CalcFuelCost(StartPos, startingGuess).Sum());
    }

    private List<int> CalcFuelCost(List<int> startPos, int corrDistance) =>
        startPos.Select(x => GetFuelCost(Math.Abs(x - corrDistance))).ToList();

    static int GetFuelCost(int distance)
    {
        int cost = 0;
        for (int i = 1; i <= distance; i++)
        {
            cost += i;
        }
        return cost;
    }
}