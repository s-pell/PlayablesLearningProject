using System;
using UnityEngine;
using UnityEngine.UIElements;
using Game;

namespace Game.UI
{
    [System.Serializable]
    public struct UILevelConfig
    {
        public string ButtonName;
        public LevelTilesConfig Config;
    }

    [Serializable]
    public struct UITileToggle
    {
        public string ToggleName;
        public int Col;
        public int Row;
    }

    public class SelectLevelController : MonoBehaviour
    {
        public UIDocument uiDocument;
        public UILevelConfig[] Configs;
        public UITileToggle[] Toggles;

        void OnEnable()
        {
            var root = uiDocument.rootVisualElement;
            foreach (var uiLevelConfig in Configs)
            {
                var levelButton = root.Q<Button>(uiLevelConfig.ButtonName);
                levelButton.clicked += OnLevelClicked(uiLevelConfig.Config);
            }

            foreach (var tileToggle in Toggles)
            {
                var toggle = root.Q<Toggle>(tileToggle.ToggleName);
                toggle.RegisterValueChangedCallback(OnToggleClicked);

                void OnToggleClicked(ChangeEvent<bool> evt)
                {
                    if (evt.newValue)
                    {
                        SceneManager.Instance.ShowTile(tileToggle.Col, tileToggle.Row);
                    }
                    else
                    {
                        SceneManager.Instance.HideTile(tileToggle.Col, tileToggle.Row);
                    }
                }
            }
        }

        private void OnDisable()
        {
            var root = uiDocument.rootVisualElement;
            foreach (var uiLevelConfig in Configs)
            {
                var levelButton = root.Q<Button>(uiLevelConfig.ButtonName);
                levelButton.clicked -= OnLevelClicked(uiLevelConfig.Config);
            }

            foreach (var tileToggle in Toggles)
            {
                var toggle = root.Q<Toggle>(tileToggle.ToggleName);
                toggle.UnregisterValueChangedCallback(OnToggleClicked);

                void OnToggleClicked(ChangeEvent<bool> evt)
                {
                    if (evt.newValue)
                    {
                        SceneManager.Instance.ShowTile(tileToggle.Col, tileToggle.Row);
                    }
                    else
                    {
                        SceneManager.Instance.HideTile(tileToggle.Col, tileToggle.Row);
                    }
                }
            }
        }

        private Action OnLevelClicked(LevelTilesConfig config)
        {
            return () => SceneManager.Instance.SelectLevel(config);
        }
    }
}