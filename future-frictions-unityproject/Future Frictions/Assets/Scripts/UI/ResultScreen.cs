using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ResultScreen : BaseScreen
    {
        [SerializeField] private ScenarioManager scenarioManager;
        
        [SerializeField] private ScenarioScreen scenarioScreen;
        [SerializeField] private ScenarioResult scenarioResult;
        [SerializeField] private Button nextButton;

        [SerializeField] private GameObject floatingElements;
        [SerializeField] private GameObject backgroundElements;
        [SerializeField] private GameObject foregroundElements;
        
        public void InitializeResults(Options option)
        {
            nextButton.onClick.AddListener(Close);
            
            scenarioScreen.SetActorsToScenarioAnswer(option);
            SetElementsState(true);

            switch (option)
            {
                case Options.A:
                    scenarioResult.ShowResults(scenarioManager.ScenarioData.friction.options.a);
                    break;
                case Options.B:
                    scenarioResult.ShowResults(scenarioManager.ScenarioData.friction.options.b);
                    break;
                case Options.C:
                    scenarioResult.ShowResults(scenarioManager.ScenarioData.friction.options.c);
                    break;
                case Options.None:
                default:
                    throw new ArgumentOutOfRangeException(nameof(option), option, null);
            }
            
            Open();
        }
        
        private void SetElementsState(bool state)
        {
            floatingElements.gameObject.SetActive(state);
            backgroundElements.gameObject.SetActive(state);
            foregroundElements.gameObject.SetActive(state);
        }

        public override void ResetScreen()
        {
            SetElementsState(false);
            scenarioResult.ResetResults();
        }

        public override void Close()
        {
            base.Close();
            SetElementsState(false);
        }
    }
}
