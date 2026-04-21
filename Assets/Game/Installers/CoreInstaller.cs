using Game.Core.Configs;
using Game.UI;
using UnityEngine;
using Zenject;

namespace Game.Installers {
    public class CoreInstaller : MonoInstaller {
        public override void InstallBindings() {
            Container.BindInterfacesAndSelfTo<SelectLevelController>()
                .FromComponentInNewPrefabResource("Prefabs/Core/UIRoot")
                .AsSingle().NonLazy();
            Container.Bind<UICoreConfig>().FromInstance(Resources.Load<UICoreConfig>("Configs/UIConfig")
            ).AsSingle();
            Container.Bind<UITileToggleConfig>().FromInstance(Resources.Load<UITileToggleConfig>("Configs/ToggleConfig")
            ).AsSingle();
        }
    }
}