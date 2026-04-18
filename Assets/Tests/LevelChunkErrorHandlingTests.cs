using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.TestTools;

namespace Game.Tests
{
    public class LevelChunkErrorHandlingTests
    {
        private const string invalidAddress = "NonExistent_LevelChunk";
    
        [UnityTest]
        public IEnumerator LoadInvalidAddress_ShouldFailGracefully()
        {
            AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(invalidAddress);
    
            yield return handle;
    
            // Проверяем, что загрузка завершилась с ошибкой
            Assert.AreEqual(AsyncOperationStatus.Failed, handle.Status, "Загрузка несуществующего адреса должна завершиться с ошибкой");
    
            // Опционально: проверяем, что результат null
            Assert.IsNull(handle.Result, "Результат загрузки несуществующего адреса должен быть null");
    
            // Освобождаем handle, даже если загрузка не удалась
            Addressables.Release(handle);
        }
    }
}