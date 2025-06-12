using UnityEngine;
using UnityEngine.UI;

namespace GameTemplate._Game.Scripts.Inventory
{
    public class InventoryItem : MonoBehaviour
    {
        public ItemData itemData;

        public int Height => (rotated ? itemData.size.x : itemData.size.y);

        public int Width => (rotated ? itemData.size.y : itemData.size.x);

        public int onGridPositionX, onGridPositionY;

        public bool rotated;
        private Image _image;
        RectTransform _rectTransform;

        public void Set(ItemData item)
        {
            _image = GetComponent<Image>();
            _rectTransform = GetComponent<RectTransform>();
            
            itemData = item;

            _image.sprite = itemData.itemIcon;

            Vector2 size = new Vector2();
            size.x = itemData.size.x * GlobalVariables.tileSizeWidth;
            size.y = itemData.size.y * GlobalVariables.tileSizeHeight;
            _rectTransform.sizeDelta = size;
        }

        public void Rotate()
        {
            rotated = !rotated;

            _rectTransform.rotation = Quaternion.Euler(0, 0, rotated ? 90 : 0);
        }
    }
}