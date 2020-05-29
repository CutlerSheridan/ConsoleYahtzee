using System;

namespace Yahtzee
{
    class Player
    {
        //CONSTRUCTOR
        public Player(string name)
        {
            Game.NumOfPlayers++;
            PlayerNum = Game.NumOfPlayers;
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

        public int PlayerNum
        {get; private set;}

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
                    if (HasDupes(3) || HasDupes(4) || HasDupes(5))
                    {
                        for (int i = 0; i < Ds.DiceArray.Length; i++)
                        {
                            score += Ds.DiceArray[i].Num;
                        }
                    }
                    ThreeOfAKind = score;
                    break;
                case 8:
                    if (HasDupes(4) || HasDupes(5))
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
                case 14:
                    if (YahtzeeBonuses == -1)
                    {
                        YahtzeeBonuses = 0;
                    }
                    YahtzeeBonuses++;
                    if (Yahtzee != 0)
                    {
                        score = 100 * YahtzeeBonuses;
                        LowerTotal -= YahtzeeBonus;
                        YahtzeeBonus = score;
                    }
                    else
                    {
                        YahtzeeBonus = 0;
                    }
                    score = YahtzeeBonus;
                    YahtzeeBonusReplacement();
                    break;
            }
            LowerTotal += score;
        }

