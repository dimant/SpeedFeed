namespace SpeedFeed.Operations
{
    internal class Root : Operation
    {
        private int speed;
        private IList<string> headerLines = new List<string>();

        public Root(int speed)
        {
            this.speed = speed;
        }

        public void AddHeaderLine(string headerLine)
        {
            headerLines.Add(headerLine);
        }

        public override void Generate()
        {
            foreach(var headerLine in headerLines)
            {
                Line($"( {headerLine} )");
            }

            Line($"M03 S{speed}"); // turn on spindle, set spindle speed
            Line($"G00 Z{Number(Constants.SafeHeight)}"); // move to safety above work-piece
            Line($"G00 X0.0000 Y0.0000"); // move to xy home
            Line($"G00 Z0.5000"); // move to 0.5mm above work-piece
            Line("");

            base.Generate();

            Line($"G00 Z{Number(Constants.SafeHeight)}"); // move to 2mm above work-piece
            Line($"G00 Y0.0000"); // move to y home
            Line($"G00 X0.0000"); // move to x home
            Line($"M05"); // turn off spindle
        }

        public override string GetDescription()
        {
            return $"Auto generated speed and feed tester.";
        }
    }
}
