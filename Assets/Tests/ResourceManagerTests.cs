using System.Collections;
using System.Resources;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Game.Tests
{
    public class ResourceManagerTests
    {
    /*    private ResourceManager resourceManager;

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            // Предполагается, что ResourceManager — Singleton или компонент на объекте
            resourceManager = Object.FindObjectOfType<ResourceManager>();
            Assert.IsNotNull(resourceManager, "ResourceManager не найден в сцене");

            yield return null;
        }

        [UnityTest]
        public async UniTask ResourceManager_LoadAndReleaseAsset_WorksCorrectly()
        {
            string testAddress = "LevelChunk_01"; // Замените на реальный адрес

            // Запрашиваем загрузку ассета через ResourceManager
            var loadHandle = resourceManager.LoadAssetAsync<GameObject>(testAddress);

            // Ждём завершения загрузки
            yield return loadHandle;

            Assert.IsTrue(loadHandle.IsCompleted && !loadHandle.IsFaulted, "Загрузка ассета через ResourceManager не удалась");
            Assert.IsNotNull(loadHandle.Result, "Результат загрузки ассета через ResourceManager null");

            // Инстанцируем объект (если нужно)
            GameObject instance = Object.Instantiate(loadHandle.Result);
            Assert.IsNotNull(instance);

            // Освобождаем объект и ресурсы через ResourceManager
            Object.Destroy(instance);
            resourceManager.ReleaseAsset(loadHandle);

            // Ждём один кадр для освобождения ресурсов
            yield return null;

            // Дополнительных прямых проверок освобождения ресурсов нет, но отсутствие ошибок — хороший знак
        }*/
    }
}