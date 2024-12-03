using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using Random = UnityEngine.Random;

namespace GameTemplate._Game.Scripts.Inventory
{
    public class InventoryController : MonoBehaviour
    {
        /*private static InventoryController _instance;

        public static InventoryController Instance
        {
            get { return _instance; }
        }*/
        public ItemsDataList _itemsDataList;

        private ItemGrid selectedItemGrid;

        public ItemGrid SelectedItemGrid
        {
            get => selectedItemGrid;
            set
            {
                selectedItemGrid = value;
                inventoryHighlight.SetParent(value);
            }
        }

        InventoryItem selectedItem;
        InventoryItem overlapItem;
        RectTransform rectTransform;
        
        public GameObject itemPrefab;
        public RectTransform canvasTransform;
        private InventoryHighlight inventoryHighlight;

        [Inject]
        public void Construct(ItemsDataList ItemsDataList)
        {
            _itemsDataList = ItemsDataList;
        }

        private void Awake()
        {
            /*if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }*/

            inventoryHighlight = GetComponent<InventoryHighlight>();

            Timer.OnTimesUp += OnTimesUp;
        }

        private void OnDestroy()
        {
            Timer.OnTimesUp -= OnTimesUp;
        }

        private bool isPlaying = true;

        private void OnTimesUp()
        {
            isPlaying = false;
        }

        private void Update()
        {
            if (!isPlaying)
                return;

            ItemIconDrag();

            /*if (Input.GetKeyDown(KeyCode.Space))
            {
                if (selectedItem == null)
                {
                    CreateRandomItem();
                }
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                InsertRandomItem();
            }*/

            if (Input.GetKeyDown(KeyCode.R))
            {
                RotateItem();
            }

            if (selectedItemGrid == null)
            {
                inventoryHighlight.Show(false);
                return;
            }

            HandleHighlight();

            if (Input.GetMouseButtonDown(0))
            {
                LeftMouseButtonDown();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                LeftMouseButtonUp();
            }
        }

        private void RotateItem()
        {
            if (selectedItem == null) return;

            selectedItem.Rotate();
        }

        public void InsertRandomItem()
        {
            CreateRandomItem();
            InventoryItem itemToInsert = selectedItem;
            selectedItem = null;
            InsertItem(itemToInsert);
        }

        public void InsertRandomItem(ItemGrid grid)
        {
            CreateRandomItem();
            InventoryItem itemToInsert = selectedItem;
            selectedItem = null;

            // add random rotation to the object
            if (Random.Range(0, 10) < 5)
            {
                itemToInsert.Rotate();
            }

            InsertItem(itemToInsert, grid);
        }

        private void InsertItem(InventoryItem itemToInsert)
        {
            if (selectedItemGrid == null) return;

            Vector2Int? posOnGrid = selectedItemGrid.FindSpaceForObject(itemToInsert);

            if (posOnGrid == null)
            {
                Destroy(itemToInsert.gameObject);
                return;
            }

            selectedItemGrid.PlaceItem(itemToInsert, posOnGrid.Value.x, posOnGrid.Value.y);
        }

        private void InsertItem(InventoryItem itemToInsert, ItemGrid grid)
        {
            if (grid == null) return;

            Vector2Int? posOnGrid = grid.FindSpaceForObject(itemToInsert);

            if (posOnGrid == null)
            {
                Destroy(itemToInsert.gameObject);
                return;
            }

            grid.PlaceItem(itemToInsert, posOnGrid.Value.x, posOnGrid.Value.y);
        }

        private Vector2Int oldPosition;
        private InventoryItem itemToHighlight;

