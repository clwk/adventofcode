public class Day03
{
    public void RunA()
    {
        var input = File.ReadAllLines(@".\day03.input.txt");//.ToList();

        int skip = 0;

        var gammaString = "";
        var newInput = input.Select(x => string.Concat(x.Skip(skip))).ToArray();

        while (!string.IsNullOrEmpty(newInput[0]))
        {
            var nr1 = newInput.Select(x => x.First()).Count(x => x == '1');
            if (nr1 > input.Count() / 2)
                gammaString += "1";
            else
                gammaString += "0";
            skip++;
            newInput = input.Select(x => string.Concat(x.Skip(skip))).ToArray();
        }

        var gammaBits = Convert.ToInt32(gammaString, 2);
        var xorString = new string('1', input[0].Length);
        var xorBits = Convert.ToInt32(xorString, 2);

        var epsilonBits = gammaBits ^ xorBits;

        var epsilonString = Convert.ToString(epsilonBits, 2);

        System.Console.WriteLine($"Gamma string {gammaString}");
        System.Console.WriteLine($"Epsilon string {epsilonString}");
        System.Console.WriteLine($"Power consumption {gammaBits * epsilonBits}");
    }

    public void RunB()
    {
        var input = File.ReadAllLines(@".\day03.input.txt");//.ToList();

        string oxyRating = GetRating(input);
        string coRating = GetRating(input, '0');

        var oxyBits = Convert.ToInt32(oxyRating, 2);
        var coBits = Convert.ToInt32(coRating, 2);

        System.Console.WriteLine($"Oxy rating: {oxyRating}");
        System.Console.WriteLine($"Co rating: {string.Concat(coRating)}");

        System.Console.WriteLine($"Suuport rating {oxyBits * coBits}");
    }

    private static string GetRating(string[] input, char preferChar = '1')
    {
        int skip = 0;

        var newInput = input.Select(x => string.Concat(x.Skip(skip))).ToArray();
        var lastchar = ' ';

        while (skip < input[0].Length && newInput.Length > 1)
        {
            var nr1 = newInput.Select(x => x[skip]).Count(x => x == '1');
            var nr0 = newInput.Select(x => x[skip]).Count(x => x == '0');
            if (nr1 >= nr0)
                lastchar = preferChar;
            else
                lastchar = Convert.ToChar((int.Parse(preferChar.ToString()) ^ 1).ToString());

            newInput = newInput.Where(l => l[skip] == lastchar).ToArray();
            skip++;
        }

        return newInput[0];
    }
}
