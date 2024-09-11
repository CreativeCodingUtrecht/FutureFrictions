using UI;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioManager : MonoBehaviour
{
    [SerializeField]
    private DownloadHandler downloadHandler;
    
    [Header("References")]
    [SerializeField]
    private Image backgroundImage;
    
    [SerializeField] 
    private IntroScreen introScreen;

    [SerializeField]
    private ScenarioScreen scenarioScreen;

    [SerializeField]
    private Friction friction;

    [SerializeField]
    private TitleScreen titleScreen;
    
    private ScenarioData _scenarioData;
    private Scenario _scenario;

    public void ShowTitle(ScenarioData scenarioData)
    {
        _scenarioData = scenarioData;
        
        UpdateBackground(_scenarioData.collage.present.definition.background);
        titleScreen.Initialize(scenarioData, this);
    }
    
    public void StartPopulating()
    {
        ResetScenarios();

        // Set background to the present background
        UpdateBackground(_scenarioData.collage.present.definition.background);
        
        // Set the friction on the intro screen with the avatar
        downloadHandler.GetImage(_scenarioData.friction.avatarSrc, (sprite, hasError) =>
        {
            introScreen.OnClose += OnIntroClosed;
            introScreen.InitializeScreen(sprite, _scenarioData.friction.frictionalstatement);
        });
    }

    public void PopulateFrictionalObjects()
    {
        scenarioScreen.InitializeFrictionalObjects(_scenarioData);
    }
    
    public void PopulateTheFuture()
    {
        UpdateBackground(_scenarioData.collage.future.definition.background);
        
        scenarioScreen.InitializeScenario(_scenarioData, TimeFrame.Future);
    }
    
    private void UpdateBackground(string url)
    {
        if (url == null || string.IsNullOrEmpty(url)) return;
        
        downloadHandler.GetImage(url, (sprite, hasError) =>
        {
            backgroundImage.sprite = sprite;
        });
    }

    private void OnIntroClosed()
    {
        scenarioScreen.InitializeScenario(_scenarioData, TimeFrame.Present);
    }

    private void OnDestroy()
    {
        introScreen.OnClose -= OnIntroClosed;
    }

    private void ResetScenarios()
    {
        friction.ResetFriction();
        scenarioScreen.Close();
    }
}
