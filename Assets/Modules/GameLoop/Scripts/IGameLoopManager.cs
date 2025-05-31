namespace Modules.Core.GameLoop
{
    public interface IGameLoopManager
    {
        void AddListener(IGameLoopListener listener);
        void RemoveListener(IGameLoopListener listener);
        void PauseGame();
        void ResumeGame();
        void StartGame();
        void FinishGame();
    }
}