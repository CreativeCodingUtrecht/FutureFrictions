using UnityEngine;

namespace UI
{
    public class ScenarioScreen : BaseScreen
    {
        [SerializeField] 
        private DownloadHandler downloadHandler;
    
        [SerializeField]
        private Actor[] actors;

        public void InitializeActors(Character[] characters)
        {
            for (var i = 0; i < characters.Length; i++)
            {
                var currentIndex = i;
            
                downloadHandler.GetImage(characters[i].url, (actorSprite, hasError) =>
                {
                    actors[currentIndex].Initialize(actorSprite, characters[currentIndex]);
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
