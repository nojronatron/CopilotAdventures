namespace Gridlock_Solution
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Creature dragon = new Creature("Dragon", 2, 2, 7, ProgramDefaults.DragonIcon, new List<Direction> { Direction.RIGHT, Direction.LEFT, Direction.DOWN });
            Creature goblin = new Creature("Goblin", 2, 3, 3, ProgramDefaults.GoblinIcon, new List<Direction> { Direction.LEFT, Direction.RIGHT, Direction.UP });
            Creature ogre = new Creature("Ogre", 0, 0, 5, ProgramDefaults.OgreIcon, new List<Direction> { Direction.RIGHT, Direction.DOWN, Direction.DOWN });
            List<Creature> creatures = new List<Creature> { dragon, goblin, ogre };
            Arena arena = new Arena(creatures);
            arena.InitSimulation(creatures);
            arena.StartSimulation();
            Console.WriteLine("Simulation complete.");
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
