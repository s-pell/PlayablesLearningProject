using System.Collections;
using System.ComponentModel;
using Cysharp.Threading.Tasks;
using Game.Services;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Zenject;
using SceneManager = UnityEngine.SceneManagement.SceneManager;

namespace Game.Tests
{
    public class BootstrapSceneTests
    {
        private const string bootstrapSceneName = "Bootstrap";
        
        [UnityTest]
        public async UniTask BootstrapScene_ManagersAreInitialized(DiContainer container)
        {
            // Загружаем сцену Bootstrap асинхронно
            var manager = container.Resolve<AddressablesService>();

        }
    }
}            //var sceneInstance = await AddressablesServcie.Instance.LoadSceneAsync(bootstrapSceneName, LoadSceneMode.Single);
             //AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(bootstrapSceneName, LoadSceneMode.Single);
 
             // Ждём загрузки сцены
             // while (!asyncLoad.isDone)
             //     yield return null;
 
             // Здесь проверяем наличие и инициализацию менеджеров
             // Например, если у вас есть GameManager и ResourceManager как одиночки (Singletons):
 
             // var gameManager = GameObject.FindObjectOfType<GameManager>();
             // Assert.IsNotNull(gameManager, "GameManager не найден в сцене Bootstrap");
             // Assert.IsTrue(gameManager.IsInitialized, "GameManager не инициализирован");
             //
             // var resourceManager = GameObject.FindObjectOfType<ResourceManager>();
             // Assert.IsNotNull(resourceManager, "ResourceManager не найден в сцене Bootstrap");
             // Assert.IsTrue(resourceManager.IsInitialized, "ResourceManager не инициализирован");