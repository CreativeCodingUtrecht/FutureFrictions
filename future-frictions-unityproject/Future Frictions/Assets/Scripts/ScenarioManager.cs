using System;
using System.Collections.Generic;
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
    
    private ScenarioData _scenarioData;

    private readonly List<string> _interactedActors = new();

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

    public void StoreActorInteracted(string actorId)
    {
        if (!_interactedActors.Contains(actorId))
        {
            _interactedActors.Add(actorId);
        }
    }

    public void CheckInteractionsDone()
    {
        if (_interactedActors.Count >= 3)
        {
            friction.Initialize(_scenarioData.friction);
        }
    }
    
    public void QuestionAnswered()
    {
        // Set new state
        // Update actors
        // Set choice images in the scene
    }

    private void OnDestroy()
    {
        introScreen.OnClose -= OnIntroClosed;
    }
}
