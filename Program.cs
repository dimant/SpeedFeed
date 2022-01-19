namespace SpeedFeed
{
    using System;

    internal class Program
    {
        /// <summary>
        /// Generate cuts at different speeds and feeds.
        /// </summary>
        /// <param name="n">number of cuts</param>
        /// <param name="lineLength">length of lines</param>
        /// <param name="depthOfCut">depth of cut</param>
        /// <param name="speed">starting spindle speed</param>
        /// <param name="speedStep">increments of spindle speed</param>
        /// <param name="feed">starting feed speed</param>
        /// <param name="feedStep">increments of feed speed</param>
        public static void Main(
            int n = 3,
            double lineLength = 25.0,
            double depthOfCut = 1.0,
            int speed = 1000, int speedStep = 100,
            int feed = 2600, int feedStep = 100)
        {
            var root = new RootBlock(speed);

            var currentSpeed = speed;
            var currentFeed = feed;
            var currentX = 0.0;
            var currentY = 0.0;
            var xoffset = 10.0;

            root.AddHeaderLine($"Total Width: {string.Format("{0:0.0000}", n * xoffset)}");
            root.AddHeaderLine($"Total Height: {string.Format("{0:0.0000}", lineLength)}");
            root.AddHeaderLine($"Depth of cut: {string.Format("{0:0.0000}", depthOfCut)}");
            root.AddHeaderLine($"Speed from: {speed} to: {speed + n * speedStep} step: {speedStep}");
            root.AddHeaderLine($"Feed from: {feed}  to: {feed + n * feedStep} step: {feedStep}");

            for (int i = 0; i < n; i++)
            {
                root.AddChild(new LineBlock(
                    length: lineLength,
                    depthOfCut: depthOfCut,
                    speed: currentSpeed,
                    feed: currentFeed));

                currentX += xoffset;
                currentY += 0.0;
                currentSpeed += speedStep;
                currentFeed += feedStep;

                if(i < n - 1)
                {
                    root.AddChild(new StepBlock(currentX, currentY));
                }
            }

            root.Generate();

            Console.WriteLine(root);
        }
    }
}