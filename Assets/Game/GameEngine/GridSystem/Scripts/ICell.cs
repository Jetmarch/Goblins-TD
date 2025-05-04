namespace Game.GameEngine.GridSystem
{
    public interface ICell
    {
        public int GridX { get; }
        public int GridY { get; }
        
        public float WorldX { get; }
        public float WorldY { get; }
        public float Size { get; }
        public bool IsWalkable { get; }
        
        public void SetBusy(bool isBusy);
    }
}