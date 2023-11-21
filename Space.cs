namespace Minefield
{
    public class Space
    {
        public SpaceStatus Status { get; private set; }

        public Space()
        {
            var random = new Random();
            Status = random.NextDouble() > 0.5 ? SpaceStatus.HasMine : SpaceStatus.NoMine;
        }

        public void ClearInitialSpace()
        {
            Status = SpaceStatus.Found;
        }

        public void MoveIntoSpace(Action spaceBlowUpCallback)
        {
            // Do nothing if already found or has been blown up;
            if (Status == SpaceStatus.HasMine)
            {
                BlowUpMine(spaceBlowUpCallback);
            }
            else if (Status == SpaceStatus.NoMine)
            {
                BecomeFound();
            }
        }

        private void BlowUpMine(Action spaceBlowUpAction)
        {
            spaceBlowUpAction.Invoke();
            Status = SpaceStatus.BlownUp;
        }

        private void BecomeFound()
        {
            Status = SpaceStatus.Found;
        }
    }
}