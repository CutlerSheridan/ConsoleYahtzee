using System;

namespace Yahtzee
{
    class Player
    {
        //CONSTRUCTOR
        public Player(string name)
        {
            Game.NumOfPlayers++;
            this.Name = name;
            InitUpperTotal = 0;
            UpperBonus = 0;
            FinalUpperTotal = 0;
            YahtzeeBonus = 0;
            LowerTotal = 0;
            GrandTotal = 0;
            Ds = new Dice();
        }

        //FIELDS
        public Dice Ds
        {get; set;}
        public string Name
        {get; set;}

        // Everything for upper section
        public int Aces
        {get; set;} = -1;
        public int Twos
        {get; set;} = -1;
        public int Threes
        {get; set;} = -1;
        public int Fours
        {get; set;} = -1;
        public int Fives
        {get; set;} = -1;
        public int Sixes
        {get; set;} = -1;
        private int initUpperTotal;
        public int InitUpperTotal
        {
            get {return initUpperTotal;}
            set
            {
                FinalUpperTotal -= initUpperTotal;
                FinalUpperTotal += value;
                initUpperTotal = value;
                if (value >= 63)
                {
                    UpperBonus = 35;
                }
            }
        }
        private int upperBonus;
        public int UpperBonus
        {
            get {return upperBonus;}
            set
            {
                if (upperBonus == 0)
                {
                    FinalUpperTotal += value;
                }
                upperBonus = value;
            }
        }
        private int finalUpperTotal;
        public int FinalUpperTotal
        {
            get {return finalUpperTotal;}
            set
            {
                GrandTotal -= finalUpperTotal;
                GrandTotal += value;
                finalUpperTotal = value;
            }
        }

        // Everything for lower section
        public int ThreeOfAKind
        {get; set;} = -1;
        public int FourOfAKind
        {get; set;} = -1;
        public int FullHouse
        {get; set;} = -1;
        public int SmallStraight
        {get; set;} = -1;
        public int LargeStraight
        {get; set;} = -1;
        public int Yahtzee
        {get; set;} = -1;
        public int Chance
        {get; set;} = -1;
        // this one keeps track of how many bonuses there have been
        public int YahtzeeBonuses
        {get; set;} = -1;
        // this one keeps track of actual score
        public int YahtzeeBonus
        {get; set;}

        private int lowerTotal;
        public int LowerTotal
        {
            get {return lowerTotal;}
            set
            {
                GrandTotal -= lowerTotal;
                GrandTotal += value;
                lowerTotal = value;
            }
        }
        public int GrandTotal
        {get; set;}

        // METHODS
        // in the "turn" method there will be a loop to see if they're using something
        // in the upper or lower section
        // that method will also make sure the input is valid
        public void ScoreUpper(int choice)
        {
            int score = 0;
            foreach (Die d in Ds.DiceArray)
            {
                if (d.Num == choice)
                {
                    score += choice;
                }
            }
            // now we add that to the appropriate location
            switch (choice)
            {
                case 1:
                    Aces = score;
                    break;
                case 2:
                    Twos = score;
                    break;
                case 3:
                    Threes = score;
                    break;
                case 4:
                    Fours = score;
                    break;
                case 5:
                    Fives = score;
                    break;
                case 6:
                    Sixes = score;
                    break;
                default:
                    break;
            }
            // here we add it to the upper score
            InitUpperTotal += score;
        }
        public void ScoreLower(int choice)
        {
            int score = 0;
            switch (choice)
            {
                case 7:
                    if (HasDupes(3))
                    {
                        for (int i = 0; i < Ds.DiceArray.Length; i++)
                        {
                            score += Ds.DiceArray[i].Num;
                        }
                    }
                    ThreeOfAKind = score;
                    break;
                case 8:
                    if (HasDupes(4))
                    {
                        for (int i = 0; i < Ds.DiceArray.Length; i++)
                        {
                            score += Ds.DiceArray[i].Num;
                        }
                    }
                    FourOfAKind = score;
                    break;
                case 9:
                    if (IsFullHouse())
                    {
                        score = 25;
                    }
                    FullHouse = score;
                    break;
                case 10:
                    if (IsStraight(4))
                    {
                        score = 30;
                    }
                    SmallStraight = score;
                    break;
                case 11:
                    if (IsStraight(5))
                    {
                        score = 40;
                    }
                    LargeStraight = score;
                    break;
                case 12:
                    if (HasDupes(5))
                    {
                        score = 50;
                    }
                    Yahtzee = score;
                    break;
                case 13:
                    for (int i = 0; i < Ds.DiceArray.Length; i++)
                    {
                        score += Ds.DiceArray[i].Num;
                    }
                    Chance = score;
                    break;
                // for 14, make sure the "turn" method checks if it's a valid bonus
                // since you can't put 0 for this category
                case 14:
                    if (YahtzeeBonuses == -1)
                    {
                        YahtzeeBonuses = 0;
                    }
                    YahtzeeBonuses++;
                    score = 100 * YahtzeeBonuses;
                    LowerTotal -= YahtzeeBonus;
                    YahtzeeBonus = score;
                    break;
            }
            LowerTotal += score;
        }

