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

    public Aoc2021_Day21(string inputFileName) : base(inputFileName)
    {
        Player1Start = int.Parse(Input[0].Split(": ")[1]);
        Player2Start = int.Parse(Input[1].Split(": ")[1]);
        Player1LastPos = Player1Start;
        Player2LastPos = Player2Start;
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

    public override void RunB()
    {

    }
}