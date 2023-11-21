var game = new Minefield.Minefield();

game.IntialiseGame();

do
{
    game.Move();

} while (!game.IsFinished);

//game.ReportShortestPath();
game.ReportHighScore();