public class Aoc2021_Day10 : BaseDay
{
    public Aoc2021_Day10(string inputFileName) : base(inputFileName)
    { }

    public override void RunA()
    {
        Dictionary<char, char> matching = new();
        matching.Add('(', ')');
        matching.Add('{', '}');
        matching.Add('[', ']');
        matching.Add('<', '>');

        List<string> unmatchedCollection = new List<string>();
        foreach (var line in Input)
        {
            var lineToSplit = line.ToList();
            string unmatched = "";
            while (lineToSplit.Count() != 0)
            {

                if (!lineToSplit.Remove(matching[lineToSplit.First()]))
                    unmatched += lineToSplit.First();
                lineToSplit.Remove(lineToSplit.First());
                // line.Select(x => 
                // foreach (var c in line)
                // {

                // }
                unmatchedCollection.Add(unmatched);
            }
        }
        
    }

    public override void RunB()
    {

    }
}