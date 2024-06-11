using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class QuestionScreen : BaseScreen
{
    [Header("References")]
    [SerializeField] private DownloadHandler downloadHandler;
    [SerializeField] private ResultScreen resultScreen;
    [SerializeField] private ScenarioManager scenarioManager;
    
    [Header("UI Items")]
    [SerializeField] private Image questionAvatar;
    [SerializeField] private TMP_Text questionText;

    [SerializeField] private Button answerButton;

    private Options _currentOption;
    
    public void Initialize(ScenarioData sceneData, FrictionData frictionData)
    {
        _currentOption = Options.None;
        
        questionText.text = frictionData.frictionalstatement;
        
        downloadHandler.GetImage(sceneData.friction.avatar, (avatarSprite, hasError) =>
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
        resultScreen.InitializeResults(Options.A);
        Close();
    }
}

public enum Options
{
    None,
    A,
    B,
    C
}