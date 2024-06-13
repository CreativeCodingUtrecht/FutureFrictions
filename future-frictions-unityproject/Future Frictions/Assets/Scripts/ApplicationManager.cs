using UnityEngine;

public class ApplicationManager : MonoBehaviour
{
    [SerializeField] private DataManager dataManager;
    [SerializeField] private ScenarioManager scenarioManager;

    [SerializeField] private BaseScreen[] allScreens;

    private void Awake()
    {
        allScreens = FindObjectsByType<BaseScreen>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        
        dataManager.OnLoadComplete += LoadCompleted;
        dataManager.DownloadScenario();
    }

    private void LoadCompleted(ScenarioData scenarioDataData)
    {
        // Start the building of the app and open the first screen
        scenarioManager.ShowTitle(scenarioDataData);
    }

    private void OnDestroy()
    {
        dataManager.OnLoadComplete -= LoadCompleted;
    }

    public void Restart()
    {
        CloseAllScreens();
        scenarioManager.StartPopulating();
    }

    public void CloseAllScreens()
    {
        foreach (var screen in allScreens)
        {
            screen.Close();
        }
    }
}