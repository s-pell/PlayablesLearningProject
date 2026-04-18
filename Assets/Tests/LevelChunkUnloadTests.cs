using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.TestTools;

namespace Game.Tests
{
    public class LevelChunkUnloadTests
    {
        private const string testAddress = "1__0_0"; 
    
        [UnityTest]
        public IEnumerator LoadAndUnloadLevelChunk_ReleasesResources()
        {
            // Загружаем часть уровня
            AsyncOperationHandle<GameObject> loadHandle = Addressables.LoadAssetAsync<GameObject>(testAddress);
            yield return loadHandle;
    
            Assert.AreEqual(AsyncOperationStatus.Succeeded, loadHandle.Status, $"Failed to load asset at address {testAddress}");
    
            // Инстанцируем объект
            GameObject levelChunkInstance = Object.Instantiate(loadHandle.Result);
            Assert.IsNotNull(levelChunkInstance);
            
            Object.Destroy(levelChunkInstance);// Освобождаем инстанцированный объект
            Addressables.Release(loadHandle);// Выгружаем Addressable ресурс
            yield return null;// Ждём один кадр, чтобы Unity успел освободить ресурсы
    
            // Проверяем, что ресурс выгружен
            // Прямого API для проверки выгрузки нет, но можно проверить, что handle не валиден
            Assert.IsFalse(loadHandle.IsValid(), "Addressable handle должен быть невалиден после Release");
    
            // Дополнительно можно вызвать Resources.UnloadUnusedAssets и проверить состояние памяти,
            // но это дорогостоящая операция и редко делается в юнит-тестах
            yield return Resources.UnloadUnusedAssets();
        }
    }
}
