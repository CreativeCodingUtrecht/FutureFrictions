using System;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class Actor : MonoBehaviour
{
    [Header("External References")]
    [SerializeField] 
    private DialogScreen dialogScreen;

    [SerializeField]
    private DownloadHandler downloadHandler;

    [SerializeField] 
    private ScenarioManager scenarioManager;
    
    [Header("Local References")]
    [SerializeField] 
    private Image image;
    
    [SerializeField] 
    private GameObject marker;
    
    private int _actorId;
    private Character _characterData;
    private ActorState currentState = ActorState.None;

    private string _currentStateText;
    
    private Sprite avatar;
    
    public void Initialize(Sprite actorSprite, Character characterData)
    {
        _actorId = characterData.id;
        this._characterData = characterData;

        if (actorSprite == null)
        {
            image.enabled = false;
        }
        else
        {
            image.sprite = actorSprite;
        }

        _currentStateText = characterData.statement;
        
        currentState = ActorState.Clickable;
        marker.SetActive(true);
        gameObject.SetActive(true);
    }

    public void SetActorToOption(Options option)
    {
        switch (option)
        {
            case Options.A:
            case Options.B:
            case Options.C:
            case Options.None:
            default:
                _currentStateText = _characterData.statement;
                break;
        }

        currentState = ActorState.Clickable;
        marker.SetActive(true); // TODO: Animate the marker
        gameObject.SetActive(true);
    }
    
    public void Interact()
    {
        if (currentState != ActorState.Clickable) return;
        
        SetState(ActorState.Interacted);
        // Update manager that we interacted with this actor
        marker.SetActive(false);

        scenarioManager.StoreActorInteracted(_actorId);
        
        // Show dialog
        if (avatar == null)
        {
            downloadHandler.GetImage(_characterData.url, (avatarSprite, hasError) =>
            {
                avatar = avatarSprite;
                dialogScreen.InitializeScreen(avatarSprite, _currentStateText, _characterData.statement, () =>
                {
                    scenarioManager.CheckInteractionsDone();
                }); 
            });
        }
        else
        {
            dialogScreen.InitializeScreen(avatar, _currentStateText, _characterData.statement, () =>
            {
                scenarioManager.CheckInteractionsDone();
            });
        }
    }

    private void SetState(ActorState state)
    {
        currentState = state;
    }
}

public enum ActorState
{
    None,
    Clickable,
    Interacted
}
