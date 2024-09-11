using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class NextButton : MonoBehaviour
    {
        public static NextButton Instance; 
        
        private Button _button;
        
        private void Awake()
        {
            Instance = this;
            _button = GetComponent<Button>();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void SetInteraction(Action action)
        {
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(() => action?.Invoke());
        }
    }
}