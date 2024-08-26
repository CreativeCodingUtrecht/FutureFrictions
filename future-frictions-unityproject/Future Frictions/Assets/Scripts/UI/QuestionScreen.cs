using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class QuestionScreen : BaseScreen
{
    [Header("References")]
    [SerializeField] private DownloadHandler downloadHandler;
    [SerializeField] private ScenarioManager scenarioManager;
    
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

        NextButton.Instance.SetInteraction(AnswerQuestion);
        NextButton.Instance.Show();
        
        Open();
    }
    
    private void AnswerQuestion()
    {
        scenarioManager.PopulateTheFuture();
        
        Close();
        NextButton.Instance.Hide();
    }
}