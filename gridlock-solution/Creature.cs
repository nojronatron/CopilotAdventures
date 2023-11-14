namespace Gridlock_Solution
{
    public class Creature
    {
        public bool IsInBattle { get; set; } = false;
        public bool IsMoving { get; set; } = false;
        private string _icon = string.Empty;
        public string Name { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public int Power { get; set; }
        public int Points { get; set; }
        public string Icon
        {
            get
            {
                if (IsInBattle) { return ProgramDefaults.BattleIcon; }
                else { return _icon; }
            }
            set
            {
                _icon = value;
            }
        }
        public List<Direction> Moves { get; set; }

        public Creature(string name, int row, int col, int power, string icon, List<Direction> moves)
        {
            Name = name;
            Row = row;
            Column = col;
            Power = power;
            Points = 0;
            Icon = icon;
            Moves = moves;
        }

        public void DisplayCreature()
        {
            Console.WriteLine($"{Name} {Icon} at grid [{Row},{Column}] has {Power} power.");
        }

        public override string ToString()
        {
            return $"{Name} {Icon} (R:{Row},C:{Column}) Po:{Power} Pts:{Points} M? {IsMoving} B? {IsInBattle}";
        }
    }
}
