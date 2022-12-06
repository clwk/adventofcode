public class Aoc2022_Day06 : BaseDay
{
    public Aoc2022_Day06(string inputFileName) : base(inputFileName)
    { }

    public override void RunA()
    {
        var positions = Input.Select(x => GetMarkerPosition(x));
        foreach (var pos in positions)
        {
            System.Console.WriteLine($"Position {pos}");
        }
    }

    private int GetMarkerPosition(string buffer)
    {
        int i = 0;
        int markerLength = 4;
        var test = buffer.Skip(i).Take(markerLength).Distinct();
        while (buffer.Skip(i).Take(markerLength).Distinct().Count() != 4)
        {
            i++;
        }
        return markerLength + i;
    }

    public override void RunB()
    {

    }
}