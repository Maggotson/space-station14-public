using Content.Shared.Lathe;
using Content.Shared.Research.Components;
using Content.Shared.PrinterDoc; // Imperial PrinterDoc
using JetBrains.Annotations;

namespace Content.Client.Lathe.UI
{
    [UsedImplicitly]
    public sealed class LatheBoundUserInterface : BoundUserInterface
    {
        [ViewVariables]
        private LatheMenu? _menu;
        public LatheBoundUserInterface(EntityUid owner, Enum uiKey) : base(owner, uiKey)
        {
        }

        protected override void Open()
        {
            base.Open();

            _menu = new LatheMenu(this);
            _menu.OnClose += Close;


            _menu.OnServerListButtonPressed += _ =>
            {
                SendMessage(new ConsoleServerSelectionMessage());
            };

            _menu.RecipeQueueAction += (recipe, amount) =>
            {
                SendMessage(new LatheQueueRecipeMessage(recipe, amount));
            };

            // Imperial PrinterDoc
            _menu.OnUseCardIdCheckBoxChanged += useCardId =>
            {
                SendMessage(new PrinterDocCheckIdCardMessage(useCardId));
            };

            _menu.OpenCenteredRight();
        }

        // Imperial PrinterDoc
        protected override void UpdateState(BoundUserInterfaceState state)
        {
            base.UpdateState(state);

            if (_menu == null)
                return;
            if (state is LatheUpdateState msg)
            {
                _menu.Recipes = msg.Recipes;
                _menu.PopulateRecipes();
                _menu.UpdateCategories();
                _menu.PopulateQueueList(msg.Queue);
                _menu.SetQueueInfo(msg.CurrentlyProducing);
                _menu.SetUseCardId(msg.UseCardId);
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (!disposing)
                return;
            _menu?.Dispose();
        }
    }
}
