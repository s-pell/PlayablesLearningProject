using UnityEngine;

namespace Game.Core.Configs
{
    [CreateAssetMenu(fileName = "UIConfig", menuName = "_Configs/Core/UIConfig")]
    public class UICoreConfig : ScriptableObject {
        public UILevelConfig[] Configs;
    }
}