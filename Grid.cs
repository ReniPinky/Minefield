﻿namespace Minefield
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
            const char SPACER = '|';
            const string END_ROW = "-----------------";
            const char PLAYER_ICON = 'P';
            int rowLength = grid.GetLength(0);
            int colLength = grid.GetLength(1);

            Console.WriteLine("Current Grid:");

            for (int y = 0; y < rowLength; y++)
            {
                Console.Write(SPACER);
                for (int x = 0; x < colLength; x++)
                {
                    if (playerY == y && playerX == x)
                    {
                        Console.Write(PLAYER_ICON);
                        Console.Write(SPACER);
                        continue;
                    }

                    Console.Write(string.Format("{0}", ChoseIcon(x, y)));
                    Console.Write(SPACER);
                }
                Console.Write(Environment.NewLine);
                Console.Write(END_ROW);
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
            grid[rowLength - 1, 0].ClearInitialSpace();
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

            return BestFitPath.FindPath(intGrid);
        }

        private char ChoseIcon(int x, int y)
        {
            return grid[y, x].Status switch
            {
                0 => '?',
                (SpaceStatus)1 => '?',
                (SpaceStatus)2 => '/',
                (SpaceStatus)3 => 'M',
                _ => '?',
            };
        }
    }
}