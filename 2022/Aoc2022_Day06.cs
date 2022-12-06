public class Aoc2022_Day06 : BaseDay
{
    public Aoc2022_Day06(string inputFileName) : base(inputFileName)
    { }

    public override void RunA()
    {
        var positions = Input.Select(x => GetMarkerPosition(x, 4));
        foreach (var pos in positions)
        {
            System.Console.WriteLine($"Position {pos}");
        }
    }

    private int GetMarkerPosition(string buffer, int markerLength)
    {
        int i = 0;
        while (buffer.Skip(i).Take(markerLength).Distinct().Count() != markerLength)
        {
            i++;
        }
        return markerLength + i;
    }

    public override void RunB()
    {
        var position = GetMarkerPosition(Input.First(), 14);
        Console.WriteLine($"Position {position}");
    }
}