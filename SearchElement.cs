namespace Minefield
{
    public class SearchElement
    {
        private readonly int _i, _j;

        public SearchElement(int i, int j)
        {
            _i = i;
            _j = j;
        }

        public int I { get { return _i; } }

        public int J { get { return _j; } }
    }
}
