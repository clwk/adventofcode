public class Aoc2021_Day25 : BaseDay
{
    protected int DimRow { get; }
    protected int DimCol { get; }
    protected bool Moved { get; set; }

    public Aoc2021_Day25(string inputFileName) : base(inputFileName)
    {
        DimRow = Input.Count;
        DimCol = Input[0].Count();
        Moved = true;
    }

    public override void RunA()
    {
        var inputArray = InputTo2dArray();
        char[,] nextStepArr = inputArray;
        int step = 0;

        while (Moved)
        {
            step++;
            Moved = false;
            nextStepArr = PerformRightStep(nextStepArr);
            nextStepArr = PerformDownStep(nextStepArr);
            System.Console.WriteLine($"Step {step}" + Environment.NewLine);
            PrintArr(nextStepArr);
        }

        Console.WriteLine($"Last step was {step}");
    }

    private char[,] InputTo2dArray()
    {
        var inputArray = new char[Input.Count(), Input[0].Length];
        for (int i = 0; i < Input.Count(); i++)
        {
            var row = Input[i];
            for (int j = 0; j < Input[0].Length; j++)
            {
                inputArray[i, j] = row[j];
            }
        }
        return inputArray;
    }

    private void PrintArr(char[,] nextStepArr)
    {
        for (int row = 0; row < Input.Count; row++)
        {
            for (int col = 0; col < Input[0].Count(); col++)
            {
                System.Console.Write(nextStepArr[row, col]);
            }
            System.Console.Write(Environment.NewLine);
        }
    }

    private char[,] PerformRightStep(char[,] inputArray)
    {
        var nextStepArr = new char[DimRow, DimCol];

        for (int row = 0; row < Input.Count; row++)
        {
            for (int col = 0; col < Input[0].Length; col++)
            {
                var nextPos = GetNextPosRight(row, col);

                if (inputArray[row, col] == '>' &&
                inputArray[nextPos.Item1, nextPos.Item2] == '.')
                {
                    nextStepArr[nextPos.Item1, nextPos.Item2] = inputArray[row, col];
                    nextStepArr[row, col] = '.';
                    Moved = true;
                }
                else if (nextStepArr[row, col] == 0)
                {
                    nextStepArr[row, col] = inputArray[row, col];
                }
            }
        }
        return nextStepArr;
    }

    private char[,] PerformDownStep(char[,] inputArray)
    {
        var nextStepArr = new char[DimRow, DimCol];

        for (int row = 0; row < Input.Count(); row++)
        {
            for (int col = 0; col < Input[0].Length; col++)
            {
                var nextPos = GetNextPosDown(row, col);

                if (inputArray[row, col] == 'v' &&
                    inputArray[nextPos.Item1, nextPos.Item2] == '.')
                {
                    nextStepArr[nextPos.Item1, nextPos.Item2] = inputArray[row, col];
                    nextStepArr[row, col] = '.';
                    Moved = true;
                }
                else if (nextStepArr[row, col] == 0)
                {
                    nextStepArr[row, col] = inputArray[row, col];
                }
            }
        }
        return nextStepArr;
    }

    private (int, int) GetNextPosDown(int row, int col)
    {
        return row == (DimRow - 1) ? (0, col) : (row + 1, col);
    }

    private (int, int) GetNextPosRight(int row, int col)
    {
        return col == (DimCol - 1) ? (row, 0) : (row, col + 1);
    }

    public override void RunB()
    {

    }
}