using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class IntroScreen : BaseScreen
{
    public UnityAction OnClose;
    
    [SerializeField]
    private Image avatarImage;
    
    [SerializeField]
    private TMP_Text mainText;

    public void InitializeScreen(Sprite avatar, string text)
    {
        avatarImage.sprite = avatar;
        mainText.text = text;
        
        Open();
    }

    public override void Close()
    {
        base.Close();
        
        OnClose?.Invoke();
    }
}
