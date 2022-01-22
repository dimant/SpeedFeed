namespace SpeedFeed
{
    using SpeedFeed.Operations;
    using System;

    internal class Program
    {
        /// <summary>
        /// Generate cuts at different speeds and feeds.
        /// </summary>
        /// <param name="maxDepth">max depth</param>
        /// <param name="depthOfCut">depth of cut</param>
        /// <param name="lineLength">length of lines</param>
        /// <param name="speed">starting spindle speed</param>
        /// <param name="speedStep">increments of spindle speed</param>
        /// <param name="feed">starting feed speed</param>
        /// <param name="feedStep">increments of feed speed</param>
        public static void Main(
            double maxDepth,
            double depthOfCut = 1.0,
            double lineLength = 5.0,
            int speeds = 3, int speed = 1000, int speedStep = 100,
            int feeds = 3, int feed = 2600, int feedStep = 100)
        {
            var root = new Root(speed);

            var currentSpeed = speed;
            var currentFeed = feed;
            var currentX = 0.0;
            var currentY = 0.0;
            var xoffset = 5.0;
            var yoffset = 5.0;

            root.AddHeaderLine($"Total Width: {string.Format("{0:0.0000}", feeds * xoffset - xoffset)}");
            root.AddHeaderLine($"Total Height: {string.Format("{0:0.0000}", speeds * (lineLength + yoffset) - yoffset)}");
            root.AddHeaderLine($"Depth of cut: {string.Format("{0:0.0000}", depthOfCut)}");
            root.AddHeaderLine($"Speed from: {speed} to: {speed + (speeds - 1) * speedStep} step: {speedStep}");
            root.AddHeaderLine($"Feed from: {feed}  to: {feed + (feeds - 1) * feedStep} step: {feedStep}");

            var cutToDepth = new ForEachDecrement(-maxDepth, 0.0, depthOfCut);

            for (int s = 0; s < speeds; s++)
            {
                for (int f = 0; f < feeds; f++)
                {
                    cutToDepth.Do((z) =>
                    {
                        root.AddChild(new Line(
                            y: currentY,
                            length: lineLength,
                            depth: z,
                            speed: currentSpeed,
                            feed: currentFeed));
                    });

                    currentX += xoffset;
                    currentFeed += feedStep;

                    if(f < feeds - 1)
                    {
                        root.AddChild(new Move(currentX, currentY));
                    }
                }

                currentSpeed += speedStep;
                currentY += yoffset + lineLength;

                currentFeed = feed;
                currentX = 0.0;

                if(s < speeds - 1)
                {
                    root.AddChild(new Move(currentX, currentY));
                }
            }

            root.Generate();

            Console.WriteLine(root);
        }
    }
}