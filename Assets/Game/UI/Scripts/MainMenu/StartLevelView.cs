using Game.App;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.MainMenu
{
    public class StartLevelView : MonoBehaviour
    {
        [SerializeField] private string _levelName;
        [SerializeField] private Button _button;

        private void Start()
        {
            _button = GetComponentInChildren<Button>();
            _button.onClick.AddListener(LoadLevel);
        }

        private void LoadLevel()
        {
            LoadingManager.LoadScene(_levelName);
        }
    }
}