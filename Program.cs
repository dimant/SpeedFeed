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
            double lineLength = 5.0,
            double depthOfCut = 1.0,
            int speeds = 3, int speed = 1000, int speedStep = 100,
            int feeds = 3, int feed = 2600, int feedStep = 100)
        {
            var root = new RootBlock(speed);

            var currentSpeed = speed;
            var currentFeed = feed;
            var currentX = 0.0;
            var currentY = 0.0;
            var xoffset = 5.0;
            var yoffset = 3;

            root.AddHeaderLine($"Total Width: {string.Format("{0:0.0000}", feeds * xoffset - xoffset)}");
            root.AddHeaderLine($"Total Height: {string.Format("{0:0.0000}", speeds * (lineLength + yoffset) - yoffset)}");
            root.AddHeaderLine($"Depth of cut: {string.Format("{0:0.0000}", depthOfCut)}");
            root.AddHeaderLine($"Speed from: {speed} to: {speed + speeds * speedStep} step: {speedStep}");
            root.AddHeaderLine($"Feed from: {feed}  to: {feed + feeds * feedStep} step: {feedStep}");

            for (int s = 0; s < speeds; s++)
            {
                for (int f = 0; f < feeds; f++)
                {
                    root.AddChild(new LineBlock(
                        y: currentY,
                        length: lineLength,
                        depthOfCut: depthOfCut,
                        speed: currentSpeed,
                        feed: currentFeed));

                    currentX += xoffset;
                    currentFeed += feedStep;

                    if(f < feeds - 1)
                    {
                        root.AddChild(new MoveBlock(currentX, currentY));
                    }
                }

                currentSpeed += speedStep;
                currentY += yoffset + lineLength;

                currentFeed = feed;
                currentX = 0.0;

                if(s < speeds - 1)
                {
                    root.AddChild(new MoveBlock(currentX, currentY));
                }
            }

            root.Generate();

            Console.WriteLine(root);
        }
    }
}