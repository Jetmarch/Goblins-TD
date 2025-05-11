namespace Modules.Core.GameLoop
{
    public interface IUpdateListener : IGameLoopListener
    {
        void OnUpdate(float deltaTime);
    }
}