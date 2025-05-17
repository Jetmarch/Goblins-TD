namespace Game.GameEngine.GridSystem
{
    public interface ICell
    {
        public int GridX { get; }
        public int GridY { get; }
        
        public float Size { get; }
    }
}