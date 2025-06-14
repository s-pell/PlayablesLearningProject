using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Game
{
    public class AssetLoader<T> where T: class
    {
        private string _address; 
        private T _loaded;
        private AsyncOperationHandle<T> _handle;

        public AssetLoader(string address)
        {
            _address = address;
        }
        public async UniTask<T> LoadAsync()
        {
            _handle = Addressables.LoadAssetAsync<T>(_address);
            _loaded = await _handle.Task;
            if (_loaded == null)
            {
                Debug.LogError($"Не удалось загрузить ассет по адресу {_address}");
            }

            return _loaded;
        }
    
        public void Release()
        {
            if (_handle.IsValid())
            {
                Addressables.Release(_handle);
                _loaded = null;
            }
        }
    }
}


