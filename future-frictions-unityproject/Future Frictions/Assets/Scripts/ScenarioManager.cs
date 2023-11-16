using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioManager : MonoBehaviour
{
    public ApplicationState ApplicationState => _currentState;
    public ScenarioData ScenarioData => _scenarioData;

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

    private ApplicationState _currentState;
    
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
            _currentState = ApplicationState.Intro;
        });
    }

    private void OnIntroClosed()
    {
        scenarioScreen.InitializeActors(_scenarioData.actors);
        _currentState = ApplicationState.BeforeInteractions;
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
        if (_currentState != ApplicationState.BeforeInteractions) return;
        if (_interactedActors.Count < 3) return;
        
        friction.Initialize(_scenarioData.friction);
        _currentState = ApplicationState.Question;
    }

    public void SetScenarioInFront()
    {
        scenarioScreen.transform.SetAsLastSibling();
    }
    
    public void SetUIInFront()
    {
        scenarioScreen.transform.SetAsLastSibling();
    }
    
    // Set this from the resultscreen
    public void QuestionAnswered()
    {
        Debug.Log("Question answered");
        _currentState = ApplicationState.Results;
    }

    private void OnDestroy()
    {
        introScreen.OnClose -= OnIntroClosed;
    }
}

public enum ApplicationState {
    Intro,
    BeforeInteractions,
    Question,
    Results,
    End
}
