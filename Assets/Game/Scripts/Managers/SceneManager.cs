using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Game
{
    public class SceneManager : MonoBehaviour
    {
        public static SceneManager Instance;

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
        
        public async UniTask<SceneInstance> LoadSceneAsync(string address, UnityEngine.SceneManagement.LoadSceneMode loadMode = UnityEngine.SceneManagement.LoadSceneMode.Single, bool activateOnLoad = true)
        {
            var sceneInstance = await AddressablesManager.Instance.LoadSceneAsync(address, loadMode, activateOnLoad);
            return sceneInstance;
        }

        /// <summary>
        /// Загрузка сцены по адресу через AddressablesManager
        /// </summary>
        /// <param name="address">Адрес Addressable сцены</param>
        /// <param name="loadMode">Режим загрузки сцены</param>
        /// <param name="activateOnLoad">Активировать сцену после загрузки</param>
        public async UniTask<SceneInstance> LoadScene(string address, LoadSceneMode loadMode = LoadSceneMode.Single, bool activateOnLoad = true)
        {
            Debug.Log($"Loading scene {address}...");
            var sceneInstance = await AddressablesManager.Instance.LoadSceneAsync(address, loadMode, activateOnLoad);
            Debug.Log($"Scene {address} loaded.");
            return sceneInstance;
        }

        /// <summary>
        /// Выгрузка сцены по SceneInstance
        /// </summary>
        /// <param name="sceneInstance">Инстанс сцены для выгрузки</param>
        public async UniTask UnloadScene(SceneInstance sceneInstance)
        {
            Debug.Log($"Unloading scene {sceneInstance.Scene.name}...");
            await AddressablesManager.Instance.UnloadSceneAsync(sceneInstance);
            Debug.Log($"Scene {sceneInstance.Scene.name} unloaded.");
        }

        /// <summary>
        /// Выгрузка сцены по имени (если сцена загружена)
        /// </summary>
        /// <param name="sceneName">Имя сцены</param>
        public async UniTask UnloadSceneByName(string sceneName)
        {
            var scene = UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneName);
            if (scene.isLoaded)
            {
                Debug.Log($"Unloading scene {sceneName}...");
                var unloadOp = UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(scene);
                await unloadOp.ToUniTask();
                Debug.Log($"Scene {sceneName} unloaded.");
            }
            else
            {
                Debug.LogWarning($"Scene {sceneName} is not loaded.");
            }
        }
    }
}