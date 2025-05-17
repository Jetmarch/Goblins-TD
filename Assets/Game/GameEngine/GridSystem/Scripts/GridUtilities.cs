namespace Game.GameEngine.GridSystem
{
    public static class GridUtilities
    {
        public static bool CheckGridBounds(int gridX, int gridY, int width, int height)
        {
            if (gridX < 0 || gridX >= width || gridY < 0 || gridY >= height) return false;
            return true;
        }
    }
}