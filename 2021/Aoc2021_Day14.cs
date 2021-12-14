public class Aoc2021_Day14 : BaseDay
{
    private List<string> Template { get; }
    private Dictionary<string, string> Rules { get; }
    public Aoc2021_Day14(string inputFileName) : base(inputFileName)
    {
        // Template = Input[0].ToCharArray().ToList();
        Template = Input[0].ToCharArray().Select(x => x.ToString()).ToList();
        // var rules=Input.Skip(2).Select(x=>x.Split(" -> ")).ToLookup()
        // var rules2= Input.Skip(2).ToLookup(x => x.Split(" -> ")[0], x => x.Split(" -> ")[1]);
        Rules = Input.Skip(2).ToDictionary(x => x.Split(" -> ")[0], x => x.Split(" -> ")[1]);
    }

    public override void RunA()
    {
        int diff = RunPolymerization(10);
        System.Console.WriteLine($"Difference most common - least common is {diff}.");
    }

    private int RunPolymerization(int steps)
    {
        var listan = new LinkedList<string>(Template);
        var frequencies = new Dictionary<string, int>();
        
        for (int step = 1; step <= steps; step++)
        {
            var currentNode = listan.First;
            while (currentNode.Next != null)
            {
                var str = currentNode.Value + currentNode.Next.Value;
                var insert = Rules[str];
                listan.AddAfter(currentNode, new LinkedListNode<string>(insert));
                currentNode = currentNode.Next.Next;
            }
        }


        foreach (var key in Rules.Values.Distinct())
        {
            var frequency = listan.Count(x => x == key);
            frequencies.Add(key, frequency);
        }

        var diff = frequencies.Max(f => f.Value) - frequencies.Min(f => f.Value);
        return diff;
    }

    public override void RunB()
    {

    }
}