        private void HandleHighlight()
        {
            Vector2Int positionOnTheGrid = GetTileGridPosition();

            if (oldPosition == positionOnTheGrid)
                return;

            oldPosition = positionOnTheGrid;
            if (selectedItem == null)
            {
                itemToHighlight = selectedItemGrid.GetItem(positionOnTheGrid.x, positionOnTheGrid.y);

                if (itemToHighlight != null)
                {
                    inventoryHighlight.Show(true);
                    inventoryHighlight.SetSize(itemToHighlight);
                    inventoryHighlight.SetPosition(selectedItemGrid, itemToHighlight);
                }
                else
                {
                    inventoryHighlight.Show(false);
                }
            }
            else
            {
                inventoryHighlight.Show(selectedItemGrid.BoundryCheck(positionOnTheGrid.x, positionOnTheGrid.y,
                    selectedItem.Width, selectedItem.Height));

                inventoryHighlight.SetSize(selectedItem);
                inventoryHighlight.SetPosition(selectedItemGrid, selectedItem, positionOnTheGrid.x,
                    positionOnTheGrid.y);
            }
        }

        private void CreateRandomItem()
        {
            InventoryItem inventoryItem = Instantiate(itemPrefab, transform).GetComponent<InventoryItem>();
            selectedItem = inventoryItem;

            rectTransform = inventoryItem.GetComponent<RectTransform>();
            rectTransform.SetParent(canvasTransform);
            rectTransform.SetAsLastSibling();

            int selectedItemID = Random.Range(0, _itemsDataList.itemDatas.Count);
            inventoryItem.Set(_itemsDataList.itemDatas[selectedItemID]);
        }

        private void LeftMouseButtonDown()
        {
            var tileGridPosition = GetTileGridPosition();

            if (selectedItem == null)
            {
                PickUpItem(tileGridPosition);
                selectedItemGrid.CheckBasket();
            }
        }

        private void LeftMouseButtonUp()
        {
            var tileGridPosition = GetTileGridPosition();

            if (selectedItem != null && tileGridPosition != null)
            {
                PlaceItem(tileGridPosition);
            }
        }

        private Vector2Int GetTileGridPosition()
        {
            Vector2 position = Input.mousePosition;

            if (selectedItem != null)
            {
                position.x -= (selectedItem.Width - 1) * ItemGrid.tileSizeWidth / 2;
                position.y += (selectedItem.Height - 1) * ItemGrid.tileSizeHeight / 2;
            }

            return selectedItemGrid.GetTileGridPosition(position);
        }

        private void PlaceItem(Vector2Int tileGridPosition)
        {
            bool complete =
                selectedItemGrid.PlaceItem(selectedItem, tileGridPosition.x, tileGridPosition.y, ref overlapItem);
            if (complete)
            {
                selectedItem = null;
                if (overlapItem != null)
                {
                    selectedItem = overlapItem;
                    overlapItem = null;
                    rectTransform = selectedItem.GetComponent<RectTransform>();
                    rectTransform.SetAsLastSibling();
                }
            }
        }

        private void PickUpItem(Vector2Int tileGridPosition)
        {
            selectedItem = selectedItemGrid.PickUpItem(tileGridPosition.x, tileGridPosition.y);
            if (selectedItem != null)
            {
                rectTransform = selectedItem.GetComponent<RectTransform>();
                rectTransform.SetAsLastSibling();
            }
        }

        private void ItemIconDrag()
        {
            if (selectedItem != null)
            {
                rectTransform.transform.position = Input.mousePosition;
            }
        }

#if UNITY_EDITOR
        public void InsertAllItemsEditor(ItemGrid grid)
        {
            for (int i = 0; i < _itemsDataList.itemDatas.Count; i++)
            {
                InsertItemEditor(grid, i);
            }
        }
        
        private void InsertItemEditor(ItemGrid grid, int id)
        {
            CreateItemEditor(id);
            InventoryItem itemToInsert = selectedItem;
            selectedItem = null;

            InsertItem(itemToInsert, grid);
        }

        private void CreateItemEditor(int id)
        {
            InventoryItem inventoryItem = Instantiate(itemPrefab, transform).GetComponent<InventoryItem>();
            selectedItem = inventoryItem;

            rectTransform = inventoryItem.GetComponent<RectTransform>();
            rectTransform.SetParent(canvasTransform);
            rectTransform.SetAsLastSibling();

            int selectedItemID = id;
            inventoryItem.Set(_itemsDataList.itemDatas[selectedItemID]);
        }
#endif
        
    }
}