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

        public int SmallestSide
        {
            get { return Height >= Width ? Height : Width; }
        }

        public int onGridPositionX, onGridPositionY;

        public bool rotated;
        private Image _image;

        private void Start()
        {
            _image = GetComponent<Image>();
        }

        public void Set(ItemData item)
        {
            itemData = item;

            if (_image == null)
            {
                _image = GetComponent<Image>();
            }

            _image.sprite = itemData.itemIcon;

            Vector2 size = new Vector2();
            size.x = itemData.width * ItemGrid.tileSizeWidth;
            size.y = itemData.height * ItemGrid.tileSizeHeight;
            GetComponent<RectTransform>().sizeDelta = size;
        }

        public void Rotate()
        {
            rotated = !rotated;

            RectTransform rect = GetComponent<RectTransform>();
            rect.rotation = Quaternion.Euler(0, 0, rotated ? 90 : 0);
        }

        public async UniTask FlashRed()
        {
            _image.color = Color.red;
            await UniTask.Delay(200);
            _image.color = Color.white;
        }
    }
}