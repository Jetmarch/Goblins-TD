namespace Modules.Core.GameLoop
{
    public interface ILateUpdateListener : IGameLoopListener
    {
        void OnLateUpdate(float deltaTime);
    }
}