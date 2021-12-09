using System.Diagnostics;

public class Aoc2021_Day09 : BaseDay
{
    public Aoc2021_Day09(string inputFileName) : base(inputFileName)
    { }

    private List<(int, int)> LowPoints { get; set; } = new();
    private int[,] inputArray;

    public override void RunA()
    {
        var test = Input.ToArray();//.Select(x => x.Split)
        inputArray = InputTo2dArray(test);

        for (int i = 0; i < test.Count(); i++)
        {
            for (int j = 0; j < test[0].Length; j++)
            {
                bool left = j <= 0 ? true : inputArray[i, j] < inputArray[i, j - 1];
                bool right = j >= test[0].Length - 1 ? true : inputArray[i, j] < inputArray[i, j + 1];
                bool above = i <= 0 ? true : inputArray[i, j] < inputArray[i - 1, j];
                bool below = i >= test.Length - 1 ? true : inputArray[i, j] < inputArray[i + 1, j];
                if (left && // left point
                    right && // right point
                    above && // above
                    below)
                    LowPoints.Add((i, j));
            }
        }
        var sum = LowPoints.Sum(x => inputArray[x.Item1, x.Item2] + 1);
        System.Console.WriteLine($"Risk level is {sum}");
    }

    private int[,] InputTo2dArray(string[] test)
    {
        var inputArray = new int[test.Count(), test[0].Length];
        for (int i = 0; i < test.Count(); i++)
        {
            var row = Input[i].ToArray();
            for (int j = 0; j < test[0].Length; j++)
            {
                inputArray[i, j] = int.Parse(row[j].ToString());
            }
        }
        return inputArray;
    }

    public override void RunB()
    {
        List<HashSet<(int, int)>> basins = new List<HashSet<(int, int)>>();

        foreach (var p in LowPoints)
        {
            var basin = new HashSet<(int, int)>() { p };

            var coordsToCheck = new List<(int, int)>() { p };
            var alreadyChecked = new List<(int, int)>() { };
            while (coordsToCheck.Any())
            {
                var nextCoordsToCheck = new List<(int, int)>();
                foreach (var c in coordsToCheck)
                {
                    if (!alreadyChecked.Contains(c))
                    {
                        alreadyChecked.Add(c);
                        if (c.Item2 > 0 && inputArray[c.Item1, c.Item2 - 1] != 9)
                        {
                            nextCoordsToCheck.Add((c.Item1, c.Item2 - 1));
                            basin.Add((c.Item1, c.Item2 - 1));
                        }
                        if (c.Item2 < Input[0].Length - 1 && inputArray[c.Item1, c.Item2 + 1] != 9)
                        {
                            nextCoordsToCheck.Add((c.Item1, c.Item2 + 1));
                            basin.Add((c.Item1, c.Item2 + 1));
                        }
                        if (c.Item1 > 0 && inputArray[c.Item1 - 1, c.Item2] != 9)
                        {
                            nextCoordsToCheck.Add((c.Item1 - 1, c.Item2));
                            basin.Add((c.Item1 - 1, c.Item2));
                        }
                        if (c.Item1 < Input.Count() - 1 && inputArray[c.Item1 + 1, c.Item2] != 9)
                        {
                            nextCoordsToCheck.Add((c.Item1 + 1, c.Item2));
                            basin.Add((c.Item1 + 1, c.Item2));
                        }
                    }
                }
                coordsToCheck = nextCoordsToCheck;
            }
            basins.Add(basin);
        }
        var score = basins.Select(x => x.Count()).OrderByDescending(x => x).Take(3).Aggregate((b1, b2) => b1 * b2);
        System.Console.WriteLine($"Score is {score}");
    }
}