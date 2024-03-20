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
    
    private string _actorId;
    private ActorData _actorData;
    private ActorState currentState = ActorState.None;

    private string _currentStateText;
    
    private Sprite avatar;
    
    public void Initialize(Sprite actorSprite, ActorData actorData)
    {
        _actorId = actorData.description;
        _actorData = actorData;

        if (actorSprite == null)
        {
            image.enabled = false;
        }
        else
        {
            image.sprite = actorSprite;
        }

        _currentStateText = actorData.content.before;
        
        currentState = ActorState.Clickable;
        marker.SetActive(true); // TODO: Animate the marker
        gameObject.SetActive(true);
    }

    public void SetActorToOption(Options option)
    {
        switch (option)
        {
            case Options.A:
                _currentStateText = _actorData.content.after.a;
                break;
            case Options.B:
                _currentStateText = _actorData.content.after.b;
                break;
            case Options.C:
                _currentStateText = _actorData.content.after.c;
                break;
            case Options.None:
            default:
                throw new ArgumentOutOfRangeException(nameof(option), option, null);
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
            downloadHandler.GetImage(_actorData.avatar, (avatarSprite, hasError) =>
            {
                avatar = avatarSprite;
                dialogScreen.InitializeScreen(avatarSprite, _currentStateText, _actorData.description, () =>
                {
                    scenarioManager.CheckInteractionsDone();
                }); 
            });
        }
        else
        {
            dialogScreen.InitializeScreen(avatar, _currentStateText, _actorData.description, () =>
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
