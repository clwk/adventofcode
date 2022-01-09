using System.Diagnostics;

public class Aoc2021_Day21 : BaseDay
{
    private int Player1Start { get; }
    private int Player2Start { get; }
    private int Player1LastPos { get; set; }
    private int Player2LastPos { get; set; }
    private int Player1Score { get; set; } = 0;
    private int Player2Score { get; set; } = 0;
    private int LastDice { get; set; }
    private int DiceRolls { get; set; } = 0;
    // Part B
    private long Player1Wins { get; set; } = 0;
    private long Player2Wins { get; set; } = 0;
    // private List<(int, bool)> Player1LastPoses { get; set; }// = new();
    private Dictionary<long, int> Player1LastPoses { get; set; }// = new();
    private List<(int, bool)> Player2LastPoses { get; set; }// = new();// (int, bool)[3];
    // private List<(int, bool)> Player1Scores { get; set; } = new();
    private Dictionary<long, int> Player1Scores { get; set; } = new();
    private List<(int, bool)> Player2Scores { get; set; } = new();

    public Aoc2021_Day21(string inputFileName) : base(inputFileName)
    {
        Player1Start = int.Parse(Input[0].Split(": ")[1]);
        Player2Start = int.Parse(Input[1].Split(": ")[1]);
        Player1LastPos = Player1Start;
        Player2LastPos = Player2Start;
        Player1LastPoses.Add(1, Player1LastPos);
        Player2LastPoses.Add(2, Player2LastPos);
        Player2LastPoses = new List<(int, bool)>() { (Player2LastPos, false) };
    }

    public override void RunA()
    {
        while (Player1Score < 1000 && Player2Score < 1000)
        {
            Player1LastPos = GetDiceRoll(Player1LastPos);
            Player1Score += GetScore(Player1LastPos);
            if (Player1Score < 1000)
            {
                Player2LastPos = GetDiceRoll(Player2LastPos);
                Player2Score += GetScore(Player2LastPos);
            }
            Debug.WriteLine(Player1Score);
            Debug.WriteLine(Player2Score);
        }
        var result = Math.Min(Player1Score, Player2Score) * LastDice;

        System.Console.WriteLine($"Result is {result}");
    }

    public override void RunB()
    {
        // Nåt med grafbibliotek ?

        // trädstruktur?
        // alt hur generera alla möjligheter?
        // skapa ny tråd för varje delning?
        // isf behöver skapa objekt

        // bara spara alla olika resultat efter x kast?
        // per spelare eller per kast?
        // spela de 3 olika för varje spelare?

        // Hur veta vilken spelare som fick 21 först i just den omgången?
        // Hur para ihop omgångar från spelare1/2 ?
        long gameIndex = 0;
        long lastGameIndex = 0;
        Dictionary<long, int>

        while (Player1Score < 1000 && Player2Score < 1000)
        {
            var tempPlayer1Poses = new List<(int, bool)>();
            var tempPlayer1Scores = new List<(int, bool)>();
            var tempPlayer2Poses = new List<(int, bool)>();
            var tempPlayer2Scores = new List<(int, bool)>();

            // index som identifierar ett spel ?
            for (int pos2 = 0; pos2 < Player2LastPoses.Count; pos2++)
            {
                var pos1Idx = 0;

                for (int dice = 1; dice <= 3; dice++)
                {
                    var newPos = (Player1LastPoses[pos1Idx].Item1 + dice) % 10;
                    var newScore = GetScore(newPos);

                    (int, bool) newPosT = (newPos, false);
                    (int, bool) newScoreT = (newScore, false);

                    if (newScore > 21)
                    {
                        newPosT.Item2 = true;
                        newScoreT.Item2 = true;
                    }
                    tempPlayer1Poses.Add(newPos);
                    tempPlayer1Scores.Add(GetScore(newPos));
                }

                pos1Idx++;
            }
            Player1LastPos = GetDiceRollB(Player1LastPos);
            Player1Score += GetScore(Player1LastPos);
            if (Player1Score < 1000)
            {
                Player2LastPos = GetDiceRoll(Player2LastPos);
                Player2Score += GetScore(Player2LastPos);
            }

            Player1LastPoses = tempPlayer1Poses;
            Player1Scores = tempPlayer1Scores;
            Player2LastPoses = tempPlayer2Poses;
            Player2Scores = tempPlayer2Scores;


            Debug.WriteLine(Player1Scores.Take(10).Select(x => x.ToString()));
            Debug.WriteLine(Player2Scores.Take(10).Select(x => x.ToString()));
        }

    }

    public int GetDiceRollB(int lastPos)
    {
        LastDice += 1;
        var diceResult = (LastDice + lastPos) % 10;
        if (LastDice == 3) LastDice = 0;
        return diceResult;
    }

    public int GetDiceRoll(int lastPos)
    {
        var diceResult = (Enumerable.Range(LastDice + 1, 3).Sum() + lastPos) % 10;
        LastDice += 3;
        DiceRolls++;
        return diceResult;
    }

    public int GetScore(int position)
    {
        if (position == 0)
            return 10;
        else
            return position;
    }


}