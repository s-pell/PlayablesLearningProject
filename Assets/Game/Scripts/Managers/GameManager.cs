using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public enum GameState
        {
            Bootstrap,
            Loading,
            MainMenu,
            Playing,
            Paused,
            GameOver
        }

        public GameState CurrentState { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                CurrentState = GameState.Bootstrap;
            }
            else
            {
                Destroy(gameObject);
            }
            
        }

        public void ChangeState(GameState newState)
        {
            if (CurrentState == newState)
                return;

            Debug.Log($"Game state changed from {CurrentState} to {newState}");
            CurrentState = newState;

            switch (newState)
            {
                case GameState.Loading:
                    break;
                case GameState.MainMenu:
                    LoadMainMenu().Forget();
                    break;
                case GameState.Playing:
                    LoadGameScene().Forget();
                    break;
                case GameState.Paused:
                    break;
                case GameState.GameOver:
                    break;
            }
        }

        private async UniTaskVoid LoadMainMenu()
        {
            await SceneManager.Instance.LoadScene("MainMenu");//, LoadSceneMode.Additive, true);
        }
        
        private async UniTaskVoid LoadGameScene()
        {
            await SceneManager.Instance.LoadScene("Core");//, LoadSceneMode.Additive, true);
        }

        private async void Unload(string name)
        {
            await SceneManager.Instance.UnloadSceneByName(name);
        }
    }
}