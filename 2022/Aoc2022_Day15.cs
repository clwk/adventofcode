public class Aoc2022_Day15 : BaseDay
{
    public Aoc2022_Day15(string inputFileName) : base(inputFileName)
    {
        ParseInput();
    }

    private List<(int x, int y)> _sensors = new();
    private List<(int x, int y)> _beacons = new();

    public override void RunA()
    {

        // var sensorInput = Input.Select(x => x.Skip(10).ToString().Split(':')[0].Split(',')[0]
    }

    private void ParseInput()
    {
        char[] delimiterChars = { '=', ',', ':' };
        // var test = Input[0].Split(delimiterChars);
        // var testList = Input.Select(x => ((int.Parse(x.Split(delimiterChars)[1])), int.Parse(x.Split(delimiterChars)[3]))).ToList();
        _sensors = Input.Select(x => ((int.Parse(x.Split(delimiterChars)[1])), int.Parse(x.Split(delimiterChars)[3]))).ToList();
        _beacons = Input.Select(x => ((int.Parse(x.Split(delimiterChars)[5])), int.Parse(x.Split(delimiterChars)[7]))).ToList();
    }

    public override void RunB()
    {

    }
}