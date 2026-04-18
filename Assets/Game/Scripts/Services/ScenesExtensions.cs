using System.ComponentModel;
using Cysharp.Threading.Tasks;
using Game.Services;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders;
using Zenject;

namespace Game
{
    public static class ScenesExtensions
    {
        public static async UniTask UnloadScene(this SceneInstance sceneInstance, DiContainer container)
        {
            Debug.Log($"Unloading scene {sceneInstance.Scene.name}...");
            var addressablesService = container.Resolve<IAddressablesService>();
            await addressablesService.UnloadSceneAsync(sceneInstance);
            Debug.Log($"Scene {sceneInstance.Scene.name} unloaded.");
        }
        
    }
}