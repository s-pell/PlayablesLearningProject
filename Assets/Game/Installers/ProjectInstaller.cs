using Game.Services;
using Zenject;

namespace Game.Installers {
    public class ProjectInstaller : MonoInstaller {
        public override void InstallBindings() {
            Container.Bind<IAddressablesService>().To<AddressablesService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SceneService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameService>().AsSingle().NonLazy();
        }
    }
}
