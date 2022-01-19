namespace SpeedFeed
{
    internal class LineBlock : Block
    {
        private double length;
        private double depthOfCut;
        private int speed;
        private int feed;

        public LineBlock(
            double length,
            double depthOfCut,
            int speed, int feed)
        {
            this.length = length;
            this.depthOfCut = depthOfCut;
            this.speed = speed;
            this.feed = feed;
        }

        public override void Generate()
        {
            Line($"G01 Z0.0000 F{feed} S{speed}"); // set feed and speed
            Line($"G01 Z-{depthOfCut}"); // plunge
            Line($"G01 Y{Number(length)}");
            Line($"G01 Z{Constants.SafeHeight}");
            Line($"G01 Y0.0000");
        }

        public override string GetDescription()
        {
            return $"Cutting vertical line with S{speed} F{feed} at {Number(depthOfCut)} deep, {Number(length)} long.";
        }
    }
}
