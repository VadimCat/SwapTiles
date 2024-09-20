using Cysharp.Threading.Tasks;
using Ji2.Context;
using Ji2.ScreenNavigation;

namespace SwapTiles.UI.Screens
{
    public class LevelSelectionScreen : BaseScreen
    {
        public override async UniTask Show()
        {
            await base.Show();

            DiContext context = DiContext.GetOrCreateInstance();
        }
    }
}