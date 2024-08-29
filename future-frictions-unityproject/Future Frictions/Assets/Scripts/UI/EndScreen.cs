using TMPro;
using UnityEngine;

public class EndScreen : BaseScreen
{
    [SerializeField]
    private TMP_Text authorNameText;

    [SerializeField]
    private TMP_Text statementText;
    
    [SerializeField]
    private GameObject authorGroup;

    [SerializeField]
    private GameObject statementGroup;

    public void Initialize(ScenarioData scenarioData)
    {
        var author = scenarioData.author;
        if (string.IsNullOrEmpty(author))
        {
            authorGroup.SetActive(false);
        }
        else
        {
            authorGroup.SetActive(true);
            authorNameText.text = author;
        }
        
        var statement = scenarioData.provocativestatement;
        if (string.IsNullOrEmpty(statement))
        {
            statementGroup.SetActive(false);
        }
        else
        {
            statementGroup.SetActive(true);
            statementText.text = statement;
        }
        
        Open();
    }
}
