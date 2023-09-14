using System;
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

    private ScenarioData _scenarioData;
    
    public void StartPopulating(ScenarioData scenarioDataData)
    {
        _scenarioData = scenarioDataData;
        
        downloadHandler.GetImage(scenarioDataData.scene.background, sprite =>
        {
            backgroundImage.sprite = sprite;
        });
        
        downloadHandler.GetImage(scenarioDataData.scene.avatar, sprite =>
        {
            introScreen.OnClose += OnIntroClosed;
            introScreen.InitializeScreen(sprite, scenarioDataData.scene.content.welcome);
        });
    }

    private void OnIntroClosed()
    {
        scenarioScreen.InitializeActors(_scenarioData.actors);
    }

    private void OnDestroy()
    {
        introScreen.OnClose -= OnIntroClosed;
    }
}
