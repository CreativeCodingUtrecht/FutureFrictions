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

    // [SerializeField] private TMP_Text optionAText;
    // [SerializeField] private Image optionAImage;
    // [SerializeField] private Toggle optionAToggle;
    //
    // [SerializeField] private TMP_Text optionBText;
    // [SerializeField] private Image optionBImage;
    // [SerializeField] private Toggle optionBToggle;
    //
    // [SerializeField] private TMP_Text optionCText;
    // [SerializeField] private Image optionCImage;
    // [SerializeField] private Toggle optionCToggle;

    [SerializeField] private Button answerButton;

    private Options _currentOption;
    
    public void Initialize(ScenarioSceneData sceneData, FrictionData frictionData)
    {
        _currentOption = Options.None;
        
        questionText.text = frictionData.content.before;
        
        downloadHandler.GetImage(sceneData.avatar, (avatarSprite, hasError) =>
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
        
        // optionAText.text = frictionData.options.a.content;
        // optionBText.text = frictionData.options.b.content;
        // optionCText.text = frictionData.options.c.content;
        //
        // downloadHandler.GetImage(frictionData.options.a.avatar, (avatarSprite, hasError) =>
        // {
        //     optionAImage.sprite = avatarSprite;
        // });
        //
        // downloadHandler.GetImage(frictionData.options.b.avatar, (avatarSprite, hasError) =>
        // {
        //     optionBImage.sprite = avatarSprite;
        // });
        //
        // downloadHandler.GetImage(frictionData.options.c.avatar, (avatarSprite, hasError) =>
        // {
        //     optionCImage.sprite = avatarSprite;
        // });
        //
        // answerButton.interactable = false;

        // optionAToggle.onValueChanged.AddListener(state => {
        //     if (state)
        //     {
        //         SetSelectedOption(Options.A);
        //     } 
        // });
        //
        // optionBToggle.onValueChanged.AddListener(state => {
        //     if (state)
        //     {
        //         SetSelectedOption(Options.B);
        //     } 
        // });
        //
        // optionCToggle.onValueChanged.AddListener(state => {
        //     if (state)
        //     {
        //         SetSelectedOption(Options.C);
        //     } 
        // });

        answerButton.interactable = true;
        answerButton.onClick.AddListener(AnswerQuestion);
        
        Open();
    }

    // public void SetSelectedOption(Options option)
    // {
    //     // Enable answer button
    //     answerButton.interactable = true;
    //
    //     _currentOption = option;
    //     
    //     Debug.Log($"Answer: {option}");
    // }
    
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