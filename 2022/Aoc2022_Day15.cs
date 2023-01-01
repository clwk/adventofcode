public class Aoc2022_Day15 : BaseDay
{
    private const int MaxRow = 4000000;

    public Aoc2022_Day15(string inputFileName) : base(inputFileName)
    {
        ParseInput();
    }

    private List<(int x, int y)> _sensors = new();
    private List<(int x, int y)> _beacons = new();

    public override void RunA()
    {
        const int rowToCheck = 2000000;
        var cols = GetNoBeaconCols(rowToCheck);
        var existingBeaconsAtRow = _beacons.Where(p => p.y == rowToCheck).Distinct().Select(p => p.x).ToList();
        var noBeaconsDiff = cols.Except(existingBeaconsAtRow).ToList();
        System.Console.WriteLine($"Number of cols: {noBeaconsDiff.Count()}");
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

            if (searchDistance >= verticalDistance)
            {
                var fillCols = searchDistance - verticalDistance;
                for (var col = sensorCol - fillCols; col <= sensorCol + fillCols; col++)
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

    private static int GetDistance((int x, int y) sensor, (int x, int y) beacon)
    {
        return Math.Abs(sensor.x - beacon.x) + Math.Abs(sensor.y - beacon.y);
    }

    public override void RunB()
    {
        var intersections = GetIntersections();

        var rowsToTest = new List<int>();
        foreach (var intersection in intersections.Where(intersection => intersection.y > 0))
        {
            // rowsToTest.Add(intersection.y - 2);
            rowsToTest.Add(intersection.y - 1);
            rowsToTest.Add(intersection.y);
            rowsToTest.Add(intersection.y + 1);
            // rowsToTest.Add(intersection.y + 2);
        }

        rowsToTest = rowsToTest.Distinct().ToList();

        (int x, int y) distressPos = (0, 0);

        foreach (var row in rowsToTest)
        {
            var colsWithNoBeacon = GetNoBeaconCols(row);
            var existingBeaconsAtRow = _beacons.Where(p => p.y == row).Distinct().Select(p => p.x);
            var possibleCols = Enumerable.Range(1, MaxRow)
                .Except(colsWithNoBeacon)
                .Except(existingBeaconsAtRow);

            if (possibleCols.Count() == 1)
            {
                distressPos = (possibleCols.Single(), row);
                System.Console.WriteLine($"Distress position found: {distressPos}");
                break;
            }
            System.Console.WriteLine($"Row {row}, {possibleCols.Count()} possible beacons");
        }

        var tuningFrequency = 4000000 * (long)distressPos.x + distressPos.y;
        System.Console.WriteLine($"Tuning frequency {tuningFrequency}");
    }

    private List<(int x, int y)> GetIntersections()
    {
        var listOfRhombuses = _sensors.Select((t, i) => GetRhombus(t, _beacons[i])).ToList();

        List<(int x, int y)> intersections = new();
        foreach (var rhombus1 in listOfRhombuses)
        {
            foreach (var rhombus2 in listOfRhombuses)
            {
                // Should not compare when rhombuses are the same
                if (rhombus1 == rhombus2)
                    continue;
                var intersection = rhombus1.Intersect(rhombus2).ToList();

                // Skip intersections with multiple points - overlapping rhombuses
                if (intersection.Count > 4)
                    System.Console.WriteLine($"Intersection count: {intersection.Count}");
                else
                    intersections.AddRange(intersection);
            }
        }
        var uniqueIntersections = intersections.Distinct().ToList();
        return uniqueIntersections;
    }

    private static List<(int x, int y)> GetRhombus((int x, int y) sensor, (int x, int y) beacon)
    {
        var pointsOnRhombus = new List<(int x, int y)>();
        var distance = GetDistance(sensor, beacon);

        (int x, int y) line1Start = (sensor.x - distance, sensor.y);
        var pointsOnLine1 = GetPointsOnLine1or3(line1Start, distance);
        pointsOnRhombus.AddRange(pointsOnLine1);

        // line2
        (int x, int y) line2Start = (sensor.x, sensor.y - distance);
        var pointsOnLine2 = GetPointsOnLine2or4(line2Start, distance);
        pointsOnRhombus.AddRange(pointsOnLine2);

        // line3
        (int x, int y) line3Start = (sensor.x, sensor.y + distance);
        var pointsOnLine3 = GetPointsOnLine1or3(line3Start, distance);
        pointsOnRhombus.AddRange(pointsOnLine3);

        // line4
        (int x, int y) line4Start = (sensor.x - distance, sensor.y);
        var pointsOnLine4 = GetPointsOnLine2or4(line4Start, distance);
        pointsOnRhombus.AddRange(pointsOnLine4);

        var points = pointsOnRhombus.Distinct().ToList();
        return points;
    }

    private static IEnumerable<(int x, int y)> GetPointsOnLine1or3((int x, int y) lineStart, int distance)
    {
        var lineX = Enumerable.Range(lineStart.x, distance + 1);
        var lineY = Enumerable.Range(lineStart.y - distance, distance + 1).Reverse();
        var points = lineX.Zip(lineY, (x, y) => (x, y));
        var validPoints = points.Where(p => PointIsInInterval(p)).ToList();
        return validPoints;
    }

    private static bool PointIsInInterval((int x, int y) p)
    {
        return p.x >= 0 && p.y >= 0 && p.x <= MaxRow && p.y <= MaxRow;
    }

    private static IEnumerable<(int x, int y)> GetPointsOnLine2or4((int x, int y) lineStart, int distance)
    {
        var lineX = Enumerable.Range(lineStart.x, distance + 1);
        var lineY = Enumerable.Range(lineStart.y, distance + 1);
        var points = lineX.Zip(lineY, (x, y) => (x, y));
        var validPoints = points.Where(p => PointIsInInterval(p)).ToList();
        return validPoints;
    }
}