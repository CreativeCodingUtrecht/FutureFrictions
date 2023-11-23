using UnityEngine;

namespace UI
{
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
            
                downloadHandler.GetImage(actorsData[i].sprite, (actorSprite, hasError) =>
                {
                    actors[currentIndex].Initialize(actorSprite, actorsData[currentIndex]);
                });
            }
        
            Open();
        }

        public void SetActorsToScenarioAnswer(Options option)
        {
            foreach (var actor in actors)
            {
                actor.SetActorToOption(option);
            }
        }
    }
}
