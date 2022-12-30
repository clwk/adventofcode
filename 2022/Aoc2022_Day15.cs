public class Aoc2022_Day15 : BaseDay
{
    public Aoc2022_Day15(string inputFileName) : base(inputFileName)
    {
        ParseInput();
    }

    private List<(int x, int y)> _sensors = new();
    private List<(int x, int y)> _beacons = new();
    private HashSet<int> NoBeaconCols = new();
    private const int rowToCheck = 2000000;

    public override void RunA()
    {
        for (int i = 0; i < _sensors.Count; i++)
        {
            var searchDistance = GetDistance(_sensors[i], _beacons[i]);
            var sensorRow = _sensors[i].y;
            var sensorCol = _sensors[i].x;

            var verticalDistance = Math.Abs(sensorRow - rowToCheck);
            if (searchDistance > verticalDistance)
            {
                var fillCols = searchDistance - verticalDistance;
                for (int col = sensorCol - fillCols; col < sensorCol + fillCols; col++)
                {
                    NoBeaconCols.Add(col);
                }
            }
        }
        System.Console.WriteLine($"Number of cols: {NoBeaconCols.Count}");
    }

    private void ParseInput()
    {
        char[] delimiterChars = { '=', ',', ':' };
        _sensors = Input.Select(x => ((int.Parse(x.Split(delimiterChars)[1])), int.Parse(x.Split(delimiterChars)[3]))).ToList();
        _beacons = Input.Select(x => ((int.Parse(x.Split(delimiterChars)[5])), int.Parse(x.Split(delimiterChars)[7]))).ToList();
    }

    private int GetDistance((int x, int y) sensor, (int x, int y) beacon)
    {
        return Math.Abs(sensor.x - beacon.x) + Math.Abs(sensor.y - beacon.y);
    }

    public override void RunB()
    {

    }
}