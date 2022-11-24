namespace Core
{
    public class StarterScene : Singleton<StarterScene>
    {
        public void StartScene()
        {
            SceneControllers.OnInit += ControllersInit;
            SceneControllers.InitControllers();
        }

        private void ControllersInit()
        {
            SceneControllers.OnInit -= ControllersInit;

            AfterInitControllers();
        }

        protected virtual void AfterInitControllers() { }
    }
}