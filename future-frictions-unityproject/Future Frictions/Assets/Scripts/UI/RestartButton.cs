using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class RestartButton : MonoBehaviour
    {
        [SerializeField] private ApplicationManager applicationManager;
        [SerializeField] private Button restartButton;
        
        private void Awake()
        {
            restartButton.onClick.AddListener(() => applicationManager.Restart());
        }
    }
}
