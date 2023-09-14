using UnityEngine;
using UnityEngine.UI;

public class Actor : MonoBehaviour
{
    [SerializeField] 
    private Image image;

    public void Initialize(Sprite actorSprite)
    {
        image.sprite = actorSprite;
        
        gameObject.SetActive(true);
    }
}
