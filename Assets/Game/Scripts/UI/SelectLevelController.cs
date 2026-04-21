using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Core.Configs;
using UnityEngine;
using UnityEngine.UIElements;
using Game.Services;
using Zenject;

namespace Game.UI {
    public class SelectLevelController : MonoBehaviour, IFontContainer, IInitializable {
        public UIDocument uiDocument;
        public UILevelConfig[] Configs;
        public UITileToggle[] Toggles;

        private AssetLoader<Font> fontLoader = null;
        private VisualElement root;
        private Dictionary<Toggle, UITileToggle> _toggles = new Dictionary<Toggle, UITileToggle>(4);
        private Dictionary<Button, LevelTilesConfig> _configs = new Dictionary<Button, LevelTilesConfig>(2);
        private SceneService _sceneService;

        [Inject]
        public void Construct(SceneService sceneService) {
            _sceneService = sceneService;
        }

        public void Initialize() {
            LoadFont();
        }
        
        void LoadFont() {
            if (fontLoader == null) {
                fontLoader = new AssetLoader<Font>(IFontContainer.FontAssetName);
                root = uiDocument.rootVisualElement;
                foreach (var tileToggle in Toggles) {
                    var toggle = root.Q<Toggle>(tileToggle.ToggleName);
                    _toggles.Add(toggle, tileToggle);
                }

                foreach (var uiLevelConfig in Configs) {
                    var levelButton = root.Q<Button>(uiLevelConfig.ButtonName);
                    _configs.Add(levelButton, uiLevelConfig.Config);
                }
            }

            foreach (var pair in _configs) {
                pair.Key.clicked += OnLevelClicked(pair.Value);
            }

            foreach (var pair in _toggles) {
                pair.Key.RegisterValueChangedCallback(evt => OnToggleClicked(evt.newValue, pair.Value));
            }

            ApplyFontToUI().Forget();
        }

        private void OnToggleClicked(bool on, UITileToggle uiToggle)
        {
            if (on)
            {
                _sceneService.ShowTile(uiToggle.Col, uiToggle.Row);
            }
            else
            {
                _sceneService.HideTile(uiToggle.Col, uiToggle.Row);
            }
        }

        private void OnDisable()
        {
            
            foreach (var pair in _configs)
            {
                pair.Key.clicked -= OnLevelClicked(pair.Value);
            }

            foreach (var pair in _toggles)
            {
                pair.Key.UnregisterValueChangedCallback(evt => OnToggleClicked(evt.newValue, pair.Value));
            }

            fontLoader.Release();
        }

        private void OnDestroy() {
            _configs.Clear();
            _toggles.Clear();
        }

        private Action OnLevelClicked(LevelTilesConfig config) {
            return () => _sceneService.SelectLevel(config);
        }

        public async UniTask ApplyFontToUI() {
            Debug.Log("Применить шрифт");
            var font = await fontLoader.LoadAsync();
            foreach (var button in _configs.Keys) {
                button.style.unityFont = font;
            }

            foreach (var toggle in _toggles.Keys) {
                toggle.style.unityFont = font;
            }
        }

    }
}