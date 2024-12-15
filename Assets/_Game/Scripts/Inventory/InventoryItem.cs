using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace GameTemplate._Game.Scripts.Inventory
{
    public class InventoryItem : MonoBehaviour
    {
        public ItemData itemData;

        public int Height
        {
            get { return rotated ? itemData.width : itemData.height; }
        }

        public int Width
        {
            get { return rotated ? itemData.height : itemData.width; }
        }

        public int onGridPositionX, onGridPositionY;

        public bool rotated;
        private Image _image;
        RectTransform _rectTransform;
        Shadow _shadow;

        public void Set(ItemData item)
        {
            _image = GetComponent<Image>();
            _rectTransform = GetComponent<RectTransform>();
            _shadow = GetComponent<Shadow>();
            
            itemData = item;

            if (_image == null)
            {
                _image = GetComponent<Image>();
            }

            _image.sprite = itemData.itemIcon;

            Vector2 size = new Vector2();
            size.x = itemData.width * GlobalVariables.tileSizeWidth;
            size.y = itemData.height * GlobalVariables.tileSizeHeight;
            _rectTransform.sizeDelta = size;
        }

        public void Rotate()
        {
            rotated = !rotated;

            _rectTransform.rotation = Quaternion.Euler(0, 0, rotated ? 90 : 0);
            _shadow.effectDistance = rotated ? new Vector2(-10, -5) : new Vector2(5, -10);
        }
    }
}