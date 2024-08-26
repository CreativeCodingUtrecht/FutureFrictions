using UI;
using UnityEngine;
using UnityEngine.UI;

public class Actor : MonoBehaviour
{
    [SerializeField] 
    private Image image;
    
    [SerializeField] 
    private GameObject marker;
    
    private DialogScreen _dialogScreen;
    private DownloadHandler _downloadHandler;
    private ScenarioScreen _scenarioScreen;

    private int _actorId;
    private string _currentStateText;
    private Sprite _avatar;
    private Element _elementData;
    
    public void Initialize(Element elementData, ScenarioScreen scenarioScreen, DialogScreen dialogScreen, DownloadHandler downloadHandler)
    {
        _scenarioScreen = scenarioScreen;
        _dialogScreen = dialogScreen;
        _elementData = elementData;

        _actorId = elementData.id;
        _currentStateText = _elementData.interaction.statement;

        if (_avatar == null)
        {
            downloadHandler.GetImage(elementData.url, (sprite, error) =>
            {
                if (error || !image) return;
                
                _avatar = sprite;
                image.sprite = _avatar;
                gameObject.SetActive(true);
            });
        }
        else
        {
            image.sprite = _avatar;
            gameObject.SetActive(true);
        }
        
        _currentStateText = elementData.interaction.statement;
        marker.SetActive(true);
    }

    public void Interact()
    {
        marker.SetActive(false);

        _scenarioScreen.InteractedWithCharacter(_actorId);
        
        if (_avatar == null)
        {
            _downloadHandler.GetImage(_elementData.url, (avatarSprite, hasError) =>
            {
                _avatar = avatarSprite;
                _dialogScreen.InitializeScreen(avatarSprite, _currentStateText, _elementData.interaction.name);
            });
        }
        else
        {
            _dialogScreen.InitializeScreen(_avatar, _currentStateText, _elementData.interaction.name);
        }
    }
}
