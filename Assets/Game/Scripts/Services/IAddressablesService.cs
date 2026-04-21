using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Game.Services
{
    public interface IAddressablesService {
        UniTask<SceneInstance> LoadSceneAsync(string address, LoadSceneMode loadMode = LoadSceneMode.Single, bool activateOnLoad = true);
        UniTask UnloadSceneAsync(SceneInstance sceneInstance);
        UniTask InitializeAsync();
        UniTask<GameObject> InstantiateAsync(string address, Vector3 position);
        UniTask<GameObject> InstantiateAsync(string address, Vector3 position, Quaternion rotation);
        UniTask<GameObject> InstantiateAsync(AssetReference prefabReference);
        UniTask<GameObject> LoadPrefabAsync(string address);
        UniTask<Texture2D> LoadTextureAsync(string address);
        UniTask LoadAudioAsync(string address);
        void Release();
        // void ReleaseInstance(GameObject instance);
        // int GetInstanceID();
        // void EnsureRunningOnMainThread();
        // IntPtr GetCachedPtr();
        // string GetName();
        // void SetName(string name);
        // void MarkDirty();
    }
}