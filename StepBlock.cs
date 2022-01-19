namespace SpeedFeed
{
    internal class StepBlock : Block
    {
        private double x;
        private double y;

        public StepBlock(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public override string GetDescription()
        {
            return $"Moving to next line.";
        }

        public override void Generate()
        {
            Line($"G00 Z{Number(Constants.SafeHeight)}");
            Line($"G00 X{Number(x)} Y{Number(y)}");
        }
    }
}
