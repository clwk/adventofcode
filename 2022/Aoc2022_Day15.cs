public class Aoc2022_Day15 : BaseDay
{
    public Aoc2022_Day15(string inputFileName) : base(inputFileName)
    {
        ParseInput();
    }

    private List<(int x, int y)> _sensors = new();
    private List<(int x, int y)> _beacons = new();
    private Dictionary<int, HashSet<int>> NoBeaconColsB = new();
    private const int rowToCheck = 2000000;

    public override void RunA()
    {
        var cols = GetNoBeaconCols(rowToCheck);
        System.Console.WriteLine($"Number of cols: {cols.Count}");
    }

    private HashSet<int> GetNoBeaconCols(int checkRow)
    {
        HashSet<int> noBeaconCols = new();
        for (int i = 0; i < _sensors.Count; i++)
        {
            var searchDistance = GetDistance(_sensors[i], _beacons[i]);
            var sensorRow = _sensors[i].y;
            var sensorCol = _sensors[i].x;

            var verticalDistance = Math.Abs(sensorRow - checkRow);
            // if (searchDistance == verticalDistance)
            // System.Console.WriteLine($"Same: {searchDistance}");
            if (searchDistance >= verticalDistance)
            {
                var fillCols = searchDistance - verticalDistance;
                for (int col = sensorCol - fillCols; col <= sensorCol + fillCols; col++)
                {
                    noBeaconCols.Add(col);
                }
            }
        }
        return noBeaconCols;
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
        (int x, int y) distressPos = (0, 0);
        int row = 0;
        const int maxRow = 20;
        while (distressPos == (0, 0) && row <= maxRow)
        {
            var colsWithNoBeacon = GetNoBeaconCols(row);
            var possibleCols = Enumerable.Range(1, 20).Except(colsWithNoBeacon);
            if (possibleCols.Count() == 1)
                distressPos = (possibleCols.Single(), row);
            System.Console.WriteLine($"Row {row}, {possibleCols.Count()} possible beacons");
            row++;
        }

        var tuningFrequency = 4000000 * distressPos.x + distressPos.y;
        System.Console.WriteLine($"Tuning frequency {tuningFrequency}");
    }
}