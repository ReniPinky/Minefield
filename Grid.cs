namespace Minefield
{
    public class Grid
    {
        readonly Space[,] grid = new Space[8, 8];

        public Grid()
        {
            InitialiseGrid();
        }

        public void PrintGrid(int playerY, int playerX)
        {
            int rowLength = grid.GetLength(0);
            int colLength = grid.GetLength(1);

            Console.WriteLine("Current Grid:");

            for (int y = 0; y < rowLength; y++)
            {
                Console.Write("|");
                for (int x = 0; x < colLength; x++)
                {
                    if (playerY == y && playerX == x)
                    {
                        Console.Write("P");
                        Console.Write("|");
                        continue;
                    }

                    Console.Write(string.Format("{0}", PrintSpace(x, y)));
                    Console.Write("|");
                }
                Console.Write(Environment.NewLine);
                Console.Write("-----------------");
                Console.Write(Environment.NewLine);
            }
        }

        public void CheckSpace(int y, int x, Action spaceBlowUpCallback)
        {
            grid[y, x].MoveIntoSpace(spaceBlowUpCallback);
        }

        public void InitialiseGrid()
        {
            int rowLength = grid.GetLength(0);
            int colLength = grid.GetLength(1);
            for (int y = 0; y < rowLength; y++)
            {
                for (int x = 0; x < colLength; x++)
                {
                    grid[y, x] = new Space();
                }
            }
            grid[7, 0].ClearInitialSpace();
        }

        private string PrintSpace(int x, int y)
        {
            return grid[y, x].Status switch
            {
                0 => "?",
                (SpaceStatus)1 => "N", //TODO change this back to ?
                (SpaceStatus)2 => "/",
                (SpaceStatus)3 => "M",
                _ => "?",
            };
        }

        public List<(int, int)> FindBestPath()
        {
            var gridCopy = grid;
            var intGrid = new int[8, 8];

            int rowLength = grid.GetLength(0);
            int colLength = grid.GetLength(1);
            for (int y = 0; y < rowLength; y++)
            {
                for (int x = 0; x < colLength; x++)
                {
                    if (y == 0)
                    {
                        intGrid[y, x] = 2;
                        break;
                    }
                    if (y == 7 && x == 0)
                    {
                        intGrid[y, x] = 1;
                        break;
                    }
                    switch (gridCopy[y, x].Status)
                    {
                        case SpaceStatus.NoMine:
                            intGrid[y, x] = 3;
                            break;
                        case SpaceStatus.HasMine:
                            intGrid[y, x] = 0;
                            break;
                        case SpaceStatus.Found:
                            intGrid[y, x] = 3;
                            break;
                        case SpaceStatus.BlownUp:
                            intGrid[y, x] = 3;
                            break;
                    }
                }
            }

            return GFG.FindPath(intGrid);

        }
    }
}