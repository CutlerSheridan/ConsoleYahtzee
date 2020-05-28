using System;

namespace Yahtzee
{
    class Dice
    {
        // CONSTRUCTOR
        public Dice()
        {
            rand = new Random();
            DiceArray = new Die[] {new Die(1), new Die(1), new Die(1), new Die(1), new Die(1)};
            DiceToRoll = 5;
            Turn = 0;
        }

        // FIELDS
        public Die[] DiceArray
        {get; set;}

        public int DiceToRoll
        {get; set;}

        private Random rand;
        // this is simply to keep track of how to print the dice
        private int Turn
        {get; set;}

        // METHODS
        public void RollAll()
        {
            DiceToRoll = 5;
            Turn = 1;
            foreach(Die d in DiceArray)
            {
                d.RollOne(rand.Next(1, 7));
            }
        }

        public void Roll()
        {
            for (int i = 0; i < DiceToRoll; i++)
            {
                DiceArray[i].RollOne(rand.Next(1, 7));
            }
            Turn++;
        }

        public void PrintDice()
        {
            for (int i = 0; i < DiceArray[0].Pips.Length; i++)
            {
                for (int n = 0; n < DiceArray.Length; n++)
                {
                    Console.Write(DiceArray[n].Pips[i]);
                    if (DiceToRoll < 5)
                    {
                        if (n == DiceToRoll - 1)
                        {
                            Console.Write("   #   ");
                        }
                    }
                }
                Console.WriteLine();
            }
            for (int i = 0; i < DiceArray.Length; i++)
            {
                if (Turn > 0 && Turn < 3)
                {
                    Console.Write($"      ({i + 1})      ");
                }
                else {Console.Write("               ");}

                if (DiceToRoll == i + 1 && DiceToRoll != 5)
                {
                    Console.Write("   #   ");
                }
            }
            Console.WriteLine("\n");
        }

        public void ResetDice()
        {
            foreach (Die d in DiceArray)
            {
                d.Num = 1;
            }
        }

        public void Hold(string selection)
        {
            string input = selection;

            // this whole while takes input and makes sure it doesn't fuck up
            while (true)
            {
                string[] inputTestStrings = input.Split(' ');
                int[] inputTestInts = new int[inputTestStrings.Length];
                for (int i = 0; i < inputTestStrings.Length; i++)
                {
                    inputTestInts[i] = Convert.ToInt32(inputTestStrings[i]);
                }

                if (Array.Exists(inputTestInts, x => x > 6 || x < 0))
                {
                    Console.Write("\nPlease only pick numbers 0 - 6.  ");
                    input = Console.ReadLine();
                }
                else {break;}
            }
            if (input == "0")
            {
                DiceToRoll = 5;
                return;
            }
            if (input == "6")
            {
                DiceToRoll = 0;
                return;
            }
            // put numbers into int array
            string[] keepersString = input.Split(' ');
            int diceToHold = keepersString.Length;
            int[] keepers = new int[diceToHold];
            for (int i = 0; i < diceToHold; i++)
            {
                // -1 here so I don't have to +1 to all the indices later since
                // the numbers entered will start at 1 rather than 0
                keepers[i] = Convert.ToInt32(keepersString[i]) - 1;
            }


            DiceToRoll = 5 - diceToHold;
            Die[] dArrayCopy = new Die[] {new Die(), new Die(), new Die(), new Die(), new Die()};

            // this is to keep track of where we need to start putting the held nums
            int rightStart = 5 - diceToHold;
            int leftStart = 0;

            // first this will copy the ones we're holding to the appropriate spots in
            // the copy
            for (int i = 0; i < 5; i++)
            {
                if (Array.Exists(keepers, k => k == i))
                {
                    dArrayCopy[rightStart].Num = DiceArray[i].Num;
                    rightStart++;
                }
            }
            // then this will shift everything to where it needs to go
            for (int i = 0; i < 5; i++)
            {
                if(!(Array.Exists(keepers, k => k == i)))
                {
                    dArrayCopy[leftStart].Num = DiceArray[i].Num;
                    leftStart++;
                }
            }

            Array.Sort(dArrayCopy, DiceToRoll, diceToHold);

            // put result back into original Dice array
            for (int i = 0; i < 5; i++)
            {
                DiceArray[i] = dArrayCopy[i];
            }
        }
    }
}