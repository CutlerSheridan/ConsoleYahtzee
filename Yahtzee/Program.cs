using System;

namespace Yahtzee
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("\nWelcome to Yahtzee!\n\nHow many players are there?  ");
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
                Console.Write("\nWhat is Player 2's name?  ");
                p2 = new Player(Console.ReadLine());
                if (playerCount >= 3)
                {
                    Console.Write("\nWhat is Player 3's name?  ");
                    p3 = new Player(Console.ReadLine());
                    if (playerCount >= 4)
                    {
                        Console.Write("\nWhat is Player 4's name?  ");
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

            for (int i = 0; i < 13; i++)
            {
                Game.Turn(p1, p2, p3, p4);
            }
            
            switch (Game.NumOfPlayers)
            {
                case 1:
                    Console.WriteLine("\nIT'S OVER!  Well done!  Hope you had a good time.");
                    break;
                case 2:
                    Console.WriteLine($"\nIT'S OVER!  Good game everyone.  Looks like {(p1.GrandTotal > p2.GrandTotal ? p1.Name : p2.Name)} won.  Congrats!");
                    break;
                default:
                    break;

            }
            // put in congrats message
        }
    }
}
