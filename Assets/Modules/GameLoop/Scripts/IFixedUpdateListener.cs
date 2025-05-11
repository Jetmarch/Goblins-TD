namespace Modules.Core.GameLoop
{
    public interface IFixedUpdateListener : IGameLoopListener
    {
        void OnFixedUpdate(float deltaTime);
    }
}