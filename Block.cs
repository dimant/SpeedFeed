namespace SpeedFeed
{
    using System.Text;

    internal abstract class Block : IBlock
    {
        private StringBuilder builder = new StringBuilder();
        private IList<IBlock> children = new List<IBlock>();

        public abstract string GetDescription();

        protected string Number(double d)
        {
            return String.Format("{0:0.0000}", d);
        }

        protected void Line(string line)
        {
            builder.Append(line);
            builder.Append(System.Environment.NewLine);
        }

        protected void Write(string data)
        {
            builder.Append(data);
        }

        public virtual void Generate()
        {
            foreach(var child in children)
            {
                child.Generate();
                Line($"({child.GetDescription()})");
                Line(child.ToString());
            }
        }

        public override string ToString()
        {
            return builder.ToString();
        }

        public void AddChild(IBlock child)
        {
            children.Add(child);
        }
    }
}
