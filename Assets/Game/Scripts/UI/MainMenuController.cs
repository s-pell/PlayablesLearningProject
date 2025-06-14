using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Game.UI
{
    public class MainMenuController : MonoBehaviour, IFontContainer
    {
        public UIDocument uiDocument;
        private Button _gameButton;
        private Button _exitButton;
        private AssetLoader<Font> fontLoader = null;


        void OnEnable()
        {
            if (fontLoader == null)
            {
                fontLoader = new AssetLoader<Font>(IFontContainer.FontAssetName);
                var root = uiDocument.rootVisualElement;
                _gameButton = root.Q<Button>("gameButton");
                _exitButton = root.Q<Button>("exitButton");
            }
            ApplyFontToUI().Forget();
            _gameButton.clicked += OnGameButtonClicked;
            _exitButton.clicked += OnExitButtonClicked;
        }
        private void OnDisable()
        {
            _gameButton.clicked -= OnGameButtonClicked;
            _exitButton.clicked -= OnExitButtonClicked;
            fontLoader.Release();
        }

        private void OnExitButtonClicked()
        {
            Debug.Log("ExitGame");
            
        }

        private void OnGameButtonClicked()
        {
            Debug.Log("LoadGameScene");
            GameManager.Instance.ChangeState(GameManager.GameState.Playing);
           // SceneManager.Instance.MainScene.
            //SceneManager.Instance.LoadSceneAsync("Level_1", LoadSceneMode.Additive, true).Forget();
        }

        public async UniTask ApplyFontToUI()
        {
            Debug.Log("Загрузить шрифт");
            var font = await fontLoader.LoadAsync();
            _gameButton.style.unityFont = font;
            _exitButton.style.unityFont = font;
        }
    }
}

