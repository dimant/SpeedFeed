namespace SpeedFeed.Operations
{
    internal interface IOperation
    {
        string GetDescription();

        void Generate();

        void AddChild(IOperation block);
    }
}
