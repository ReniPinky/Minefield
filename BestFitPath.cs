namespace Minefield
{
    public class BestFitPath
    {
        private static readonly int row = 8, column = 8;

        public static List<(int, int)> FindPath(int[,] grid)
        {
            List<(int, int)> path = new();
            var wallCount = 0;

            Queue<SearchElement> searchQueue = new();

            for (int i = 0; i < row; ++i)
            {
                for (int j = 0; j < column; ++j)
                {
                    if (grid[i, j] == 1)
                    {
                        searchQueue.Enqueue(new SearchElement(i, j));
                        break;
                    }
                }
            }

            while (searchQueue.Count != 0)
            {
                var element = searchQueue.Peek();
                searchQueue.Dequeue();
                int i = element.I;
                int j = element.J;

                if (i < 0 || i >= row || j < 0 || j >= column)
                    continue;

                if (grid[i, j] == 0 && wallCount < 3)
                {
                    wallCount++;
                    continue;
                }

                if (grid[i, j] == 2)
                {
                    if (!path.Contains((i, j)))
                    {
                        path.Add((i, j));
                    }
                    return path;
                }

                grid[i, j] = 0;

                if (!path.Contains((i, j)))
                {
                    path.Add((i, j));
                }

                for (int k = -1; k <= 1; k += 2)
                {
                    searchQueue.Enqueue(new SearchElement(i + k, j));
                    searchQueue.Enqueue(new SearchElement(i, j + k));
                }
            }

            // finding a path was impossible
            return new List<(int, int)>();
        }
    }
}