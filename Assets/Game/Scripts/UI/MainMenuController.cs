using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Game
{
    public class MainMenuController : MonoBehaviour
    {
        public UIDocument uiDocument;
        private Button _gameButton;
        private Button _exitButton;

        void OnEnable()
        {
            var root = uiDocument.rootVisualElement;
            _gameButton = root.Q<Button>("gameButton");
            _exitButton = root.Q<Button>("exitButton");
            _gameButton.clicked += OnGameButtonClicked;
            _exitButton.clicked += OnExitButtonClicked;
        }

        private void OnExitButtonClicked()
        {
            Debug.Log("ExitGame");
            
        }

        private void OnGameButtonClicked()
        {
            Debug.Log("LoadGameScene");
            GameManager.Instance.ChangeState(GameManager.GameState.Playing);
            SceneManager.Instance.LoadSceneAsync("Game_1_base", LoadSceneMode.Additive, true).Forget();
        }

        private void OnDisable()
        {
            _gameButton.clicked -= OnGameButtonClicked;
            _exitButton.clicked -= OnExitButtonClicked;
        }
    }
}

