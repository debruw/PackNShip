using UnityEngine;
using UnityEngine.UI;

namespace GameTemplate._Game.Scripts
{
    public class Label : MonoBehaviour
    {
        public Image IconImage;

        public void SetSprite(Sprite icon)
        {
            IconImage.sprite = icon;
        }
    }
}