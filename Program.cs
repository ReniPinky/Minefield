var game = new Minefield.Minefield();

do
{
    game.Move();

} while (!game.IsFinished);

Minefield.Minefield.ReportHighScore();
//game.ReportShortestPath();