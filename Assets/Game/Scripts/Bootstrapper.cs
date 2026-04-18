using Cysharp.Threading.Tasks;
using Game.Services;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders;
using Zenject;

namespace Game
{
    public class Bootstrapper : IInitializable {
        private ISceneService _sceneService;
        [Inject]
        public Bootstrapper(ISceneService sceneService) {
            _sceneService = sceneService;
        }
        
        public void Initialize() {
            Debug.LogError("Bootstrapper: Инициализация завершена.");
            LoadNextScene("MainMenu").Forget();
        }

        private async UniTask InitializeManagers()
        {
            // Пример задержки для имитации загрузки
            await UniTask.Delay(500);

            // Можно добавить вызовы инициализации конкретных менеджеров, если нужно
            // Например:
            // await AddressablesManager.Instance.InitializeAsync();

            // Если есть подключение к сети, можно дождаться подключения
            // await NetworkManager.Instance.ConnectAsync();

            // И т.п.
        }

        private async UniTask LoadNextScene(string sceneName) {
            Debug.LogError($"Bootstrapper: Загрузка сцены {sceneName}...");
            UniTask<SceneInstance> asyncOp = _sceneService.LoadSceneAsync(sceneName);
            await asyncOp;
            // while (!asyncOp.isDone)
            // {
            //     // Можно здесь показывать прогресс загрузки, если есть UI
            //     await UniTask.Yield();
            // }
            Debug.Log($"Bootstrapper: Сцена {sceneName} загружена.");
        }
    }
}