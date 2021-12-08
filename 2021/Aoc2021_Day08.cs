using System.Diagnostics;

public class Aoc2021_Day08 : BaseDay
{
    public Aoc2021_Day08(string inputFileName) : base(inputFileName)
    { }

    public override void RunA()
    {
        var outputLines = Input.Select(x => x.Split('|')[1].Trim(' '));
        List<int> CountLength = new() { 2, 3, 4, 7 };
        int count = 0;
        foreach (var line in outputLines)
        {
            count += line.Split().Where(x => CountLength.Contains(x.Length)).Count();
        }
        System.Console.WriteLine($"Count is {count}");
    }

    public override void RunB()
    {
        var digitSets = Input.Select(x => x.Split('|')[0].Trim(' ')).ToList();
        var outPuts = Input.Select(x => x.Split('|')[1].Trim(' ')).ToList();

        long sum = 0;
        for (int line = 0; line < digitSets.Count(); line++)
        {
            var digitSet = digitSets[line].Split(' ').ToList();
            var output = outPuts[line].Split(' ').ToList();
            Debug.Assert(digitSet.Count() == 10);

            var digitDictionary = RemoveEasyDigits(digitSet);
            RemoveHardDigits(digitSet, digitDictionary);

            Debug.Assert(digitSet.Count() == 0);

            // Decode output and calculate sum
            string decoded = "";
            foreach (var code in output)
            {
                var digit = digitDictionary.Single(x => x.Value.ToHashSet().SetEquals(code.ToHashSet())).Key;
                decoded += digit.ToString();
            }
            int decodedSum = int.Parse(decoded);
            sum += decodedSum;
        }
        System.Console.WriteLine($"Sum is {sum}");
    }

    // Method intentionally modfies parameters
    private static void RemoveHardDigits(List<string> digitSet, Dictionary<int, HashSet<char>> digitDictionary)
    {
        var three = digitSet.First(x => x.Length == 5 && x.Intersect(digitDictionary[1]).Count() == 2);
        digitDictionary.Add(3, three.ToHashSet());
        digitSet.Remove(three);

        var nine = digitSet.Single(x => x.Length == 6 && x.Intersect(digitDictionary[4]).Count() == 4);
        digitDictionary.Add(9, nine.ToHashSet());
        digitSet.Remove(nine);

        var zero = digitSet.Single(x => x.Length == 6 && x.Intersect(digitDictionary[1]).Count() == 2);
        digitDictionary.Add(0, zero.ToHashSet());
        digitSet.Remove(zero);

        var six = digitSet.Single(x => x.Length == 6);
        digitDictionary.Add(6, six.ToHashSet());
        digitSet.Remove(six);

        var five = digitSet.Single(x => x.Length == 5 && x.Intersect(digitDictionary[6]).Count() == 5);
        digitDictionary.Add(5, five.ToHashSet());
        digitSet.Remove(five);

        var two = digitSet.Single();
        digitDictionary.Add(2, two.ToHashSet());
        digitSet.Remove(two);
    }

    // Method intentionally modfies parameter
    private static Dictionary<int, HashSet<char>> RemoveEasyDigits(List<string> digitSet)
    {
        var digitDictionary = new Dictionary<int, HashSet<char>>();
        var one = digitSet.Single(x => x.Length == 2);
        var four = digitSet.Single(x => x.Length == 4);
        var seven = digitSet.Single(x => x.Length == 3);
        var eight = digitSet.Single(x => x.Length == 7);
        digitDictionary.Add(1, one.ToHashSet());
        digitDictionary.Add(4, four.ToHashSet());
        digitDictionary.Add(7, seven.ToHashSet());
        digitDictionary.Add(8, eight.ToHashSet());
        digitSet.Remove(one);
        digitSet.Remove(four);
        digitSet.Remove(seven);
        digitSet.Remove(eight);
        return digitDictionary;
    }
}