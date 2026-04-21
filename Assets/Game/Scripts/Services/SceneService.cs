using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.ResourceManagement.ResourceProviders;
using Zenject;

namespace Game.Services {
    public class SceneService : ISceneService{
        private LevelTilesController _levelTilesController;
        private List<SceneInstance?> _additionalScenes;
        
        private readonly DiContainer _container;
        private readonly IAddressablesService _addressablesService;

        public SceneService(DiContainer container, IAddressablesService addressablesService){
            _container = container;
            _addressablesService = addressablesService;
        }

        private SceneInstance? MainScene { get; set; }

        public void SelectLevel(LevelTilesConfig config) {
            _levelTilesController ??= new LevelTilesController(config, _addressablesService);
            _levelTilesController.SetAnotherLevel(config).Forget();
        }

        public async UniTask<SceneInstance> LoadSceneAsync(string address, LoadSceneMode loadMode = LoadSceneMode.Single, bool activateOnLoad = true) {
            if (loadMode == LoadSceneMode.Single) {
                return await LoadSingle(address, activateOnLoad);
            }
            return await LoadAdditive(address, activateOnLoad);
        }

        public async UniTask<SceneInstance> LoadSingle(string address, bool activateOnLoad = true) {
            MainScene?.UnloadScene(_container).Forget();
            if (_additionalScenes != null) {
                foreach (var scene in _additionalScenes) {
                    scene?.UnloadScene(_container).Forget();
                }

                _additionalScenes.Clear();
            }

            var sceneInstance = await _addressablesService.LoadSceneAsync(address, LoadSceneMode.Single, activateOnLoad);
            MainScene = sceneInstance;
            return sceneInstance;
        }

        public async UniTask<SceneInstance> LoadAdditive(string address, bool activateOnLoad = true) {
            var sceneInstance = await _addressablesService.LoadSceneAsync(address, LoadSceneMode.Additive, activateOnLoad);
            _additionalScenes.Add(sceneInstance);
            return sceneInstance;
        }

        public async UniTask<SceneInstance> LoadScene(string address, LoadSceneMode loadMode = LoadSceneMode.Single, bool activateOnLoad = true) {
            Debug.Log($"Loading scene {address}...");
            var sceneInstance = await _addressablesService.LoadSceneAsync(address, loadMode, activateOnLoad);
            Debug.Log($"Scene {address} loaded.");
            return sceneInstance;
        }
        
        public async UniTask UnloadScene(SceneInstance sceneInstance) {
            Debug.Log($"Unloading scene {sceneInstance.Scene.name}...");
            await _addressablesService.UnloadSceneAsync(sceneInstance);
            Debug.Log($"Scene {sceneInstance.Scene.name} unloaded.");
        }
        
        public async UniTask UnloadSceneByName(string sceneName) {
            var scene = SceneManager.GetSceneByName(sceneName);
            if (scene.isLoaded) {
                Debug.Log($"Unloading scene {sceneName}...");
                var unloadOp = SceneManager.UnloadSceneAsync(scene);
                await unloadOp.ToUniTask();
                Debug.Log($"Scene {sceneName} unloaded.");
            }
            else {
                Debug.LogWarning($"Scene {sceneName} is not loaded.");
            }
        }

        public void ShowTile(int col, int row) {
            _levelTilesController?.ShowTile(col, row).Forget();
        }

        public void HideTile(int col, int row) {
            _levelTilesController?.HideTile(col, row).Forget();
        }
    }
}