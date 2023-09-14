using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogScreen : BaseScreen
{
    [SerializeField]
    private Image avatarImage;
    
    [SerializeField]
    private TMP_Text mainText;

    private Action _closeAction;

    public void InitializeScreen(Sprite avatar, string text, Action closeAction = null)
    {
        _closeAction = closeAction;
        
        avatarImage.sprite = avatar;
        mainText.text = text;
        
        Open();
    }

    public override void Close()
    {
        base.Close();
        
        _closeAction?.Invoke();
    }
}
