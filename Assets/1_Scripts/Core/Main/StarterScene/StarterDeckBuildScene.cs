using UI;

namespace Core
{
    public class StarterDeckBuildScene : StarterScene
    {
        protected override void AfterInitControllers()
        {
            UIManager.ShowWindow<MenuWindow>();
        }
    }
}