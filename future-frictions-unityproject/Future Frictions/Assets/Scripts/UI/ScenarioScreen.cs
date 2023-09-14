using UnityEngine;

public class ScenarioScreen : BaseScreen
{
    [SerializeField] 
    private DownloadHandler downloadHandler;
    
    [SerializeField]
    private Actor[] actors;

    public void InitializeActors(ActorData[] actorsData)
    {
        for (var i = 0; i < actorsData.Length; i++)
        {
            var currentIndex = i;
            
            downloadHandler.GetImage(actorsData[i].sprite, actorSprite =>
            {
                actors[currentIndex].Initialize(actorSprite, actorsData[currentIndex]);
            });
        }
        
        Open();
    }
}
