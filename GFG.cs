namespace Minefield
{
    public class GFG
    {
        static int R = 8, C = 8;
        public static List<(int, int)> FindPath(int[,] M)
        {
            List<(int, int)> path = new();
            var wallCount = 0;
            // 1) Create BFS queue q
            Queue<BFSElement> q = new Queue<BFSElement>();

            // 2)scan the matrix
            for (int i = 0; i < R; ++i)
            {
                for (int j = 0; j < C; ++j)
                {

                    // if there exists a cell in the matrix such
                    // that its value is 1 then push it to q
                    if (M[i, j] == 1)
                    {
                        q.Enqueue(new BFSElement(i, j));
                        break;
                    }
                }
            }

            // 3) run BFS algorithm with q.
            while (q.Count != 0)
            {
                BFSElement x = q.Peek();
                q.Dequeue();
                int i = x.i;
                int j = x.j;

                // skipping cells which are not valid.
                // if outside the matrix bounds
                if (i < 0 || i >= R || j < 0 || j >= C)
                    continue;

                // if they are walls (value is 0).
                if (M[i, j] == 0 && wallCount < 3)
                {
                    wallCount++;
                    continue;
                }

                // 3.1) if in the BFS algorithm process there
                // was a vertex x=(i,j) such that M[i][j] is 2
                // stop and return true
                if (M[i, j] == 2)
                {
                    path.Add((i, j));
                    return path;
                }


                // marking as wall upon successful visitation
                M[i, j] = 0;

                path.Add((i, j));

                // pushing to queue u=(i,j+1),u=(i,j-1)
                // u=(i+1,j),u=(i-1,j)
                for (int k = -1; k <= 1; k += 2)
                {
                    q.Enqueue(new BFSElement(i + k, j));
                    q.Enqueue(new BFSElement(i, j + k));
                }
            }

            // BFS algorithm terminated without returning true
            // then there was no element M[i][j] which is 2,
            // then return false
            return new List<(int, int)>();
        }
    }
}