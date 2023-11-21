namespace Minefield
{
    [Flags]
    public enum SpaceStatus
    {
        NoMine = 0,
        HasMine = 1,
        Found = 2,
        BlownUp = 3,
    }
}