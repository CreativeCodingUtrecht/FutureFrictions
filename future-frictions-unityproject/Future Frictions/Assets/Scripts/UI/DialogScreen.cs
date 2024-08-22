using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DialogScreen : BaseScreen
    {
        [SerializeField]
        private Image avatarImage;

        [SerializeField]
        private TMP_Text characterName;

        [SerializeField]
        private TMP_Text mainText;
        
        public void InitializeScreen(Sprite avatar, string text, string description)
        {
            if (avatar == null)
            {
                avatarImage.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                avatarImage.transform.parent.gameObject.SetActive(true);
                avatarImage.sprite = avatar;
            }

            characterName.text = description;
            mainText.text = text;
        
            Open();
        }
    }
}
