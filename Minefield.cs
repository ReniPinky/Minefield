using System.Text.RegularExpressions;

namespace Minefield
{
    public class Minefield
    {
        private readonly Grid grid;
        private readonly Player player;

        public Minefield()
        {
            grid = new Grid();
            player = new Player();
            // Check the start space to make sure it is validated
            grid.CheckSpace(player.Y, player.X, HandleBlowUp);
            grid.PrintGrid(player.Y, player.X);
            IsFinished = false;
            // Subscribe to the event to be notified if the player dies
            player.PlayerDiedEvent += HandlePlayerDeath;
        }

        public bool IsFinished { get; private set; }

        public void Move()
        {
            Console.WriteLine("What direction would you like to move: ");
            try
            {
                // was told to assume correct input so little input validation here.
                var NextMovement = (Direction)Console.ReadLine()?.ToUpper().ToCharArray()[0];
                player.Move(NextMovement);
                grid.CheckSpace(player.Y, player.X, HandleBlowUp);

                if (player.Y == 0 && !IsFinished)
                {
                    WinGame();
                }

                grid.PrintGrid(player.Y, player.X);
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Please give a valid input");
            }
        }

        public static void ReportHighScore()
        {
            string highScorePath = Path.GetTempPath() + "HighScores.txt";

            if (!File.Exists(highScorePath))
            {
                File.Create(highScorePath).Dispose();
            }

            var highScoreList = File.ReadAllLines(highScorePath).ToList();
            Console.WriteLine("Current High scores:");

            // Match the Regex
            var numberRegex = new Regex("[0-9]{1,}");
            List<Match> matches = highScoreList.Select(x => numberRegex.Match(x)).ToList();

            // Cast the match to ints
            var scores = matches.Select(x => int.Parse(x.Value)).ToList();
            // Sort lowest to highest
            scores.Sort();
            // Get the 10 top scores
            var highScores = scores.GetRange(0, 10);
            // Print scores
            foreach (var score in highScores)
            {
                Console.WriteLine(string.Format("{0}", score));
            }
        }

        public void ReportShortestPath()
        {
            // implement a path finding alg
            var bestPath = grid.FindBestPath();

            if (bestPath.Count < 1)
            {
                Console.WriteLine("This game was impossible");
                return;
            }
            foreach (var coord in bestPath)
            {
                Console.WriteLine("Best path was: ");
                Console.WriteLine(string.Format("{0},{1}", coord.Item1, coord.Item2));
            }
        }

        private void WinGame()
        {
            Console.WriteLine("Congratulations you have won the game");
            IsFinished = true;

            //Open high score file and write to file
            using (StreamWriter writetext = File.AppendText(Path.GetTempPath() + "HighScores.txt"))
            {
                writetext.WriteLine(string.Format("Game won in {0} moves", player.MoveCounter));
            }
        }

        private void HandleBlowUp()
        {
            player.LoseLife();
        }

        private void HandlePlayerDeath(object? _, EventArgs e)
        {
            IsFinished = true;
        }
    }
}