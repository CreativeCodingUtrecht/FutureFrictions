using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioManager : MonoBehaviour
{
    public ApplicationState ApplicationState => _currentState;
    public ScenarioData ScenarioData => _scenarioData;

    [SerializeField]
    private DownloadHandler downloadHandler;

    [SerializeField] private ResultScreen resultScreen;
    
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
        ResetScenarios();
        
        _scenarioData = scenarioDataData;

        UpdateBackground(scenarioDataData.scene.background);
        
        downloadHandler.GetImage(scenarioDataData.scene.avatar, (sprite, hasError) =>
        {
            introScreen.OnClose += OnIntroClosed;
            introScreen.InitializeScreen(sprite, scenarioDataData.scene.content.welcome);
            _currentState = ApplicationState.Intro;
        });
    }

    public void UpdateBackground(string url)
    {
        if (url == null || string.IsNullOrEmpty(url)) return;
        
        downloadHandler.GetImage(url, (sprite, hasError) =>
        {
            backgroundImage.sprite = sprite;
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

    // public void SetScenarioInFront()
    // {
    //     scenarioScreen.transform.SetAsLastSibling();
    // }
    //
    // public void SetUIInFront()
    // {
    //     scenarioScreen.transform.SetSiblingIndex(1);
    // }
    
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

    public void ResetScenarios()
    {
        _currentState = ApplicationState.Intro;
        _interactedActors.Clear();
        
        friction.ResetFriction();
        scenarioScreen.Close();
    }
}

public enum ApplicationState {
    Intro,
    BeforeInteractions,
    Question,
    Results,
    End
}
