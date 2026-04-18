using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.TestTools;

namespace Game.Tests
{
    public class LevelChunkPlacementTests
    {
        private const string testAddress = "LevelChunk_01"; // Замените на ваш адрес

        [UnityTest]
        public IEnumerator LoadLevelChunk_IsPlacedCorrectly()
        {
            // Загружаем часть уровня асинхронно
            AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(testAddress);
            yield return handle;

            Assert.AreEqual(AsyncOperationStatus.Succeeded, handle.Status,
                $"Failed to load asset at address {testAddress}");

            // Инстанцируем загруженный объект
            GameObject levelChunkInstance = Object.Instantiate(handle.Result);

            // Пример проверки позиции — замените на вашу логику размещения
            Vector3 expectedPosition = Vector3.zero; // Например, ожидаем, что объект должен быть в (0,0,0)
            Assert.AreEqual(expectedPosition, levelChunkInstance.transform.position,
                "Level chunk placed at incorrect position");

            // Можно также проверить родителя, если у вас есть контейнер для частей уровня
            // Например:
            // Assert.AreEqual(expectedParentTransform, levelChunkInstance.transform.parent);

            // Проверка, что объект активен
            Assert.IsTrue(levelChunkInstance.activeInHierarchy, "Level chunk is not active after instantiation");

            // Очистка
            Object.Destroy(levelChunkInstance);
            Addressables.Release(handle);
        }
    }
}