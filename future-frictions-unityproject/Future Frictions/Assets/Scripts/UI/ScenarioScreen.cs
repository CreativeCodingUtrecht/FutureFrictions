using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ScenarioScreen : BaseScreen
    {
        [SerializeField] 
        private DownloadHandler downloadHandler;
        
        [SerializeField] 
        private DialogScreen dialogScreen;

        [SerializeField]
        private Friction friction;
        
        [SerializeField] 
        private Actor characterUIPrefab;
        
        [SerializeField] 
        private Image elementPrefab;

        private ScenarioData _scenarioData;
        private TimeFrame _currentTimeFrame = TimeFrame.Present;
        
        private List<int> _interactedCharacters = new();
        private List<GameObject> _characters = new();
        private List<GameObject> _elements = new();
        
        private const float XScale = 960f / 1920f;
        private const float YScale = 540f / 1080f;

        public void InitializeFrictionalObjects(ScenarioData scenarioData)
        {
            Clean();
            
            _scenarioData = scenarioData;

            SpawnFrictionalObjects(_scenarioData.collage.future.definition.elements);
        }
        
        public void InitializeScenario(ScenarioData scenarioData, TimeFrame timeFrame)
        {
            Clean();

            _scenarioData = scenarioData;
            _currentTimeFrame = timeFrame;

            switch (timeFrame)
            {
                case TimeFrame.Present:
                    // SpawnCharacters(scenarioData.collage.present.definition.characters);
                    SpawnElements(scenarioData.collage.present.definition.elements);
                    break;
                case TimeFrame.Future:
                    // SpawnCharacters(scenarioData.collage.future.definition.characters);
                    SpawnElements(scenarioData.collage.future.definition.elements);
                    break;
            }
            
            Open();
        }

        public void InteractedWithCharacter(int characterId)
        {
            if (_interactedCharacters.Contains(characterId)) return;
            
            _interactedCharacters.Add(characterId);

            if (_interactedCharacters.Count == _characters.Count)
            {
                NextButton.Instance.SetInteraction(() =>
                {
                    dialogScreen.Close();
                    CheckInteractionsDone();
                });
                NextButton.Instance.Show();
            }
        }

        public void CheckInteractionsDone()
        {
            if (_interactedCharacters.Count == _characters.Count)
            {
                if (_currentTimeFrame == TimeFrame.Present)
                {
                    IntroduceFriction();
                }
                else
                {
                    // END   
                    NextButton.Instance.Hide();
                    Debug.Log("The game is done!");
                }
            }
        }

        private void IntroduceFriction()
        {
            friction.Initialize(_scenarioData);
        }

        private void SpawnElements(Element[] elements)
        {
            foreach (var element in elements)
            {
                if (element.interactable)
                {
                    SpawnActor(element);
                }
                else
                {
                    SpawnElement(element);
                }
            }
        }

        private void SpawnElement(Element element)
        {
            var newElement = Instantiate(elementPrefab, transform);
            
            downloadHandler.GetImage(element.url, (sprite, error) =>
            {
                if (error)
                {
                    newElement.gameObject.SetActive(false);
                }
                else
                {
                    newElement.sprite = sprite;
                    newElement.gameObject.SetActive(true);
                }
            });

            SetPlacement(newElement.transform, element);

            _elements.Add(newElement.gameObject);
        }

        private void SpawnActor(Element actor)
        {
            var newActor = Instantiate(characterUIPrefab, transform);

            SetPlacement(newActor.transform, actor);
            
            newActor.Initialize(actor, this, dialogScreen, downloadHandler);

            _characters.Add(newActor.gameObject);
        }

        private void SpawnFrictionalObjects(Element[] elements)
        {
            foreach (var element in elements)
            {
                if (!element.friction) continue;
                
                var newElement = Instantiate(elementPrefab, transform);

                downloadHandler.GetImage(element.url, (sprite, error) =>
                {
                    var el = newElement;

                    if (error)
                    {
                        el.gameObject.SetActive(false);
                    }
                    else
                    {
                        el.sprite = sprite;
                        el.gameObject.SetActive(true);
                    }
                });
                
                SetPlacement(newElement.transform, element);

                _elements.Add(newElement.gameObject);
            }
        }

        private void SetPlacement(Transform elementTransform, Element element)
        {
            ((RectTransform) elementTransform).pivot = new Vector2(0.5f, 0.5f);

            ((RectTransform) elementTransform).rotation = Quaternion.Euler(0, 0, -element.placement.angle);

            ((RectTransform) elementTransform).pivot = new Vector2(0, 1);

            ((RectTransform) elementTransform).anchoredPosition =
                new Vector2(element.placement.left * XScale, -element.placement.top * YScale);

            ((RectTransform) elementTransform).sizeDelta =
                new Vector2((element.placement.width * XScale) * element.placement.scaleX,
                    (element.placement.height * YScale) * element.placement.scaleY);
        }
        
        private void Clean()
        {
            foreach (var character in _characters)
            {
                Destroy(character);
            }
            
            foreach (var element in _elements)
            {
                Destroy(element);
            }
            
            _interactedCharacters.Clear();
            _characters.Clear();
            _elements.Clear();
        }
    }

    public enum TimeFrame
    {
        Present,
        Future
    }
}
