using System;

namespace Yahtzee
{
    abstract class Game
    {
        // FIELDS
        public static int NumOfPlayers
        {get; set;} = 0;
        public static string[] scoreColumn1 = {"                   ",
        "|‾‾‾‾   UPPER   ‾‾‾‾|",
        "|____  SECTION  ____|",
        "                    |",
        " 1) Aces            |",
        " 2) Twos            |",
        " 3) Threes          |",
        " 4) Fours           |",
        " 5) Fives           |",
        " 6) Sixes           |",
        "—————————————————————",
        " Total Score        |",
        " Bonus (if >= 63)   |",
        " Upper Total        |",
        "                    |",
        "|‾‾‾‾   LOWER   ‾‾‾‾‾",
        "|____  SECTION  _____",
        "                    |",
        " 7) 3 of a Kind     |",
        " 8) 4 of a Kind     |",
        " 9) Full House      |",
        "10) SM. Straight (4)|",
        "11) LG. Straight (5)|",
        "12) Yahtzee         |",
        "13) Chance          |",
        "14) Yahtzee Bonus   |",
        "—————————————————————",
        " Lower Total        |",
        " Upper Total        |",
        "—————————————————————",
        "    GRAND TOTAL     |",
        "—————————————————————"};

        public static string[] scoreColumn2 = {"",
        "",
        };
        // METHODS
        // print scores for 1, 2, 3, or 4 players
        public static void PrintScores(Player p1, Player p2, Player p3, Player p4)
        {
            string[] p1Score = p1.ScoreArray();
            string[] p2Score = p2.ScoreArray();
            string[] p3Score = p3.ScoreArray();
            string[] p4Score = p4.ScoreArray();

            for (int i = 0; i < scoreColumn1.Length; i++)
            {
                switch (Game.NumOfPlayers)
                {
                    case 1:
                        Console.Write(scoreColumn1[i]);
                        //Console.Write(scoreColumn2[i]);
                        Console.WriteLine(p1Score[i]);
                        break;
                    case 2:
                        Console.Write(scoreColumn1[i]);
                        //Console.Write(scoreColumn2[i]);
                        Console.Write(p1Score[i]);
                        Console.WriteLine(p2Score[i]);
                        break;
                    case 3:
                        Console.Write(scoreColumn1[i]);
                        //Console.Write(scoreColumn2[i]);
                        Console.Write(p1Score[i]);
                        Console.Write(p2Score[i]);
                        Console.WriteLine(p3Score[i]);
                        break;
                    case 4:
                        Console.Write(scoreColumn1[i]);
                        //Console.Write(scoreColumn2[i]);
                        Console.Write(p1Score[i]);
                        Console.Write(p2Score[i]);
                        Console.Write(p3Score[i]);
                        Console.WriteLine(p4Score[i]);
                        break;
                }
            }
        }
        // Turn method overloads for multiple players
        public static void Turn(Player p1, Player p2, Player p3, Player p4)
        {
            Player[] pArray;
            switch (Game.NumOfPlayers)
            {
                case 1:
                    pArray = new Player[] {p1};
                    break;
                case 2:
                    pArray = new Player[] {p1, p2};
                    break;
                case 3:
                    pArray = new Player[] {p1, p2, p3};
                    break;
                case 4:
                    pArray = new Player[] {p1, p2, p3, p4};
                    break;
                default:
                    pArray = new Player[] {p1};
                    break; 
            }
            PrintScores(p1, p2, p3, p4);
            p1.Ds.PrintDice();
            Console.Write($"{p1.Name}, your turn!  Press enter to roll your dice.  ");
            Console.ReadLine();
            PrintScores(p1, p2, p3, p4);
            p1.Ds.RollAll();
            p1.Ds.PrintDice();
            if (IsYahtzee(p1))
            {
                return;
            }
            else
            {
                Console.WriteLine("Enter the assigned numbers of the dice you'd like to hold onto (1-5).  Separate by spaces.");
                Console.Write("Or, enter 0 to re-roll them all, or 6 to end your turn with these dice.  ");
                string input = Console.ReadLine();
                if (input == "6")
                {
                    p1.Ds.DiceToRoll = 0;
                    PrintScores(p1, p2, p3, p4);
                    Array.Sort(p1.Ds.DiceArray);
                    p1.Ds.PrintDice();
                }
                else
                {
                    p1.Ds.Hold(input);
                    p1.Ds.Roll();
                    PrintScores(p1, p2, p3, p4);
                    p1.Ds.PrintDice();
                    if (IsYahtzee(p1))
                    {
                        return;
                    }
                    else
                    {
                        Console.WriteLine("This time, make sure to include dice you held for this turn.");
                        Console.Write("(0 to re-roll them all; 6 to end your turn with these dice)  ");
                        input = Console.ReadLine();
                        if (input == "6")
                        {
                            p1.Ds.DiceToRoll = 0;
                            PrintScores(p1, p2, p3, p4);
                            Array.Sort(p1.Ds.DiceArray);
                            p1.Ds.PrintDice();
                        }
                        else
                        {
                            p1.Ds.Hold(input);
                            p1.Ds.Roll();
                            p1.Ds.DiceToRoll = 0;
                            Array.Sort(p1.Ds.DiceArray);
                            PrintScores(p1, p2, p3, p4);
                            p1.Ds.PrintDice();
                            if (IsYahtzee(p1))
                            {
                                return;
                            }
                        }
                    }
                }
            }
            Console.Write("Where would you like to use these dice?  Type the associated number from the score card.  ");
            int choice = Convert.ToInt32(Console.ReadLine());
            // this part makes sure they haven't filled that box already and scores it
            while (true)
            {
                if (choice >= 14)
                {
                    PrintScores(p1, p2, p3, p4);
                    p1.Ds.PrintDice();
                    Console.Write("You may not use the Yahtzee Bonus space unless you roll appropriately.  Please try again.  ");
                    choice = Convert.ToInt32(Console.ReadLine());
                    continue;
                }
                if (!p1.IsSpaceFree(choice))
                {
                    PrintScores(p1, p2, p3, p4);
                    p1.Ds.PrintDice();
                    Console.Write("You've already used that space.  Try again.  ");
                    choice = Convert.ToInt32(Console.ReadLine());
                    continue;
                }
                if (choice > 0 && choice < 7)
                {
                    p1.ScoreUpper(choice);
                    break;
                }
                if (choice > 6 && choice < 14)
                {
                    p1.ScoreLower(choice);
                    break;
                }
            }
            PrintScores(p1, p2, p3, p4);
            p1.Ds.PrintDice();
            Console.Write("Excellent.  Now press enter to end your turn.  ");
            Console.ReadLine();
            p1.Ds.ResetDice();
        }
        public static bool IsYahtzee(Player p)
        {
            if (p.HasDupes(5))
            {
                Console.Write("YAHTZEE!");
                switch (p.Yahtzee)
                {
                    case -1:
                        Console.Write("  Let's mark those 50 points!  (Press \"Enter\")  ");
                        Console.ReadLine();
                        p.ScoreLower(12);
                        break;
                    case 0:
                        Console.Write("  But you've already marked a zero.  :(  No bonus, but we'll still stick it where we can.  Press \"Enter.\" (Goes to upper box if possible, then empty lower box, then zero for empty upper).");
                        Console.ReadLine();
                        p.ScoreLower(14);
                        break;
                    case 50:
                        Console.Write("  Another one!  We'll mark another 100 points then try to fill the correct upper square.  Press \"Enter.\" (If that square isn't available, we'll fill a lower square, and if that isn't available, then zero for an empty upper.");
                        Console.ReadLine();
                        p.ScoreLower(14);
                        break;
                    default:
                        break;
                }
                p.PrintScores();
                p.Ds.PrintDice();
                Console.Write("Now press enter to finish your turn.  ");
                Console.ReadLine();
                return true;
            }
            return false;
        }
    }
}