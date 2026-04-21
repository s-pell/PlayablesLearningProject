using UnityEngine;

namespace Game.Core.Configs
{
    [CreateAssetMenu(fileName = "ToggleConfig", menuName = "_Configs/Core/ToggleConfig")]
    public class UITileToggleConfig : ScriptableObject {
        public UITileToggle[] Toggles;
    }
}