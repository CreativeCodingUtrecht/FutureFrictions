using TMPro;
using UnityEngine;

public class EndScreen : BaseScreen
{
    [SerializeField]
    private GameObject authorTitleText;
    
    [SerializeField]
    private TMP_Text authorNameText;

    public void Initialize(ScenarioData scenarioData)
    {
        var author = scenarioData.author;

        if (string.IsNullOrEmpty(author))
        {
            authorTitleText.SetActive(false);
            authorNameText.gameObject.SetActive(false);
        }
        else
        {
            authorTitleText.SetActive(true);
            authorNameText.gameObject.SetActive(true);
            
            authorNameText.text = author;
        }
        
        Open();
    }
}
