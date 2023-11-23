using UnityEngine;

namespace UI
{
    public class Friction : MonoBehaviour
    {
        [SerializeField] 
        private QuestionScreen questionScreen;
    
        private FrictionData _frictionData;
    
        public void Initialize(FrictionData frictionData)
        {
            _frictionData = frictionData;
        
            questionScreen.Initialize(frictionData);
        
            gameObject.SetActive(true);
        }

        public void ResetFriction()
        {
            gameObject.SetActive(false);
        }
    }
}
