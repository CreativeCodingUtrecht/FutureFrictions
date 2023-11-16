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

        public void ShowResults(FrictionOptionData optionData)
        {
            for (var i = 0; i < optionData.sprites.background.Length; i++)
            {
                var i1 = i;
                if (optionData.sprites.background[i] == null)
                {
                    backgroundElements[i1].sprite = null;
                    backgroundElements[i1].color = new Color(0, 0, 0, 0);
                    continue;
                }

                downloadHandler.GetImage(optionData.sprites.background[i], sprite =>
                {
                    backgroundElements[i1].sprite = sprite == null ? defaultSprite : sprite;
                });
            }
            
            for (var i = 0; i < optionData.sprites.floating.Length; i++)
            {
                var i1 = i;
                if (optionData.sprites.floating[i] == null)
                {
                    floatingElements[i1].sprite = null;
                    floatingElements[i1].color = new Color(0, 0, 0, 0);
                    continue;
                }
                
                downloadHandler.GetImage(optionData.sprites.floating[i], sprite =>
                {
                    floatingElements[i1].sprite = sprite == null ? defaultSprite : sprite;
                });
            }
            
            for (var i = 0; i < optionData.sprites.foreground.Length; i++)
            {
                var i1 = i;
                if (optionData.sprites.foreground[i] == null)
                {
                    foregroundElements[i1].sprite = null;
                    foregroundElements[i1].color = new Color(0, 0, 0, 0);
                    continue;
                }
                
                downloadHandler.GetImage(optionData.sprites.foreground[i], sprite =>
                {
                    foregroundElements[i1].sprite = sprite == null ? defaultSprite : sprite;
                });
            }
            
            gameObject.SetActive(true);
        }
    }
}
