using System;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;

public class DataManager : MonoBehaviour
{
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
    public string name;
    public FrictionData friction;
    public Collage collage;
    public WhatIf whatIf;
    public string provocativestatement;
}

[Serializable]
public class FrictionData
{
    public string frictionalstatement;
    public string avatar;
    public string emergingfrictions;
}

[Serializable]
public class Collage
{
    public Scenario present;
    public Scenario future;
}

[Serializable]
public class Scenario
{
    public Definition definition;
    public string url;
}

[Serializable]
public class Definition
{
    public string backgroundColor;
    public string background;
    public Element[] elements;
}

[Serializable]
public class Element
{
    public int id;
    public Placement placement;
    public string url;
    public bool friction;
    public bool interactable;
    public Interaction interaction;
}

[Serializable]
public class Interaction
{
    public string name;
    public string statement;
}

[Serializable]
public class Placement
{
    public int index;
    public float left;
    public float top;
    public int width;
    public int height;
    public float scaleX;
    public float scaleY;
    public float angle;
}

[Serializable]
public class WhatIf
{
    public string question;
    public string avatar;
}