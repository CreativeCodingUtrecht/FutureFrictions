using TMPro;
using UI;
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
        if (avatar == null)
        {
            avatarImage.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            avatarImage.sprite = avatar;
        }
        mainText.text = text;
     
        NextButton.Instance.Show();
        NextButton.Instance.SetInteraction(() =>
        {
            NextButton.Instance.Hide();
            Close();
        });
        
        Open();
    }

    public override void Close()
    {
        base.Close();
        
        OnClose?.Invoke();
    }
}
