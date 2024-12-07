using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameTemplate._Game.Scripts.Inventory
{
    public class ItemGrid : MonoBehaviour
    {
        public const float tileSizeWidth = 100, tileSizeHeight = 100;
        public int gridSizeWidth = 15, gridSizeHeight = 10;
        public RectTransform parentBox;
        RectTransform rectTransform;
        public InventoryItem[,] inventoryItemSlot;
        bool isInitialized;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            if (isInitialized == false)
            {
                SetSize(gridSizeWidth, gridSizeHeight);
            }
        }

        private void Init(int width, int height)
        {
            isInitialized = true;
            if (rectTransform == null)
            {
                rectTransform = GetComponent<RectTransform>();
            }

            gridSizeWidth = width;
            gridSizeHeight = height;

            inventoryItemSlot = new InventoryItem[width, height];
            Vector2 size = new Vector2(width * tileSizeWidth, height * tileSizeHeight);
            rectTransform.sizeDelta = size;

            if (parentBox != null)
            {
                parentBox.sizeDelta = size + new Vector2(50 * width, 50 * height);
                rectTransform.anchoredPosition = new Vector2((50f * width) / 2, -((50f * height) / 2));
            }
        }

        public void SetSize(int width, int height)
        {
            Init(width, height);
        }

        private Vector2 positionOnTheGrid = new Vector2();
        Vector2Int tileGridPosition = new Vector2Int();

        public Vector2Int GetTileGridPosition(Vector2 mousePosition)
        {
            positionOnTheGrid.x = mousePosition.x - rectTransform.position.x;
            positionOnTheGrid.y = rectTransform.position.y - mousePosition.y;

            tileGridPosition.x = (int)(positionOnTheGrid.x / tileSizeWidth);
            tileGridPosition.y = (int)(positionOnTheGrid.y / tileSizeHeight);

            return tileGridPosition;
        }

        public bool PlaceItem(InventoryItem inventoryItem, int posX, int posY, ref InventoryItem overlapItem)
        {
            if (BoundryCheck(posX, posY, inventoryItem.Width, inventoryItem.Height) == false)
                return false;

            if (OverlapCheck(posX, posY, inventoryItem.Width, inventoryItem.Height,
                    ref overlapItem) == false)
            {
                overlapItem = null;
                return false;
            }

            if (overlapItem != null)
            {
                CleanGridReference(overlapItem);
            }

            PlaceItem(inventoryItem, posX, posY);
            return true;
        }

        public void PlaceItem(InventoryItem inventoryItem, int posX, int posY)
        {
            RectTransform rectTransform = inventoryItem.GetComponent<RectTransform>();
            rectTransform.SetParent(this.rectTransform);

            for (int x = 0; x < inventoryItem.Width; x++)
            {
                for (int y = 0; y < inventoryItem.Height; y++)
                {
                    inventoryItemSlot[posX + x, posY + y] = inventoryItem;
                }
            }

            inventoryItem.onGridPositionX = posX;
            inventoryItem.onGridPositionY = posY;

            var position = CalculatePositionOnGrid(inventoryItem, posX, posY);

            rectTransform.localPosition = position;
        }

        public Vector2 CalculatePositionOnGrid(InventoryItem inventoryItem, int posX, int posY)
        {
            Vector2 position = new Vector2();
            position.x = posX * tileSizeWidth + tileSizeWidth * inventoryItem.Width / 2;
            position.y = -(posY * tileSizeHeight + tileSizeHeight * inventoryItem.Height / 2);
            return position;
        }

        private bool OverlapCheck(int posX, int posY, int itemDataWidth, int itemDataHeight,
            ref InventoryItem overlapItem)
        {
            for (int x = 0; x < itemDataWidth; x++)
            {
                for (int y = 0; y < itemDataHeight; y++)
                {
                    if (inventoryItemSlot[posX + x, posY + y] != null)
                    {
                        if (overlapItem == null)
                        {
                            overlapItem = inventoryItemSlot[posX + x, posY + y];
                        }
                        else
                        {
                            if (overlapItem != inventoryItemSlot[posX + x, posY + y])
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }

        public InventoryItem PickUpItem(int posX, int posY)
        {
            InventoryItem toReturn = inventoryItemSlot[posX, posY];

            if (toReturn == null) return null;

            CleanGridReference(toReturn);

            return toReturn;
        }

        private void CleanGridReference(InventoryItem item)
        {
            for (int x = 0; x < item.Width; x++)
            {
                for (int y = 0; y < item.Height; y++)
                {
                    inventoryItemSlot[item.onGridPositionX + x, item.onGridPositionY + y] = null;
                }
            }
        }

        bool PositionCheck(int posX, int posY)
        {
            if (posX < 0 || posY < 0)
            {
                return false;
            }

            if (posX >= gridSizeWidth || posY >= gridSizeHeight)
            {
                return false;
            }

            return true;
        }

        public bool BoundryCheck(int posX, int posY, int width, int height)
        {
            if (PositionCheck(posX, posY) == false) return false;

            posX += width - 1;
            posY += height - 1;

            if (PositionCheck(posX, posY) == false) return false;

            return true;
        }

        public InventoryItem GetItem(int x, int y)
        {
            return inventoryItemSlot[x, y];
        }

        public Vector2Int? FindSpaceForObject(InventoryItem itemToInsert)
        {
            int height = gridSizeHeight - itemToInsert.Height + 1;
            int width = gridSizeWidth - itemToInsert.Width + 1;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (CheckAvailableSpace(x, y, itemToInsert.Width, itemToInsert.Height))
                    {
                        return new Vector2Int(x, y);
                    }
                }
            }

            return null;
        }

        private bool CheckAvailableSpace(int posX, int posY, int itemDataWidth, int itemDataHeight)
        {
            for (int x = 0; x < itemDataWidth; x++)
            {
                for (int y = 0; y < itemDataHeight; y++)
                {
                    if (inventoryItemSlot[posX + x, posY + y] != null)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool IsEmpty()
        {
            for (int x = 0; x < gridSizeWidth; x++)
            {
                for (int y = 0; y < gridSizeHeight; y++)
                {
                    if (inventoryItemSlot[x, y] != null)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public void CheckBasket()
        {
            if (GetComponentInParent<Basket>())
            {
                if (IsEmpty())
                {
                    GetComponentInParent<Basket>().Empty();
                }
            }
        }

        public int GetEmptyCount()
        {
            int count = 0;
            for (int x = 0; x < gridSizeWidth; x++)
            {
                for (int y = 0; y < gridSizeHeight; y++)
                {
                    if (inventoryItemSlot[x, y] == null)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        public List<ItemData> GetItemsInside()
        {
            List<ItemData> items = new List<ItemData>();
            
            for (int x = 0; x < gridSizeWidth; x++)
            {
                for (int y = 0; y < gridSizeHeight; y++)
                {
                    if (inventoryItemSlot[x, y] != null)
                    {
                        if (!items.Contains(inventoryItemSlot[x, y].itemData))
                        {
                            items.Add(inventoryItemSlot[x, y].itemData);
                        }
                    }
                }
            }
            return items;
        }
    }
}