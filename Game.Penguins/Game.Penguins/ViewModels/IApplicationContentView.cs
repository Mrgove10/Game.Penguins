namespace Game.Penguins.ViewModels
{
    internal interface IApplicationContentView
    {
        string Name { get; }

        string PreviousButtonContent { get; }

        string NextButtonContent { get; }

        bool HasPreviousView { get; }

        bool HasNextView { get; }

        IApplicationContentView GetPreviousView();

        IApplicationContentView GetNextView();
    }
}