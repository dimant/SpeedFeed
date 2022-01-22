namespace SpeedFeed.Operations
{
    internal class Line : Operation
    {
        private readonly double y;
        private readonly double length;
        private readonly double depth;
        private readonly int speed;
        private readonly int feed;

        public Line(
            double y,
            double length,
            double depth,
            int speed, int feed)
        {
            this.y = y;
            this.length = length;
            this.depth = depth;
            this.speed = speed;
            this.feed = feed;
        }

        public override void Generate()
        {
            Line($"G01 Z0.0000 F{feed} S{speed}"); // set feed and speed
            Line($"G01 Z{Number(depth)}"); // plunge
            Line($"G01 Y{Number(y + length)}");
        }

        public override string GetDescription()
        {
            return $"Cutting vertical line with S{speed} F{feed} at {Number(depth)} deep, {Number(length)} long.";
        }
    }
}
