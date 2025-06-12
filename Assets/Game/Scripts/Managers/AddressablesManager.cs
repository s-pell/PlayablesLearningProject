using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Game
{
    public class AddressablesManager : MonoBehaviour
    {
        public static AddressablesManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public async UniTask<SceneInstance> LoadSceneAsync(string address, LoadSceneMode loadMode = LoadSceneMode.Single, bool activateOnLoad = true)
        {
            var handle = Addressables.LoadSceneAsync(address, loadMode, activateOnLoad);
            await handle.ToUniTask();

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                return handle.Result; // SceneInstance
            }
            else
            {
                Debug.LogError($"Failed to load scene at address {address}");
                return default;
            }
        }

        public async UniTask UnloadSceneAsync(SceneInstance sceneInstance)
        {
            var handle = Addressables.UnloadSceneAsync(sceneInstance);
            await handle.ToUniTask();
        }

        public async UniTask<GameObject> InstantiateAsync(string address, Vector3 position)
        {
            return await InstantiateAsync(address, position, Quaternion.identity);
        }

        public async UniTask<GameObject> InstantiateAsync(string address, Vector3 position, Quaternion rotation)
        {
            AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync(address, position, rotation);
            await handle.Task; 

            if (handle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
            {
                Debug.Log("Префаб загружен и инстанцирован");
                return handle.Result;
            }
            else
            {
                Debug.LogError("Ошибка загрузки префаба");
                return null;
            }
        }

        public async UniTask<GameObject> InstantiateAsync(AssetReference prefabReference)
        {
            var handle = prefabReference.InstantiateAsync();
            await handle.Task; 

            if (handle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
            {
                Debug.Log("Префаб загружен и инстанцирован");
                return handle.Result;
            }
            else
            {
                Debug.LogError("Ошибка загрузки префаба");
                return null;
            }
        }

        public void ReleaseInstance(GameObject instance)
        {
            if (instance != null)
            {
                Addressables.ReleaseInstance(instance);
                Debug.Log("Префаб выгружен");
            }
        }
    }
}