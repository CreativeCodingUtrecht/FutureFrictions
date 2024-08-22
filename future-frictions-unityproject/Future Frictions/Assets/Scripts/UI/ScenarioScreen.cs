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
        
        public void InitializeScenario(ScenarioData scenarioData, TimeFrame timeFrame)
        {
            Clean();

            _scenarioData = scenarioData;
            _currentTimeFrame = timeFrame;
            
            if (timeFrame == TimeFrame.Present)
            {
                SpawnCharacters(scenarioData.collage.present.definition.characters);
                SpawnElements(scenarioData.collage.present.definition.elements);
            } 
            else if (timeFrame == TimeFrame.Future)
            {
                SpawnCharacters(scenarioData.collage.future.definition.characters);
                SpawnElements(scenarioData.collage.future.definition.elements);
            }
            
            Open();
        }

        public void InteractedWithCharacter(int characterId)
        {
            if (_interactedCharacters.Contains(characterId)) return;
            
            _interactedCharacters.Add(characterId);
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
                }
            }
        }

        private void IntroduceFriction()
        {
            friction.Initialize(_scenarioData);
        }

        private void SpawnCharacters(Character[] characters)
        {
            foreach (var character in characters)
            {
                // Canvas is 960 x 600 -> 1920 1080
                const float xScale = 960f / 1920f;
                const float yScale = 540f / 1080f;
                
                var newCharacter = Instantiate(characterUIPrefab, transform);
                
                var characterTransform = newCharacter.transform;
                
                ((RectTransform) characterTransform).pivot = new Vector2(0.5f, 0.5f);
                
                ((RectTransform) characterTransform).rotation = Quaternion.Euler(0, 0, -character.placement.angle);

                ((RectTransform) characterTransform).pivot = new Vector2(0, 1);
                
                ((RectTransform) characterTransform).anchoredPosition =
                    new Vector2(character.placement.left * xScale, -character.placement.top * yScale);
                
                ((RectTransform) characterTransform).sizeDelta =
                    new Vector2((character.placement.width * xScale) * character.placement.scaleX, (character.placement.height * yScale) * character.placement.scaleY);

                newCharacter.Initialize(character, this, dialogScreen, downloadHandler);
                
                _characters.Add(newCharacter.gameObject);
            }
        }

        private void SpawnElements(Element[] elements)
        {
            foreach (var element in elements)
            {
                // Canvas is 960 x 600 -> 1920 1080
                const float xScale = 960f / 1920f;
                const float yScale = 600f / 1080f;
                
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
                
                var elementTransform = newElement.transform;
                ((RectTransform) elementTransform).anchoredPosition =
                    new Vector2(element.placement.left * xScale, -element.placement.top * yScale);
                
                ((RectTransform) elementTransform).sizeDelta =
                    new Vector2((element.placement.width * xScale) * element.placement.scaleX, (element.placement.height * yScale) * element.placement.scaleY);
                
                _elements.Add(newElement.gameObject);
            }
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
