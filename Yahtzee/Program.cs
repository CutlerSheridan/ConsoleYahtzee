using System;

namespace Yahtzee
{
    class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                Console.Write("\nWant to roll all the dice? (Type \"roll\"):  ");
                string input = Console.ReadLine();
                Console.WriteLine();
                Dice dice = new Dice();
                Player cutler = new Player("Cutler");
                string[] player1Score = cutler.ScoreArray();
                if (input.ToLower() == "roll")
                {
                    for (int i = 0; i < Game.scoreColumn1.Length; i++)
                    {
                        Console.Write(Game.scoreColumn1[i]);
                        Console.WriteLine(player1Score[i]);
                    }
                    cutler.Ds.RollAll();
                    cutler.Ds.PrintDice();
                    cutler.Ds.Hold();
                    cutler.Ds.Roll();
                    cutler.Ds.PrintDice();
                    cutler.Ds.Hold();
                    cutler.Ds.Roll();
                    cutler.Ds.PrintDice();
                    
                    Console.Write("Which numbers do you want to use in your score?  ");
                    int choice = Convert.ToInt32(Console.ReadLine());
                    cutler.ScoreUpper(choice);
                    player1Score = cutler.ScoreArray();
                    for (int i = 0; i < Game.scoreColumn1.Length; i++)
                    {
                        Console.Write(Game.scoreColumn1[i]);
                        Console.WriteLine(player1Score[i]);
                    }
                    cutler.Ds.RollAll();
                    cutler.Ds.PrintDice();
                    cutler.Ds.Hold();
                    cutler.Ds.Roll();
                    cutler.Ds.PrintDice();
                    cutler.Ds.Hold();
                    cutler.Ds.Roll();
                    cutler.Ds.PrintDice();
                    Console.Write("Which numbers do you want to use in your score?  ");
                    choice = Convert.ToInt32(Console.ReadLine());
                    cutler.ScoreUpper(choice);
                    player1Score = cutler.ScoreArray();
                    for (int i = 0; i < Game.scoreColumn1.Length; i++)
                    {
                        Console.Write(Game.scoreColumn1[i]);
                        Console.WriteLine(player1Score[i]);
                    } 
                }
                else {break;}

                // in reality we'll have a for loop where i < num of players
                // ending when last person has gone
                // Player will need property to keep track of num of turns
            }
        }
    }
}
