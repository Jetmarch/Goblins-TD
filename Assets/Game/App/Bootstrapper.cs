using UnityEngine;


namespace Game.App
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private string _mainMenuSceneName = "MainMenu";
        
        private void Start()
        {
            LoadingManager.LoadScene(_mainMenuSceneName);
        }
    }
}