using System;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreen : BaseScreen
{
    [SerializeField]
    private TMP_Text titleText;
    
    [SerializeField]
    private TMP_Text frictionalStatementText;

    [SerializeField]
    private Button startButton;
    
    [SerializeField]
    private GameObject restartButton;

    private ScenarioData _scenarioData;
    private ScenarioManager _scenarioManager;

    private void Awake()
    {
        NextButton.Instance.Hide();
    }

    public void Initialize(ScenarioData scenarioData, ScenarioManager scenarioManager)
    {
        _scenarioData = scenarioData;
        _scenarioManager = scenarioManager;

        titleText.text = _scenarioData.name;

        if (!string.IsNullOrEmpty(_scenarioData.provocativestatement))
        {
            frictionalStatementText.text = _scenarioData.provocativestatement;
        }
        else
        {
            frictionalStatementText.transform.parent.gameObject.SetActive(false);
        }
        
        startButton.onClick.AddListener(Close);

        restartButton.SetActive(false);
        Open();
    }

    public override void Close()
    {
        base.Close();

        restartButton.SetActive(true);
        _scenarioManager.StartPopulating();
    }
}
