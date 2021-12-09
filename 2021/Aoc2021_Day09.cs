using System.Diagnostics;

public class Aoc2021_Day09 : BaseDay
{
    public Aoc2021_Day09(string inputFileName) : base(inputFileName)
    { }

    private List<(int, int)> LowPoints => new();

    public override void RunA()
    {
        var test = Input.ToArray();//.Select(x => x.Split)
        var inputArray = InputTo2dArray(test);

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

    // private List<(int, int)> CheckPositions((int row, int col) position, (int, int) arrDim)
    // {
    //     var pos = new List<(int, int)>();

    //     // Point to the right
    //     if (position.col < arrDim.Item2 - 1)
    //         pos.Add((position.row, position.col + 1));
    //     // Point above
    //     if (position.row > 0)
    //         pos.Add((position.row - 1, position.col));
    //     // Point to the left
    //     if (position.col > 1)
    //         pos.Add((position.row, position.col - 1));
    //     // Point below
    //     if (position.row < arrDim.Item1 - 1)
    //         pos.Add((position.row, position.col + 1));
    //     return pos;
    // }

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

    }
}