namespace SpeedFeed.Operations
{
    using System.Text;

    internal abstract class Operation : IOperation
    {
        private readonly StringBuilder builder = new StringBuilder();
        private readonly IList<IOperation> children = new List<IOperation>();

        public abstract string GetDescription();

        protected string Number(double d)
        {
            return string.Format("{0:0.0000}", d);
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

        public void AddChild(IOperation child)
        {
            children.Add(child);
        }
    }
}
