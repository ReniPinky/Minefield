namespace Minefield
{
    public class Player
    {
        private int _x, _y;
        private int _lives = 3;
        private int _moveCounter;

        public Player()
        {
            _x = 0;
            _y = 7;
            _moveCounter = 0;
        }

        public int X { get => _x; }

        public int Y { get => _y; }

        public int MoveCounter { get => _moveCounter; }

        public event EventHandler PlayerDiedEvent;

        public void Move(Direction direction)
        {
            _moveCounter++;
            switch (direction)
            {
                case Direction.Up:
                    if (_y <= 0)
                    {
                        // This is the win condition?
                        Console.WriteLine("Cannot go up");
                        break;
                    }
                    _y--;
                    Console.WriteLine("Player moved up");
                    break;
                case Direction.Down:
                    if (_y >= 7)
                    {
                        Console.WriteLine("Cannot go down");
                        break;
                    }
                    Console.WriteLine("Player moved down");
                    _y++;
                    break;
                case Direction.Left:
                    if (_x <= 0)
                    {
                        Console.WriteLine("Cannot go left");
                        break;
                    }
                    Console.WriteLine("Player moved left");
                    _x--;
                    break;
                case Direction.Right:
                    if (_x >= 7)
                    {
                        Console.WriteLine("Cannot go right");
                        break;
                    }
                    Console.WriteLine("Player moved right");
                    _x++;
                    break;
                default: throw new ArgumentException();
            }
        }

        public void LoseLife()
        {
            --_lives;
            if (_lives == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Console.WriteLine("You have died, please try again");
            PlayerDiedEvent.Invoke(this, EventArgs.Empty);
        }
    }
}