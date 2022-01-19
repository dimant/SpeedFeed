namespace SpeedFeed
{
    using System;

    internal class Program
    {
        public static void Main(
            int n = 3,
            int speed = 1000, int speedStep = 100,
            int feed = 2600, int feedStep = 100)
        {
            var root = new RootBlock(speed);


            var currentSpeed = speed;
            var currentFeed = feed;
            var currentX = 0.0;
            var currentY = 0.0;
            var xoffset = 10.0;

            var length = 25.0;

            root.AddHeaderLine($"Total Width: {n * xoffset}");
            root.AddHeaderLine($"Total Height: {length}");
            root.AddHeaderLine($"Speed from: {speed} to: {speed + n * speedStep} step: {speedStep}");
            root.AddHeaderLine($"Feed from: {feed}  to: {feed + n * feedStep} step: {feedStep}");

            for (int i = 0; i < n; i++)
            {
                root.AddChild(new LineBlock(
                    length: length,
                    depthOfCut: 1.0,
                    speed: currentSpeed,
                    feed: currentFeed));

                currentX += xoffset;
                currentY += 0.0;
                currentSpeed += speedStep;
                currentFeed += feedStep;

                root.AddChild(new StepBlock(currentX, currentY));
            }

            root.Generate();

            Console.WriteLine(root);
        }
    }
}