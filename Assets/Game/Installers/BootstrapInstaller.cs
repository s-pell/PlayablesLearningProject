using Zenject;

namespace Game.Installers
{
    public class BootstrapInstaller : MonoInstaller {
        public override void InstallBindings() {
            Container.BindInterfacesAndSelfTo<Bootstrapper>().AsSingle().NonLazy();
        }
    }
}