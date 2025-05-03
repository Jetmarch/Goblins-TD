using UnityEngine;

namespace Game.App
{
    public class DontDestroyOnLoadMark : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}