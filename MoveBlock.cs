namespace SpeedFeed
{
    internal class MoveBlock : Block
    {
        private double x;
        private double y;

        public MoveBlock(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public override string GetDescription()
        {
            return $"Moving to next cut.";
        }

        public override void Generate()
        {
            Line($"G00 Z{Number(Constants.SafeHeight)}");
            Line($"G00 X{Number(x)} Y{Number(y)}");
        }
    }
}
