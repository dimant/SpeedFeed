﻿namespace SpeedFeed
{
    internal class ForEachDecrement
    {
        public double min;
        public double max;
        public double step;

        public ForEachDecrement(double min, double max, double step)
        {
            this.min = min;
            this.max = max;
            this.step = step;
        }

        public void Do(Action<double> action)
        {
            double current = max;

            while (current - step > min)
            {
                current -= step;
                action(current);
            }

            double delta = current - min;
            if (delta> 0.001)
            {
                current -= delta;
                action(current);
            }
        }
    }
}