        public bool HasDupes(int howMany)
        {
            for (int i = 0; i < Ds.DiceArray.Length; i++)
            {
                int counter = 1;
                for (int j = i + 1; j < Ds.DiceArray.Length; j++)
                {
                    if (Ds.DiceArray[i].Num == Ds.DiceArray[j].Num)
                    {
                        counter++;
                    }
                }
                if (counter >= howMany)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsFullHouse()
        {
            bool doubles = false;
            bool triples = false;
            for (int i = 0; i < Ds.DiceArray.Length; i++)
            {
                int counter = 1;
                for (int j = i + 1; j < Ds.DiceArray.Length; j++)
                {
                    if (Ds.DiceArray[i].Num == Ds.DiceArray[j].Num)
                    {
                        counter++;
                    }
                }
                if (counter == 2)
                {
                    doubles = true;
                }
                if (counter == 3)
                {
                    triples = true;
                }
            }
            if (doubles && triples)
            {
                return true;
            }
            return false;
        }
        public bool IsStraight(int reqLength)
        {
            Array.Sort(Ds.DiceArray);
            int sequentials = 0;
            for (int i = 0; i < Ds.DiceArray.Length; i++)
            {
                if (Ds.DiceArray[i].Num == Ds.DiceArray[i + 1].Num - 1)
                {
                    sequentials++;
                }
            }
            if (sequentials >= reqLength)
            {
                return true;
            }
            return false;
        }
        public void YahtzeeBonusReplacement()
        {
            /* For this--if possible, they have to fill in the corresponding
            upper section box with the total of all five dice.
            If that is not possible, fill in a lower section box, scoring as usual.
            If that is also impossible, enter a zero in any upper section box */
            // this might require implementing an upperArray and lowerArray of scores
            // to check for -1 (aka available boxes), which would mean undoing all the
            // auto-implemented properties
        }
        // this prints out the player's score array to print alongside the score card
        public string[] ScoreArray()
        {
            // we have to stringify these for certain scores to check length later
            int[] intScores = {-1, -1, -1, Aces, Twos, Threes, Fours, Fives, Sixes, -1, InitUpperTotal, UpperBonus, FinalUpperTotal, -1, -1, -1, -1, ThreeOfAKind, FourOfAKind, FullHouse, SmallStraight, LargeStraight, Yahtzee, Chance, YahtzeeBonus, -1, LowerTotal, FinalUpperTotal, -1, GrandTotal, -1};
            string[] stringScores = new string[intScores.Length];
            for (int i = 0; i < intScores.Length; i++)
            {
                stringScores[i] = Convert.ToString(intScores[i]);
            }
            // to use for substrings of spaces for different numbers of digits
            string spaces = "         |";
            string overscores = "‾‾‾‾‾‾‾‾‾|";
            int nameSpaceIndex;
            if (Name.Length > 7)
            {
                nameSpaceIndex = 8;
            }
            else
            {
                nameSpaceIndex = Name.Length + 1;
            }

            string[] scoreColumn = {"           ",
            $"‾{(Name.Length > 7 ? Name.Substring(0, 7) : Name)}{overscores.Substring(nameSpaceIndex)}",
            "_________|",
            "         |",
            $"{(Aces == -1 ? spaces : $" {Aces}       |")}",
            $"{(Twos == -1 ? spaces : $" {Twos}{spaces.Substring(stringScores[5].Length + 1)}")}",
            $"{(Threes == -1 ? spaces : $" {Threes}{spaces.Substring(stringScores[6].Length + 1)}")}",
            $"{(Fours == -1 ? spaces : $" {Fours}{spaces.Substring(stringScores[7].Length + 1)}")}",
            $"{(Fives == -1 ? spaces : $" {Fives}{spaces.Substring(stringScores[8].Length + 1)}")}",
            $"{(Sixes == -1 ? spaces : $" {Sixes}{spaces.Substring(stringScores[9].Length + 1)}")}",
            "—————————|",
            $" {InitUpperTotal}{spaces.Substring(stringScores[11].Length + 1)}",
            $"{(UpperBonus == 0 ? spaces : " 35      |")}",
            $" {FinalUpperTotal}{spaces.Substring(stringScores[13].Length + 1)}",
            "         |",
            "‾‾‾‾‾‾‾‾‾|",
            "_________|",
            "         |",
            $"{(ThreeOfAKind == -1 ? spaces : $" {ThreeOfAKind}{spaces.Substring(stringScores[18].Length + 1)}")}",
            $"{(FourOfAKind == -1 ? spaces : $" {FourOfAKind}{spaces.Substring(stringScores[19].Length + 1)}")}",
            $"{(FullHouse == -1 ? spaces : $" {FullHouse}{spaces.Substring(stringScores[20].Length + 1)}")}",
            $"{(SmallStraight == -1 ? spaces : $" {SmallStraight}{spaces.Substring(stringScores[21].Length + 1)}")}",
            $"{(LargeStraight == -1 ? spaces : $" {LargeStraight}{spaces.Substring(stringScores[22].Length + 1)}")}",
            $"{(Yahtzee == -1 ? spaces : $" {Yahtzee}{spaces.Substring(stringScores[23].Length + 1)}")}",
            $"{(Chance == -1 ? spaces : $" {Chance}{spaces.Substring(stringScores[24].Length + 1)}")}",
            $"{(YahtzeeBonuses == -1 ? spaces : $" ({YahtzeeBonuses}) {YahtzeeBonus} |")}",
            "—————————|",
            $" {LowerTotal}{spaces.Substring(stringScores[27].Length + 1)}",
            $" {FinalUpperTotal}{spaces.Substring(stringScores[28].Length + 1)}",
            "—————————|",
            $" {GrandTotal}{spaces.Substring(stringScores[30].Length + 1)}",
            "—————————|"};

            return scoreColumn;
        }
        public void PrintScores()
        {
            Console.WriteLine();
        }
    }
}