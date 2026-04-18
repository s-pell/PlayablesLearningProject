using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.TestTools;

namespace Game.Tests
{
    public class LevelTileLoadingTests
    {
        private const string testAddress = "1__0_0"; // Адрес части уровня в Addressables

        [UnityTest]
        public IEnumerator LoadLevelChunk_ByAddress_Succeeds()
        {
            // Запускаем асинхронную загрузку части уровня по адресу
            AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(testAddress);

            // Ждём завершения загрузки
            yield return handle;

            // Проверяем, что загрузка прошла успешно
            Assert.IsTrue(handle.Status == AsyncOperationStatus.Succeeded,
                $"Failed to load addressable asset: {testAddress}");

            // Инстанцируем загруженный объект на сцену
            GameObject levelChunkInstance = Object.Instantiate(handle.Result);

            // Проверяем, что объект создан и активен
            Assert.IsNotNull(levelChunkInstance);
            Assert.IsTrue(levelChunkInstance.activeInHierarchy);

            // Освобождаем ресурсы
            Object.Destroy(levelChunkInstance);
            Addressables.Release(handle);
        }
    }
}