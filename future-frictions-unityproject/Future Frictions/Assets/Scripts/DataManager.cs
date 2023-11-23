using System;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;

public class DataManager : MonoBehaviour
{
    public static ScenarioData ScenarioData => _scenarioDataDataObject;

    public UnityAction<ScenarioData> OnLoadComplete;

    [SerializeField]
    private string defaultScenarioName = "drone";
    
    [SerializeField]
    private DownloadHandler downloadHandler;
    
    private static ScenarioData _scenarioDataDataObject;
    
    public void DownloadScenario()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        defaultScenarioName = GetScenarioNameParameter();

        if (defaultScenarioName == "none")
        {
            Debug.LogError("Invalid scenario, handle error visually");
            return;
        }
#endif
        downloadHandler.GetJsonData(defaultScenarioName, DownloadComplete);
    }

    private void DownloadComplete(string jsonString, bool hasError)
    {
        _scenarioDataDataObject = JsonConvert.DeserializeObject<ScenarioData>(jsonString);
        OnLoadComplete?.Invoke(_scenarioDataDataObject);
    }

    private string GetScenarioNameParameter()
    {
        var parameters = GetParameters();

        foreach (var parameter in parameters)
        {
            var keyValue = parameter.Split("=");

            if (keyValue[0] == "scenario")
            {
                return keyValue[1];
            }
        }

        return "none";
    }
    
    private string[] GetParameters()
    {
        var parameters = Application.absoluteURL.Substring(Application.absoluteURL.IndexOf("?", StringComparison.Ordinal)+1);
        return parameters.Split('&');
    }
}

[Serializable]
public class ScenarioData
{
    public ScenarioSceneData scene;
    public FrictionData friction;
    public ActorData[] actors;
}

[Serializable]
public class ScenarioSceneData
{
    public ScenarioContentData content;
    public string background;
    public string avatar;
}

[Serializable]
public class ScenarioContentData
{
    public string welcome;
}

[Serializable]
public class ActorData
{
    public string description;
    public string sprite;
    public string avatar;
    public ActorContentData content;
}

[Serializable]
public class ActorContentData
{
    public string before;
    public ActorContentOptionsData after;
}

[Serializable]
public class ActorContentOptionsData
{
    public string a;
    public string b;
    public string c;
}

[Serializable]
public class FrictionData
{
    public string description;
    public string avatar;
    public FrictionContentData content;
    public FrictionOptionsData options;
}

[Serializable]
public class FrictionContentData
{
    public string before;
}

[Serializable]
public class FrictionOptionsData
{
    public FrictionOptionData a;
    public FrictionOptionData b;
    public FrictionOptionData c;
}

[Serializable]
public class FrictionOptionData
{
    public string avatar;
    public string content;
    public FrictionOptionSpriteData sprites;
}

[Serializable]
public class FrictionOptionSpriteData
{
    public string[] foreground;
    public string[] background;
    public string[] floating;
}