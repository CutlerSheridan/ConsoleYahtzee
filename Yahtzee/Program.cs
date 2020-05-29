using System;

namespace Yahtzee
{
    class Program
    {
        static void Main(string[] args)
        {
            // here the game greets the player/s and determines how many players there are
            Console.Write("\nWelcome to Yahtzee!\n\nHow many players are there?  (Up to four)  ");
            int playerCount = Convert.ToInt32(Console.ReadLine());
            Player p1;
            Player p2;
            Player p3;
            Player p4;
            if (playerCount == 1)
            {
                Console.Write("\nGreat.  What's your name?  ");
            }
            else
            {
                Console.Write("\nGreat.  What is Player 1's name?  ");
            }
            p1 = new Player(Console.ReadLine());

            if (playerCount >= 2)
            {
                Console.Write("\nAnd Player 2's name?  ");
                p2 = new Player(Console.ReadLine());
                if (playerCount >= 3)
                {
                    Console.Write("\nPlayer 3's?  ");
                    p3 = new Player(Console.ReadLine());
                    if (playerCount >= 4)
                    {
                        Console.Write("\nPlayer 4's?  ");
                        p4 = new Player(Console.ReadLine());
                    }
                    else
                    {
                        p4 = new Player("");
                        Game.NumOfPlayers--;
                    }
                }
                else
                {
                    p3 = new Player("");
                    p4 = new Player("");
                    Game.NumOfPlayers = 2;
                }
            }
            else
            {
                p2 = new Player("");
                p3 = new Player("");
                p4 = new Player("");
                Game.NumOfPlayers = 1;
            }

            // this is where the game actually runs for the most part
            for (int i = 0; i < 13; i++)
            {
                Game.Turn(p1, p2, p3, p4);
            }

            // Calculate and declare the winner
            if (Game.NumOfPlayers == 1)
            {
                Console.WriteLine("\nIT'S OVER!  Well done.  \nHope you had a good time!\n");
            }
            else
            {
                int[] finalScoresArray = {p1.GrandTotal, p2.GrandTotal, p3.GrandTotal, p4.GrandTotal};
                Player[] playersArray = {p1, p2, p3, p4};
                Array.Sort(finalScoresArray, playersArray);
                Console.WriteLine($"\nIT'S OVER!  Good game everyone.  \nLooks like {playersArray[3].Name} is the winner!  Congratulations!\n");
            }
        }
    }
}
