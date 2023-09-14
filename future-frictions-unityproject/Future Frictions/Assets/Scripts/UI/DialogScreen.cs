using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogScreen : BaseScreen
{
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
}
