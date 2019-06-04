using Game.Penguins.Framework;
using Game.Penguins.ViewModels;

namespace Game.Penguins.Commands
{
    internal class MovePenguinSelectorViewModelCommand
        : Command
    {
        private readonly CurrentGameViewModel currentGameViewModel;

        public MovePenguinSelectorViewModelCommand(CurrentGameViewModel contextViewModel) : base()
        {
            currentGameViewModel = contextViewModel;
        }

        public override void Execute(object parameter)
        {
            currentGameViewModel.SelectedOriginPenguin();
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }
    }
}