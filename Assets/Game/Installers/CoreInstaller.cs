using Game.UI;
using Zenject;

namespace Game.Installers {
    public class CoreInstaller : MonoInstaller {
        public override void InstallBindings() {
            Container.BindInterfacesAndSelfTo<SelectLevelController>()
                .FromComponentInNewPrefabResource("Prefabs/Core/UIRoot")
                .AsSingle().NonLazy();
        }
    }
}