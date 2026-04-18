using Cysharp.Threading.Tasks;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Game.Services
{
    public interface ISceneService
    {
        void SelectLevel(LevelTilesConfig config);
        UniTask<SceneInstance> LoadSceneAsync(string address, UnityEngine.SceneManagement.LoadSceneMode loadMode = UnityEngine.SceneManagement.LoadSceneMode.Single, bool activateOnLoad = true);
        UniTask<SceneInstance> LoadSingle(string address, bool activateOnLoad = true);
        UniTask<SceneInstance> LoadAdditive(string address, bool activateOnLoad = true);
        UniTask<SceneInstance> LoadScene(string address, LoadSceneMode loadMode = LoadSceneMode.Single, bool activateOnLoad = true);
        UniTask UnloadScene(SceneInstance sceneInstance);
        UniTask UnloadSceneByName(string sceneName);
        void ShowTile(int col, int row);
        void HideTile(int col, int row);
    }
}