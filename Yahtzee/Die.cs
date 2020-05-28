using System;

namespace Yahtzee
{
    class Die : IComparable<Die>
    {
        // CONSTRUCTOR
        public Die(int num = 0)
        {
            Num = num;
        }

        // FIELDS
        private int num;
        public int Num
        {
            get {return num;}
            set
            {
                num = value;
                switch (value)
                {
                    case 1:
                        Pips = new string[] {
                        "               ", 
                        "   —————————   ", 
                        "  |         |  ", 
                        "  |    O    |  ", 
                        "  |         |  ", 
                        "   —————————   "};
                        break;
                    case 2:
                        Pips = new string[] {
                        "               ", 
                        "   —————————   ", 
                        "  | O       |  ", 
                        "  |         |  ", 
                        "  |       O |  ", 
                        "   —————————   "};
                        break;
                    case 3:
                        Pips = new string[] {
                        "               ", 
                        "   —————————   ", 
                        "  | O       |  ", 
                        "  |    O    |  ", 
                        "  |       O |  ", 
                        "   —————————   "};
                        break;
                    case 4:
                        Pips = new string[] {
                        "               ", 
                        "   —————————   ", 
                        "  |  O   O  |  ", 
                        "  |         |  ", 
                        "  |  O   O  |  ", 
                        "   —————————   "};
                        break;
                    case 5:
                        Pips = new string[] {
                        "               ", 
                        "   —————————   ", 
                        "  | O     O |  ", 
                        "  |    O    |  ", 
                        "  | O     O |  ", 
                        "   —————————   "};
                        break;
                    case 6:
                        Pips = new string[] {
                        "               ", 
                        "   —————————   ", 
                        "  |  O   O  |  ", 
                        "  |  O   O  |  ", 
                        "  |  O   O  |  ", 
                        "   —————————   "};
                        break;
                    default:
                        break;
                }
            }
        }

        public string[] Pips
        {get; set;}

        // METHODS
        
        public void RollOne(int i)
        {
            Num = i;
        }

        public int CompareTo(Die d)
        {
            return Num.CompareTo(d.Num);
        }
    }
}