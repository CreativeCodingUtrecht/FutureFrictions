using UnityEngine;

namespace UI
{
    public class Friction : MonoBehaviour
    {
        [SerializeField] private QuestionScreen questionScreen;
        [SerializeField] private ScenarioManager scenarioManager;
        
        public void Initialize(ScenarioData scenarioData)
        {
            questionScreen.Initialize(scenarioData);
            scenarioManager.PopulateTheFuture();
        }

        public void ResetFriction()
        {
            gameObject.SetActive(false);
        }
    }
}
