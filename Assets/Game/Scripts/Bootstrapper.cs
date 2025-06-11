using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Game
{
    public class Bootstrapper : MonoBehaviour
    {
        
        private async void Start()
        {
            Debug.Log("Bootstrapper: Инициализация...");

            // Здесь можно добавить инициализацию менеджеров, загрузку настроек и т.п.
            await InitializeManagers();

            Debug.Log("Bootstrapper: Инициализация завершена.");

            // Загружаем следующую сцену (например, Loading или MainMenu)
            await LoadNextScene("MainMenu");
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

        private async UniTask LoadNextScene(string sceneName)
        {
            Debug.Log($"Bootstrapper: Загрузка сцены {sceneName}...");
            UniTask<SceneInstance> asyncOp = SceneManager.Instance.LoadSceneAsync(sceneName);
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