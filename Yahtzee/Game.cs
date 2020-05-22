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
        public void Greet()
        {
            Console.Write("Press enter to roll!");
            string input = Console.ReadLine();
            if (input == "q")
            {
                return;
            }
        }
        // overload this for 2, 3, and 4 players
        public void PrintScores(Player p1)
        {
            string[] p1Score = p1.ScoreArray();
            for (int i = 0; i < scoreColumn1.Length; i++)
            {
                Console.Write(scoreColumn1[i]);
                Console.Write(scoreColumn2[i]);
                Console.WriteLine(p1Score[i]);
            }
        }
    }
}