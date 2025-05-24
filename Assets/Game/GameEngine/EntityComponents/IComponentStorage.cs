namespace Game.GameEngine.EntityComponents
{
    public interface IComponentStorage
    {
        IComponent Get(int componentId);
        void Set(int componentId, IComponent component);
    }
}