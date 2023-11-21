using System.Text.RegularExpressions;

namespace Minefield
{
    public class Minefield
    {
        private Grid grid;
        private Player player;

        public bool IsFinished { get; private set; }

        public void IntialiseGame()
        {
            grid = new Grid();
            player = new Player();
            grid.CheckSpace(player.Y, player.X, HandleBlowUpEvent);
            grid.PrintGrid(player.Y, player.X);
            IsFinished = false;
            player.PlayerDiedEvent += HandlePlayerDeath;
        }

        public void Move()
        {
            Console.WriteLine("What direction would you like to move: ");
            try
            {
                var NextMovement = (Direction)Console.ReadLine()?.ToUpper().ToCharArray()[0];
                player.Move(NextMovement);
                grid.CheckSpace(player.Y, player.X, HandleBlowUpEvent);

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
            // save this in a txt/csv
            if (!File.Exists(Path.GetTempPath() + "HighScores.txt"))
            {
                File.Create(Path.GetTempPath() + "HighScores.txt").Dispose();
            }
            var highScoreList = File.ReadAllLines(Path.GetTempPath() + "HighScores.txt").ToList();
            Console.WriteLine("Current High scores:");
            var numberRegex = new Regex("[0-9]{1,}");

            List<Match> matches = highScoreList.Select(x => numberRegex.Match(x)).ToList();
            var scores = matches.Select(x => int.Parse(x.Value)).ToList();
            scores.Sort();
            foreach (var score in scores)
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

        private void HandleBlowUpEvent()
        {
            player.LoseLife();
        }

        private void HandlePlayerDeath(object _, EventArgs e)
        {
            IsFinished = true;
        }

    }
}