        public bool HasDupes(int howMany)
        {
            for (int i = 0; i < Ds.DiceArray.Length; i++)
            {
                int counter = 0;
                for (int j = 0; j < Ds.DiceArray.Length; j++)
                {
                    if (Ds.DiceArray[i].Num == Ds.DiceArray[j].Num)
                    {
                        counter++;
                    }
                }
                if (counter == howMany)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsFullHouse()
        {
            if (HasDupes(3) && HasDupes(2))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsStraight(int reqLength)
        {
            Array.Sort(Ds.DiceArray);
            int sequentials = 0;
            for (int i = 0; i < Ds.DiceArray.Length - 1; i++)
            {
                if (Ds.DiceArray[i].Num == Ds.DiceArray[i + 1].Num - 1)
                {
                    sequentials++;
                }
            }
            if (sequentials >= reqLength-1)
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
            int rollNum = Ds.DiceArray[0].Num;
            if (IsSpaceFree(rollNum))
            {
                ScoreUpper(rollNum);
                Console.WriteLine("Filling in appropriate upper box and Yahtzee bonus, if applicable.");
            }
            else
            {
                int[] lowerArray = {ThreeOfAKind, FourOfAKind, FullHouse, SmallStraight, LargeStraight, 0, Chance};
                if (Array.Exists(lowerArray, l => l == -1))
                {
                    Console.Write("Your appropriate upper spot has been used.  Which lower section box would you like to fill in?  ");
                    int input = Convert.ToInt32(Console.ReadLine());
                    while (input == 12 || input < 7 || input > 13)
                    {
                        Console.Write("Please select one of the empty lower section boxes.  ");
                        input = Convert.ToInt32(Console.ReadLine());
                    }
                    ScoreLower(input);
                }
                else
                {
                    Console.Write("The appropriate upper spot and all lower sections have been used.  Which upper section box would you like to fill in?  ");
                    int input = Convert.ToInt32(Console.ReadLine());
                    while (input < 1 || input < 6)
                    {
                        Console.Write("Please select one of the empty upper section boxes.  ");
                        input = Convert.ToInt32(Console.ReadLine());
                    }
                    ScoreUpper(input);
                }
            }
        }
        // checks if applicable space is free anywhere
        public bool IsSpaceFree(int n)
        {
            switch (n)
            {
                case 1:
                    if (Aces == -1)
                    {
                        return true;
                    }
                    break;
                case 2:
                    if (Twos == -1)
                    {
                        return true;
                    }
                    break;
                case 3:
                    if (Threes == -1)
                    {
                        return true;
                    }
                    break;
                case 4:
                    if (Fours == -1)
                    {
                        return true;
                    }
                    break;
                case 5:
                    if (Fives == -1)
                    {
                        return true;
                    }
                    break;
                case 6:
                    if (Sixes == -1)
                    {
                        return true;
                    }
                    break;
                case 7:
                    if (ThreeOfAKind == -1)
                    {
                        return true;
                    }
                    break;
                case 8:
                    if (FourOfAKind == -1)
                    {
                        return true;
                    }
                    break;
                case 9:
                    if (FullHouse == -1)
                    {
                        return true;
                    }
                    break;
                case 10:
                    if (SmallStraight == -1)
                    {
                        return true;
                    }
                    break;
                case 11:
                    if (LargeStraight == -1)
                    {
                        return true;
                    }
                    break;
                case 12:
                    if (Yahtzee == -1)
                    {
                        return true;
                    }
                    break;
                case 13:
                    if (Chance == -1)
                    {
                        return true;
                    }
                    break;
                default:
                    return false;
            }
            return false;
        }
        // this returns the player's score array to print alongside the score card
        public string[] ScoreArray()
        {
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
            $"‾{(Name.Length > 7 ? Name.Substring(0, 7) + "‾|" : $"{Name}{overscores.Substring(nameSpaceIndex)}")}",
            "_________|",
            "         |",
            $"{(Aces == -1 ? spaces : String.Format(" {0,-8}|", Aces))}",
            $"{(Twos == -1 ? spaces : String.Format(" {0,-8}|", Twos))}",
            $"{(Threes == -1 ? spaces : String.Format(" {0,-8}|", Threes))}",
            $"{(Fours == -1 ? spaces : String.Format(" {0,-8}|", Fours))}",
            $"{(Fives == -1 ? spaces : String.Format(" {0,-8}|", Fives))}",
            $"{(Sixes == -1 ? spaces : String.Format(" {0,-8}|", Sixes))}",
            $"{(PlayerNum < Game.NumOfPlayers ? "——————————" : "—————————|")}",
            String.Format(" {0,-8}|", InitUpperTotal),
            $"{(UpperBonus == 0 ? spaces : " 35      |")}",
            String.Format(" {0,-8}|", FinalUpperTotal),
            "         |",
            $"{(PlayerNum < Game.NumOfPlayers ? "‾‾‾‾‾‾‾‾‾‾" : "‾‾‾‾‾‾‾‾‾|")}",
            $"{(PlayerNum < Game.NumOfPlayers ? "__________" : "_________|")}",
            "         |",
            $"{(ThreeOfAKind == -1 ? spaces : String.Format(" {0,-8}|", ThreeOfAKind))}",
            $"{(FourOfAKind == -1 ? spaces : String.Format(" {0,-8}|", FourOfAKind))}",
            $"{(FullHouse == -1 ? spaces : String.Format(" {0,-8}|", FullHouse))}",
            $"{(SmallStraight == -1 ? spaces : String.Format(" {0,-8}|", SmallStraight))}",
            $"{(LargeStraight == -1 ? spaces : String.Format(" {0,-8}|", LargeStraight))}",
            $"{(Yahtzee == -1 ? spaces : String.Format(" {0,-8}|", Yahtzee))}",
            $"{(Chance == -1 ? spaces : String.Format(" {0,-8}|", Chance))}",
            $"{(YahtzeeBonuses == -1 ? spaces : $" ({YahtzeeBonuses}) {YahtzeeBonus} |")}",
            $"{(PlayerNum < Game.NumOfPlayers ? "——————————" : "—————————|")}",
            String.Format(" {0,-8}|", LowerTotal),
            String.Format(" {0,-8}|", FinalUpperTotal),
            $"{(PlayerNum < Game.NumOfPlayers ? "——————————" : "—————————|")}",
            String.Format(" {0,-8}|", GrandTotal),
            $"{(PlayerNum < Game.NumOfPlayers ? "——————————" : "————————— ")}"};

            return scoreColumn;
        }
    }
}