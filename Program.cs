var game = new Minefield.Minefield();

game.IntialiseGame();

do
{
    game.Move();

} while (!game.IsFinished);

game.ReportShortestPath();
Minefield.Minefield.ReportHighScore();
