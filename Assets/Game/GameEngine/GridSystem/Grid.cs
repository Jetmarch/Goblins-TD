namespace Game.GameEngine.GridSystem
{
    public class Grid
    {
        public Cell[,] Cells => _cells;
        public float XPos => _xPos;
        public float YPos => _yPos;
        public int Width => _width;
        public int Height => _height;
        public float CellSize => _cellSize;
        
        private readonly Cell[,] _cells;
        private readonly float _xPos;
        private readonly float _yPos;
        private readonly int _width;
        private readonly int _height;
        private readonly float _cellSize;

        public Grid(float xPos, float yPos, int width, int height, float cellSize)
        {
            _xPos = xPos;
            _yPos = yPos;
            _width = width;
            _height = height;
            _cellSize = cellSize;
            _cells = new Cell[width, height];
            
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    var cell = new Cell(x, y, _cellSize);
                    _cells[x, y] = cell;
                }
            }
        }

        public Cell GetCellByPosition(float worldX, float worldY)
        {
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    var currentCell = _cells[x, y];
                    var cellSize = currentCell.Size;
                    var cellXPos = (_xPos + currentCell.XPos * currentCell.Size);
                    var cellYPos = (_yPos + currentCell.YPos * currentCell.Size);

                    if (CheckBounds(cellXPos, cellYPos, cellSize, cellSize, worldX, worldY))
                    {
                        return currentCell;
                    }
                }
            }
            return default;
        }
        
        //TODO: Move to extensions or utils
        private static bool CheckBounds(float aX, float aY, float aWidth, float aHeight, float bX, float bY)
        {
            return bX > aX && bX < aX + aWidth
                           && bY > aY && bY < aY + aHeight;
        }
    }
}