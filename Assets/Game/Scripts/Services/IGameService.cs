using Cysharp.Threading.Tasks;

namespace Game.Services
{
    public interface IGameService
    {
        void ChangeState(GameService.GameState newState);
        UniTaskVoid LoadMainMenu();
        UniTaskVoid LoadGameScene();
        void Unload(string name);
    }
}