using System.Diagnostics;

namespace Gridlock_Solution
{
    public static class Icons
    {
        public static string DragonIcon = "🐉";
        public static string GoblinIcon = "👺";
        public static string OgreIcon = "👹";
        public static string TileIcon = "⬜️";
        public static string BattleIcon = "⚔️";
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Creature dragon = new Creature("Dragon", 2, 2, 7, Icons.DragonIcon, new List<Direction> { Direction.RIGHT, Direction.LEFT, Direction.DOWN });
            Creature goblin = new Creature("Goblin", 2, 3, 3, Icons.GoblinIcon, new List<Direction> { Direction.LEFT, Direction.RIGHT, Direction.UP });
            Creature ogre = new Creature("Ogre", 0, 0, 5, Icons.OgreIcon, new List<Direction> { Direction.RIGHT, Direction.DOWN, Direction.DOWN });
            List<Creature> creatures = new List<Creature> { dragon, goblin, ogre };
            Arena arena = new Arena(creatures);
            arena.DisplayInitialBoard();
            arena.StartSimulation(creatures);
            Console.WriteLine("Simulation complete.");
        }
    }

    public class Arena
    {
        private int sleepTime = 2500;
        private Creature?[,] grid = new Creature?[5, 5];
        private IList<Creature> _creatures;

        public Arena(IList<Creature> creatures)
        {
            _creatures = creatures;

            foreach (Creature creature in creatures)
            {
                grid[creature.Row, creature.Column] = creature;
            }
        }

        public bool IsCellEmpty(int row, int col)
        {
            Debug.WriteLine($"Class: {nameof(Arena)}, Method: {System.Reflection.MethodBase.GetCurrentMethod().Name}");
            return grid[row, col] == null;
        }

        public void MoveCreature(int fromRow, int fromCol, int toRow, int toCol)
        {
            Debug.WriteLine($"Class: {nameof(Arena)}, Method: {System.Reflection.MethodBase.GetCurrentMethod().Name}");
            if (IsCellEmpty(toRow, toCol))
            {
                grid[toRow, toCol] = grid[fromRow, fromCol]!;
                grid[fromRow, fromCol] = null;
            }
            else
            {
                Creature movingCreature = grid[fromRow, fromCol]!;
                Creature otherCreature = grid[toRow, toCol]!;
                movingCreature.IsInBattle = true;
                otherCreature.IsInBattle = true;
                int damage = Math.Abs(movingCreature.Power - otherCreature.Power);
                movingCreature.Points += damage;
                otherCreature.Points -= damage;
            }
        }

        public void DisplayInitialBoard()
        {
            Debug.WriteLine($"Class: {nameof(Arena)}, Method: {System.Reflection.MethodBase.GetCurrentMethod().Name}");
            PrintBoard("Initial board");
        }

        public void StartSimulation(IList<Creature> creatures)
        {
            Debug.WriteLine($"Class: {nameof(Arena)}, Method: {System.Reflection.MethodBase.GetCurrentMethod().Name}");
            int totalMoves = creatures.Max(c => c.Moves.Count);

            for (int i = 0; i < totalMoves; i++)
            {
                Console.Clear();

                foreach (Creature creature in creatures)
                {
                    if (creature.Moves.Count > i)
                    {
                        int newRow = creature.Row;
                        int newCol = creature.Column;

                        switch (creature.Moves[i])
                        {
                            case Direction.UP:
                                newRow--;
                                break;
                            case Direction.DOWN:
                                newRow++;
                                break;
                            case Direction.LEFT:
                                newCol--;
                                break;
                            case Direction.RIGHT:
                                newCol++;
                                break;
                        }

                        if (newRow >= 0 && newRow < 5 && newCol >= 0 && newCol < 5)
                        {
                                MoveCreature(creature.Row, creature.Column, newRow, newCol);
                        }
                    }
                }
                PrintBoard($"Move {i + 1}");
            }
        }

        public void PrintBoard(string title)
        {
            Debug.WriteLine($"Class: {nameof(Arena)}, Method: {System.Reflection.MethodBase.GetCurrentMethod().Name}");
            Console.WriteLine(title);
            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    bool creatureFound = false;

                    if (grid[row, col] != null)
                    {
                        Console.Write(grid[row, col]!.Icon + " ");
                        creatureFound = true;
                        continue;
                    }
                    else
                    {
                        Console.Write(Icons.TileIcon + " ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            foreach (Creature creature in _creatures)
            {
                creature.IsInBattle = false;
                Console.WriteLine($"{creature.Icon} - {creature.Points} points");
            }
            Thread.Sleep(sleepTime);
        }
    }

    public class Creature
    {
        private string _icon = string.Empty;
        public bool IsInBattle { get; set; } = false;
        public string Name { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public int Power { get; set; }
        public int Points { get; set; }
        public string Icon
        {
            get
            {
                if (IsInBattle) { return Icons.BattleIcon; }
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
            Debug.WriteLine($"Class: {nameof(Creature)}, Method: {System.Reflection.MethodBase.GetCurrentMethod().Name}");
            Console.WriteLine($"{Name} {Icon} at grid [{Row},{Column}] has {Power} power.");
        }
    }

    public enum Direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }
}
