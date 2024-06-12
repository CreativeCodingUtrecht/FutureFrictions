using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class DownloadHandler : MonoBehaviour
{
    [SerializeField]
    private string endpoint = "http://localhost:3000";

    private string _scenarioName;

    public void GetJsonData(string scenarioName, UnityAction<string, bool> downloadComplete)
    {
        var uri = Path.Combine(endpoint, "api/scenarios", $"{scenarioName}");
        _scenarioName = scenarioName;
        StartCoroutine(GetTextRequest(uri, downloadComplete));
    }

    public void GetImage(string url, UnityAction<Sprite, bool> onComplete)
    {
        if (string.IsNullOrEmpty(url))
        {
            onComplete?.Invoke(null, true);
        }
        else
        {
            if (!url.StartsWith("/api/scenarios"))
            {
                url = Path.Join("api/scenarios", $"{_scenarioName}", url);
            }
            
            var uri = Path.Join(endpoint, url);
            StartCoroutine(GetImageRequest(uri, onComplete));
        }
    }
    
    private IEnumerator GetTextRequest(string uri, UnityAction<string, bool> downloadComplete)
    {
        using var webRequest = UnityWebRequest.Get(uri);

        // Request and wait for the desired page.
        yield return webRequest.SendWebRequest();

        switch (webRequest.result)
        {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
                Debug.LogError("Error: " + webRequest.error);
                downloadComplete?.Invoke(webRequest.error, true);
                break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.LogError("HTTP Error: " + webRequest.error);
                downloadComplete?.Invoke(webRequest.error, true);
                break;
            case UnityWebRequest.Result.Success:
                downloadComplete?.Invoke(webRequest.downloadHandler.text, false);
                break;
        }
    }
    
    private IEnumerator GetImageRequest(string uri, UnityAction<Sprite, bool> downloadComplete)
    {
        using var webRequest = UnityWebRequestTexture.GetTexture(uri);
        
        yield return webRequest.SendWebRequest();

        switch (webRequest.result)
        {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
                Debug.LogError("Error: " + webRequest.error);
                downloadComplete?.Invoke(null, true);
                break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.LogError("HTTP Error: " + webRequest.error);
                downloadComplete?.Invoke(null, true);
                break;
            case UnityWebRequest.Result.Success:
                var texture = DownloadHandlerTexture.GetContent(webRequest);
                var sprite = Sprite.Create(texture, new Rect(Vector2.zero, new Vector2(texture.width, texture.height)),
                    Vector2.one * 0.5f);
                downloadComplete?.Invoke(sprite, false);
                break;
        }
    }
}
