namespace SpeedFeed
{
    internal interface IBlock
    {
        string GetDescription();

        void Generate();

        void AddChild(IBlock block);
    }
}
