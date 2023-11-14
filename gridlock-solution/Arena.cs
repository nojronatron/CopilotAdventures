using System.Diagnostics;

namespace Gridlock_Solution
{
    public class Arena
    {
        private Creature?[,] grid = new Creature?[5, 5];

        public Arena(IList<Creature> creatures)
        {
            foreach (Creature creature in creatures)
            {
                grid[creature.Row, creature.Column] = creature;
            }
        }

        public bool IsCellEmpty(int row, int col)
        {
            return grid[row, col] == null;
        }

        public void MoveCreature(int fromRow, int fromCol, int toRow, int toCol)
        {
            if (fromRow == toRow && fromCol == toCol) 
            {
                return; 
            }

            Creature movingCreature = grid[fromRow, fromCol]!;
            movingCreature.IsMoving = true;

            if (IsCellEmpty(toRow, toCol))
            {
                movingCreature.Row = toRow;
                movingCreature.Column = toCol;
                grid[toRow, toCol] = movingCreature;
                grid[fromRow, fromCol] = null;
            }
            else
            {
                Creature otherCreature = grid[toRow, toCol]!;
                movingCreature.IsInBattle = true;
                otherCreature.IsInBattle = true;
                otherCreature.IsMoving = true;
                int damage = Math.Abs(movingCreature.Power - otherCreature.Power);

                if (movingCreature.Power > otherCreature.Power)
                {
                    movingCreature.Points += damage;
                    otherCreature.Points -= damage;
                }
                else
                {
                    otherCreature.Points += damage;
                    movingCreature.Points -= damage;
                }

                grid[toRow, toCol] = otherCreature;
                grid[fromRow, fromCol] = movingCreature;
            }

        }

        public void DisplayInitialBoard()
        {
            Console.Clear();
            PrintBoard("Initial board");
        }

        public void InitSimulation(IList<Creature> creatures)
        {
            foreach(Creature creature in creatures)
            {
                grid[creature.Row, creature.Column] = creature;
            }
        }

        public void StartSimulation()
        {
            if (grid.Cast<Creature?>().Where(c => c != null).Count() < 2)
            {
                Console.WriteLine("There are not enough creatures to start the simulation.");
                return;
            }

            DisplayInitialBoard();
            int totalMoves = MaxMoves();

            for (int i = 0; i < totalMoves; i++)
            {
                for (int row = 0; row < 5; row++)
                {
                    for (int col = 0; col < 5; col++)
                    {
                        if (grid[row, col] != null)
                        {
                            Creature creature = grid[row, col]!;

                            if (!creature.IsMoving && creature.Moves.Count > i)
                            {
                                int newRow = creature.Row;
                                int newCol = creature.Column;

                                switch (creature.Moves[i])
                                {
                                    case Direction.UP:
                                        if (row > 0)
                                        {
                                            newRow--;
                                            MoveCreature(row, col, newRow, newCol);
                                        }
                                        break;
                                    case Direction.DOWN:
                                        if (row < 4)
                                        {
                                            newRow++;
                                            MoveCreature(row, col, newRow, newCol);
                                        }
                                        break;
                                    case Direction.LEFT:
                                        if (col > 0)
                                        {
                                            newCol--;
                                            MoveCreature(row, col, newRow, newCol);
                                        }
                                        break;
                                    case Direction.RIGHT:
                                        if (col < 4)
                                        {
                                            newCol++;
                                            MoveCreature(row, col, newRow, newCol);
                                        }
                                        break;
                                }
                            }
                        }
                    }
                }

                PrintBoard($"Move {i + 1}");
                ResetCreatures();
            }

            PrintBoard("Final board");
        }

        private void ResetCreatures()
        {
            grid.Cast<Creature?>().Where(cre => cre != null)
                .ToList()
                .ForEach(cre =>
                {
                    cre!.IsInBattle = false;
                    cre!.IsMoving = false;
                });

            Debug.WriteLine("ResetCreatures method completed.");
        }


        private int MaxMoves()
        {
            return (from c in grid.Cast<Creature?>()
                    where c != null
                    select c!.Moves.Count).Max();
        }

        public void PrintBoard(string title)
        {
            Console.Clear();
            Console.WriteLine(title);
            List<Creature> creatures = new List<Creature>();

            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    if (grid[row, col] != null)
                    {
                        Console.Write(grid[row, col]!.Icon);
                        creatures.Add(grid[row, col]!);
                        continue;
                    }
                    else
                    {
                        Console.Write(ProgramDefaults.TileIcon);
                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine();
            
            foreach (Creature creature in creatures)
            {
                creature.IsInBattle = false;
                Console.WriteLine($"{creature.Icon} has {creature.Points} points");
            }
            
            Thread.Sleep(ProgramDefaults.SleepTime);
        }
    }
}
