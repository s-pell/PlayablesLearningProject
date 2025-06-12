using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.UIElements;

namespace Game
{
    public class SceneManager : MonoBehaviour
    {
        public static SceneManager Instance;
        private LevelTilesController _levelTilesController = null;

        //private SceneInstance? _mainScene;
        private List<SceneInstance?> _additionalScenes;

        public SceneInstance? MainScene
        {
            get;
            private set;
        }

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

        public void SelectLevel(LevelTilesConfig config)
        {
            _levelTilesController ??= new LevelTilesController(config);
            _levelTilesController.SetAnotherLevel(config).Forget();
        }

        public async UniTask<SceneInstance> LoadSceneAsync(string address, UnityEngine.SceneManagement.LoadSceneMode loadMode = UnityEngine.SceneManagement.LoadSceneMode.Single, bool activateOnLoad = true)
        {
            if (loadMode == LoadSceneMode.Single)
            {
                return await LoadSingle(address, activateOnLoad);
            }
            return await LoadAdditive(address, activateOnLoad);
        }
        
        private async UniTask<SceneInstance> LoadSingle(string address, bool activateOnLoad = true)
        {
            MainScene?.UnloadScene().Forget();
            if (_additionalScenes != null)
            {
                foreach (var scene in _additionalScenes)
                {
                    scene?.UnloadScene().Forget();
                }

                _additionalScenes.Clear();
            }

            var sceneInstance = await AddressablesManager.Instance.LoadSceneAsync(address, LoadSceneMode.Single, activateOnLoad);
            MainScene = sceneInstance;
            return sceneInstance;
        }
        
        private async UniTask<SceneInstance> LoadAdditive(string address, bool activateOnLoad = true)
        {
            var sceneInstance = await AddressablesManager.Instance.LoadSceneAsync(address, LoadSceneMode.Additive, activateOnLoad);
            _additionalScenes.Add(sceneInstance);
            return sceneInstance;
        }


        public async UniTask<SceneInstance> LoadScene(string address, LoadSceneMode loadMode = LoadSceneMode.Single, bool activateOnLoad = true)
        {
            Debug.Log($"Loading scene {address}...");
            var sceneInstance = await AddressablesManager.Instance.LoadSceneAsync(address, loadMode, activateOnLoad);
            Debug.Log($"Scene {address} loaded.");
            return sceneInstance;
        }
        
        public async UniTask UnloadScene(SceneInstance sceneInstance)
        {
            Debug.Log($"Unloading scene {sceneInstance.Scene.name}...");
            await AddressablesManager.Instance.UnloadSceneAsync(sceneInstance);
            Debug.Log($"Scene {sceneInstance.Scene.name} unloaded.");
        }
        
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

        public void ShowTile(int col, int row)
        {
            _levelTilesController?.ShowTile(col, row).Forget();
        }

        public void HideTile(int col, int row)
        {
            _levelTilesController?.HideTile(col, row).Forget();
        }
    }

    public static class ScenesExtensions
    {
        public static async UniTask UnloadScene(this SceneInstance sceneInstance)
        {
            Debug.Log($"Unloading scene {sceneInstance.Scene.name}...");
            await AddressablesManager.Instance.UnloadSceneAsync(sceneInstance);
            Debug.Log($"Scene {sceneInstance.Scene.name} unloaded.");
        }
        
    }
}