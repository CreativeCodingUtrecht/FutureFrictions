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

    private Sprite avatar;
    
    public void Initialize(Sprite actorSprite, ActorData actorData)
    {
        _actorId = actorData.description;
        _actorData = actorData;
        
        image.sprite = actorSprite;

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
            downloadHandler.GetImage(_actorData.avatar, avatarSprite =>
            {
                avatar = avatarSprite;
                dialogScreen.InitializeScreen(avatarSprite, _actorData.content.before, () =>
                {
                    scenarioManager.CheckInteractionsDone();
                }); 
            });
        }
        else
        {
            dialogScreen.InitializeScreen(avatar, _actorData.content.before, () =>
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
