using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Friction : MonoBehaviour
    {
        [SerializeField] private Image frictionImage;
        [SerializeField] private QuestionScreen questionScreen;
        [SerializeField] private DownloadHandler downloadHandler;
        
        private FrictionData _frictionData;
    
        public void Initialize(ScenarioSceneData sceneData, FrictionData frictionData)
        {
            _frictionData = frictionData;
            
            downloadHandler.GetImage(frictionData.avatar, (frictionSprite, hasError) =>
            {
                if (frictionSprite == null || hasError)
                {
                    frictionImage.enabled = false;
                }
                else
                {
                    frictionImage.sprite = frictionSprite;
                }

                questionScreen.Initialize(sceneData, frictionData);
                gameObject.SetActive(true);
            });
        }

        public void ResetFriction()
        {
            gameObject.SetActive(false);
        }
    }
}
