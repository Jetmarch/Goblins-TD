namespace Modules.Core.GameLoop
{
    public interface IPauseListener : IGameLoopListener
    {
        void OnPause();
        void OnResume();
    }
}