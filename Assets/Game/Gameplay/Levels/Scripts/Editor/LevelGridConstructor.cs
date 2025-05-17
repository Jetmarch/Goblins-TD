using System;
using UnityEditor;
using UnityEngine;

namespace Game.Gameplay.Levels.Editor
{
    public class LevelGridConstructor : EditorWindow
    {
        private LevelGrid _levelGrid;

        private int _gridWidth = 9;
        private int _gridHeight = 17;
        private float _cellSize = 0.45f;
        private Vector2 _cellGap = Vector2.zero;
        
        private readonly string _basePath = "Assets/Game/Gameplay/Levels/Content/Configs";
        
        
        //TODO: Add possible paths
        
        [MenuItem("Levels/Level Grid Constructor")]
        public static void ShowWindow()
        {
            GetWindow<LevelGridConstructor>("Level Grid Constructor");
        }

        private void OnGUI()
        {
            DrawHeader();

            DrawControlsButtons();
            
            if (_levelGrid != null)
            {
                DrawGrid();
            }
        }

        private void DrawHeader()
        {
            GUILayout.Label("Level Grid Settings", EditorStyles.boldLabel);

            _gridWidth = EditorGUILayout.IntField("Width", _gridWidth);
            if (_gridWidth <= 0)
            {
                _gridWidth = 1; 
                EditorGUILayout.HelpBox("Width cannot be negative. Setting to 0.", MessageType.Warning);
            }

            _gridHeight = EditorGUILayout.IntField("Height", _gridHeight);
            if (_gridHeight <= 0)
            {
                _gridHeight = 1; 
                EditorGUILayout.HelpBox("Height cannot be negative. Setting to 0.", MessageType.Warning);
            }

            _cellSize = EditorGUILayout.FloatField("Cell size", _cellSize);
            if (_cellSize <= 0)
            {
                _cellSize = 0.1f;
                EditorGUILayout.HelpBox("Cell size cannot be negative. Setting to 0.", MessageType.Warning);
            }

            _cellGap = EditorGUILayout.Vector2Field("Cell gap", _cellGap);
            if (_cellGap.x < 0)
            {
                _cellGap.x = 0;
            }
            if (_cellGap.y < 0)
            {
                _cellGap.y = 0;
            }
        }

        private void DrawControlsButtons()
        {
            GUILayout.BeginHorizontal();
            
            GUIStyle saveButtonStyle = new GUIStyle(GUI.skin.button);
            saveButtonStyle.normal.textColor = Color.white; // Цвет текста
            GUI.color = ColorUtility.TryParseHtmlString( "#b0323a", out var saveButtonColor) ? saveButtonColor : Color.yellow;
            if (GUILayout.Button("Save Level Grid", saveButtonStyle))
            {
                SaveLeveGrid();
            }

            GUIStyle loadButtonStyle = new GUIStyle(GUI.skin.button);
            loadButtonStyle.normal.textColor = Color.white; // Цвет текста
            GUI.color = ColorUtility.TryParseHtmlString( "#f0d8c3", out var loadButtonColor) ? loadButtonColor : Color.yellow;
            if (GUILayout.Button("Load Level Grid", loadButtonStyle))
            {
                LoadLeveGrid();
            }

            GUIStyle createButtonStyle = new GUIStyle(GUI.skin.button);
            createButtonStyle.normal.textColor = Color.white; // Цвет текста
            GUI.color = ColorUtility.TryParseHtmlString( "#c87c0c", out var createButtonColor) ? createButtonColor : Color.yellow;
            if (GUILayout.Button("Create Level Grid", createButtonStyle))
            {
                CreateLevelGrid();
            }
            GUI.color = Color.white;
            
            GUILayout.EndHorizontal();
        }

        private void SaveLeveGrid()
        {
            var path = EditorUtility.SaveFilePanel("Save Level Grid Config", _basePath, "NewLevelGridConfig", "asset");

            if (string.IsNullOrEmpty(path))
            {
                Debug.LogWarning("Save operation canceled.");
                return; 
            }
            
            path = FileUtil.GetProjectRelativePath(path);
            var levelGridConfig = CreateInstance<LevelGridConfig>();

            levelGridConfig.SetLevelGrid(_levelGrid);
            EditorUtility.SetDirty(levelGridConfig);
            
            AssetDatabase.CreateAsset(levelGridConfig, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log("Level Grid saved to " + path);
        }

        private void LoadLeveGrid()
        {
            var path = EditorUtility.OpenFilePanel("Load Level Grid Config", _basePath, "asset");

            if (string.IsNullOrEmpty(path))
            {
                Debug.LogWarning("Load operation canceled.");
                return;
            }

            path = FileUtil.GetProjectRelativePath(path);

            var levelGridConfig = AssetDatabase.LoadAssetAtPath<LevelGridConfig>(path);
            if (levelGridConfig == null)
            {
                Debug.LogError("Failed to load LevelGridConfig from path: " + path);
                return;
            }

            _levelGrid = new LevelGrid(levelGridConfig.LevelGrid);
        }

        private void CreateLevelGrid()
        {
            if(_gridWidth <= 0 || _gridHeight <= 0)
            {
                throw new ApplicationException("Grid width and height must be greater than 0.");
            }
            
            var cells = new LevelCell[_gridWidth * _gridHeight];
            
            for (int i = 0; i < _gridWidth; i++)
            {
                for (int j = 0; j < _gridHeight; j++)
                {
                    cells[i + (j * _gridWidth)] = new LevelCell(i, j, _cellSize, LevelCellType.Buildable);
                }
            }
            _levelGrid = new LevelGrid(_gridWidth, _gridHeight, _cellSize, _cellGap, cells);
        }
        
        private void DrawGrid()
        {
            for (int y = 0; y < _levelGrid.Height; y++)
            {
                GUILayout.BeginHorizontal(); 
                for (int x = 0; x < _levelGrid.Width; x++)
                {
                    if (_levelGrid.GetCell(x, y) is LevelCell cell)
                    {
                        var cellColor = GetCellColor(cell.Type);
                        GUI.color = cellColor;

                        if (GUILayout.Button(GUIContent.none, GUILayout.Width(50), GUILayout.Height(50)))
                        {
                            ChangeCellType(cell);
                        }
                    }
                    else
                    {
                        GUI.color = Color.black;
                        if (GUILayout.Button(GUIContent.none, GUILayout.Width(50), GUILayout.Height(50)))
                        {
                            Debug.LogWarning($"Cell at {x}, {y} is not LevelCell");
                        }
                    }
                }
                GUILayout.EndHorizontal(); 
            }
            GUI.color = Color.white; 
        }
        
        private void ChangeCellType(LevelCell cell)
        {
            switch (cell.Type)
            {
                case LevelCellType.Buildable:
                    cell.SetType(LevelCellType.Walkable);
                    break;
                case LevelCellType.Walkable:
                    cell.SetType(LevelCellType.Buildable);
                    break;
            }
        }
        
        private Color GetCellColor(LevelCellType type)
        {
            switch (type)
            {
                case LevelCellType.Buildable:
                    return Color.green;
                case LevelCellType.Walkable:
                    return Color.red;
                default:
                    return Color.white; 
            }
        }
    }
}