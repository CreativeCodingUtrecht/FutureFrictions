using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ScenarioResult : MonoBehaviour
    {
        [SerializeField] private ScenarioManager scenarioManager;
        [SerializeField] private DownloadHandler downloadHandler;
        
        [SerializeField] private Image[] backgroundElements;
        [SerializeField] private Image[] floatingElements;
        [SerializeField] private Image[] foregroundElements;

        [SerializeField] private Sprite defaultSprite;

        private void Awake()
        {
            ResetResults();
        }

        public void ShowResults(FrictionOptionData optionData)
        {
            for (var i = 0; i < optionData.sprites.background.Length; i++)
            {
                var i1 = i;
                if (optionData.sprites.background[i] == null)
                {
                    backgroundElements[i1].gameObject.SetActive(false);
                    continue;
                }

                downloadHandler.GetImage(optionData.sprites.background[i], (sprite, hasError) =>
                {
                    backgroundElements[i1].sprite = sprite == null ? defaultSprite : sprite;
                    backgroundElements[i1].gameObject.SetActive(true);
                });
            }
            
            for (var i = 0; i < optionData.sprites.floating.Length; i++)
            {
                var i1 = i;
                if (optionData.sprites.floating[i] == null)
                {
                    floatingElements[i1].gameObject.SetActive(false);
                    continue;
                }
                
                downloadHandler.GetImage(optionData.sprites.floating[i], (sprite, hasError) =>
                {
                    floatingElements[i1].sprite = sprite == null ? defaultSprite : sprite;
                    floatingElements[i1].gameObject.SetActive(true);
                });
            }
            
            for (var i = 0; i < optionData.sprites.foreground.Length; i++)
            {
                var i1 = i;
                if (optionData.sprites.foreground[i] == null)
                {
                    foregroundElements[i1].gameObject.SetActive(false);
                    continue;
                }
                
                downloadHandler.GetImage(optionData.sprites.foreground[i], (sprite, hasError) =>
                {
                    foregroundElements[i1].sprite = sprite == null ? defaultSprite : sprite;
                    foregroundElements[i1].gameObject.SetActive(true);
                });
            }
            
            gameObject.SetActive(true);
        }

        public void ResetResults()
        {
            foreach (var backgroundElement in backgroundElements)
            {
                backgroundElement.gameObject.SetActive(false);
            }

            foreach (var floatingElement in floatingElements)
            {
                floatingElement.gameObject.SetActive(false);
            }

            foreach (var foregroundElement in foregroundElements)
            {
                foregroundElement.gameObject.SetActive(false);
            }
        }
    }
}
