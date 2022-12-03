public class Aoc2022_Day03 : BaseDay
{
    public Aoc2022_Day03(string inputFileName) : base(inputFileName)
    {  }

    public override void RunA()
    {
        var sumOfPriorities = Input.Sum(items => GetPriority(items));
        Console.WriteLine(sumOfPriorities);
    }

    private static int GetPriority(string items)
    {
        var halfCount = items.Length / 2;
        var compartment1 = items.Take(halfCount).ToList();
        var compartment2 = items.Skip(halfCount).ToList();
        var commonItem = compartment1.Intersect(compartment2).First();
        var priority = GetPriority(commonItem);
        return priority;
    }

    private static int GetPriority(char c)
    {
        if (char.IsLower(c))
            return (Convert.ToInt32(c) - 96);
        if (char.IsUpper(c))
            return Convert.ToInt32(c) - 38;
        throw new ArgumentException($"{c} is not a valid character");
    }

    public override void RunB()
    {
        
    }
}