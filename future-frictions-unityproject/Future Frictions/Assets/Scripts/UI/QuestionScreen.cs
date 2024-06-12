using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionScreen : BaseScreen
{
    [Header("References")]
    [SerializeField] private DownloadHandler downloadHandler;
    
    [Header("UI Items")]
    [SerializeField] private Image questionAvatar;
    [SerializeField] private TMP_Text questionText;

    [SerializeField] private Button answerButton;
    
    public void Initialize(ScenarioData scenarioData)
    {
        questionText.text = scenarioData.whatIf.question;
        
        downloadHandler.GetImage(scenarioData.friction.avatar, (avatarSprite, hasError) =>
        {
            if (avatarSprite == null)
            {
                questionAvatar.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                questionAvatar.sprite = avatarSprite;
            }
        });

        answerButton.interactable = true;
        answerButton.onClick.AddListener(AnswerQuestion);
        
        Open();
    }
    
    private void AnswerQuestion()
    {
        Close();
    }
}