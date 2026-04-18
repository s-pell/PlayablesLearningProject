using Game.UI;
using Zenject;

namespace Game.Installers {
    public class MainMenuInstaller : MonoInstaller {
        //public MainMenuController Controller;
        public override void InstallBindings() {
            Container.Bind<MainMenuController>()
                .FromComponentInNewPrefabResource("Prefabs/UIRoot")
                .AsSingle().NonLazy(); // или Single
            //Container.QueueForInject(Controller);
        }
    }
}