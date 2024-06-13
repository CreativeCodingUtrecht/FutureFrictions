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
    private Character _characterData;
    
    public void Initialize(Character characterData, ScenarioScreen scenarioScreen, DialogScreen dialogScreen, DownloadHandler downloadHandler)
    {
        _scenarioScreen = scenarioScreen;
        _dialogScreen = dialogScreen;
        _characterData = characterData;

        _actorId = characterData.id;
        _currentStateText = _characterData.statement;

        if (_avatar == null)
        {
            downloadHandler.GetImage(characterData.url, (sprite, error) =>
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
        
        _currentStateText = characterData.statement;
        marker.SetActive(true);
    }

    public void Interact()
    {
        marker.SetActive(false);

        _scenarioScreen.InteractedWithCharacter(_actorId);
        
        if (_avatar == null)
        {
            _downloadHandler.GetImage(_characterData.url, (avatarSprite, hasError) =>
            {
                _avatar = avatarSprite;
                _dialogScreen.InitializeScreen(avatarSprite, _currentStateText, _characterData.name, () =>
                {
                    _scenarioScreen.CheckInteractionsDone();
                });
            });
        }
        else
        {
            _dialogScreen.InitializeScreen(_avatar, _currentStateText, _characterData.name, () =>
            {
                _scenarioScreen.CheckInteractionsDone();
            });
        }
    }
}
