using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Game.Services
{
    public interface IAddressablesService
    {
        UniTask<SceneInstance> LoadSceneAsync(string address, LoadSceneMode loadMode = LoadSceneMode.Single, bool activateOnLoad = true);
        UniTask UnloadSceneAsync(SceneInstance sceneInstance);
        UniTask<GameObject> InstantiateAsync(string address, Vector3 position);
        UniTask<GameObject> InstantiateAsync(string address, Vector3 position, Quaternion rotation);
        UniTask<GameObject> InstantiateAsync(AssetReference prefabReference);
        // void ReleaseInstance(GameObject instance);
        // int GetInstanceID();
        // void EnsureRunningOnMainThread();
        // IntPtr GetCachedPtr();
        // string GetName();
        // void SetName(string name);
        // void MarkDirty();
    }
}