public class Day05 : BaseDay
{
    public Day05(string inputFileName) : base(inputFileName)
    { }
    
    public override void RunA()
    {
        var points = ParseHorizontalVerticalLines(Input);
        var groupPoints = points.GroupBy(x => x);
        int overlapPoints = groupPoints.Count(x => x.Count() > 1);
        System.Console.WriteLine($"Number of overlapping points {overlapPoints}");
    }

    public override void RunB()
    {
        var points = ParseHorizontalVerticalLines(Input, true);
        var groupPoints = points.GroupBy(x => x);
        int overlapPoints = groupPoints.Count(x => x.Count() > 1);
        System.Console.WriteLine($"Number of overlapping points {overlapPoints}");
    }

    static List<(int, int)> ParseHorizontalVerticalLines(List<string> input, bool diagonal = false)
    {
        var linePoints = new List<(int, int)>();

        foreach (var line in input)
        {
            var twoCoords = line.Split(" -> ");
            var coordA = twoCoords[0];
            var coordB = twoCoords[1];
            (int x1, int y1) point1 = TupleFromCoord(coordA);
            (int x2, int y2) point2 = TupleFromCoord(coordB);

            if (point1.x1 == point2.x2)
            {
                var range = GenerateSeqList(point1.y1, point2.y2);

                foreach (var yVal in range)
                {
                    linePoints.Add((point1.x1, yVal));
                }
            }
            else if (point1.y1 == point2.y2)
            {
                var range = GenerateSeqList(point1.x1, point2.x2);

                foreach (var xVal in range)
                {
                    linePoints.Add((xVal, point1.y1));
                }
            }
            else if (diagonal)
            {
                var range = GenerateDiagList(point1, point2);

                foreach (var point in range)
                {
                    linePoints.Add(point);
                }
            }

        }
        return linePoints;
    }

    private static List<(int x, int y)> GenerateDiagList((int x, int y) point1, (int x, int y) point2)
    {
        var list = new List<(int, int)>();
        var start = point1.x < point2.x ? point1 : point2;
        var end = point1.x < point2.x ? point2 : point1;

        int x = start.x;
        int y = start.y;

        while (x <= end.x)
        {
            list.Add((x, y));
            x++;

            if (start.y < end.y)
                y++;
            else
                y--;
        }

        return list;
    }

    private static List<int> GenerateSeqList(int x1, int x2)
    {
        var list = new List<int>();
        int start;
        int end;

        if (x1 < x2)
        {
            start = x1;
            end = x2;
        }
        else
        {
            start = x2;
            end = x1;
        }
        for (int i = start; i <= end; i++)
        {
            list.Add(i);
        }
        return list;
    }

    static (int, int) TupleFromCoord(string coord)
    {
        var x = int.Parse(coord.Split(',')[0]);
        var y = int.Parse(coord.Split(',')[1]);
        return (x, y);
    }
}
