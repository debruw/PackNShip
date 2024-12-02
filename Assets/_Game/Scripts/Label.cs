using GameTemplate._Game.Scripts.Inventory;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace GameTemplate._Game.Scripts
{
    public class Label : MonoBehaviour
    {
        [HideInInspector]
        public ItemData _itemData;
        public Image IconImage;

        public void SetSprite(ItemData itemData)
        {
            _itemData = itemData;
            IconImage.sprite = itemData.itemIcon;
            Vector2 size = new Vector2(itemData.width * ItemGrid.tileSizeWidth,
                itemData.height * ItemGrid.tileSizeHeight);
            IconImage.GetComponent<RectTransform>().sizeDelta = size;
            IconImage.transform.localScale = Vector3.one * (.9f - (itemData.GetBigSide() * .2f));
        }
    }
}