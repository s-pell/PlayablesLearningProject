using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Services
{
    public class GameService : IGameService
    {
        private ISceneService _sceneService;
        public GameService(ISceneService sceneService) {
            _sceneService = sceneService;
        }
        
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

        public async UniTaskVoid LoadMainMenu()
        {
            await _sceneService.LoadScene("MainMenu");//, LoadSceneMode.Additive, true);
        }

        public async UniTaskVoid LoadGameScene()
        {
            await _sceneService.LoadScene("Core");//, LoadSceneMode.Additive, true);
        }

        public async void Unload(string name)
        {
            await _sceneService.UnloadSceneByName(name);
        }
    }
}