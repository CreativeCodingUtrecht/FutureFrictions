using UnityEngine;

public class ApplicationManager : MonoBehaviour
{
    [SerializeField]
    private DataManager dataManager;

    [SerializeField]
    private ScenarioManager scenarioManager;
    
    private void Awake()
    {
        dataManager.OnLoadComplete += LoadCompleted;
        dataManager.DownloadScenario();
    }

    private void LoadCompleted(ScenarioData scenarioDataData)
    {
        // Start the building of the app and open the first screen
        scenarioManager.StartPopulating(scenarioDataData);
    }

    private void OnDestroy()
    {
        dataManager.OnLoadComplete -= LoadCompleted;
    }
}