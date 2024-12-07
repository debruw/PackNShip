using System;
using System.Collections.Generic;
using DG.Tweening;
using GameTemplate.Systems.Pooling;
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
        public ItemGrid mainItemGrid;

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

        [HideInInspector] public InventoryItem selectedItem;
        InventoryItem overlapItem;
        RectTransform rectTransform;

        public GameObject itemPrefab;
        public RectTransform canvasTransform;
        private InventoryHighlight inventoryHighlight;
        private PoolingService _poolingService;

        private Vector2 offset;

        [Inject]
        public void Construct(ItemsDataList itemsDataList, PoolingService poolingService)
        {
            _itemsDataList = itemsDataList;
            _poolingService = poolingService;
        }

        private void Awake()
        {
            inventoryHighlight = GetComponent<InventoryHighlight>();

            Timer.OnTimesUp += OnTimesUp;
        }

        private void OnDestroy()
        {
            Timer.OnTimesUp -= OnTimesUp;
        }

        private bool isPlaying = true;
        [HideInInspector] public bool isMovingBox = false;

        private void OnTimesUp()
        {
            isPlaying = false;
        }

        private void Update()
        {
            if (!isPlaying)
                return;

            if (isMovingBox && selectedItem == null)
                return;

            ItemIconDrag();

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

        public InventoryItem InsertRandomItem(ItemGrid grid, List<ItemData> orderItems)
        {
            CreateRandomItem(orderItems);
            
            InventoryItem itemToInsert = selectedItem;
            selectedItem.transform.DOScale(1f, .1f);
            selectedItem = null;

            // add random rotation to the object
            if (Random.Range(0, 10) < 5)
            {
                itemToInsert.Rotate();
            }

            InsertItem(itemToInsert, grid);
            return itemToInsert;
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

        private void CreateRandomItem(List<ItemData> orderItems)
        {
            InventoryItem inventoryItem =
                _poolingService.GetGameObjectById(PoolID.ItemPrefab).GetComponent<InventoryItem>();
            selectedItem = inventoryItem;

            rectTransform = inventoryItem.GetComponent<RectTransform>();
            rectTransform.SetParent(canvasTransform);
            rectTransform.SetAsLastSibling();

            int selectedItemID = Random.Range(0, _itemsDataList.itemDatas.Count);
            while (orderItems.Contains(_itemsDataList.itemDatas[selectedItemID]))
            {
                Debug.Log("same item");
                selectedItemID = Random.Range(0, _itemsDataList.itemDatas.Count);
            }
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
                position = position + offset;
            }

            return selectedItemGrid.GetTileGridPosition(position);
        }

        private void PlaceItem(Vector2Int tileGridPosition)
        {
            bool complete =
                selectedItemGrid.PlaceItem(selectedItem, tileGridPosition.x, tileGridPosition.y, ref overlapItem);
            if (complete)
            {
                selectedItem.transform.DOScale(1, .1f);
                selectedItem = null;
                if (overlapItem != null)
                {
                    selectedItem = overlapItem;
                    offset = selectedItem.transform.position - Input.mousePosition;
                    selectedItem.transform.DOScale(1.1f, .1f);
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
                offset = selectedItem.transform.position - Input.mousePosition;
                selectedItem.transform.DOScale(1.1f, .1f);
                rectTransform = selectedItem.GetComponent<RectTransform>();
                rectTransform.SetParent(transform);
                rectTransform.SetAsLastSibling();
            }
        }

        private Vector2 _pos;

        private void ItemIconDrag()
        {
            if (selectedItem != null)
            {
                _pos = Input.mousePosition + (Vector3)offset;
                _pos.x = Mathf.Clamp(_pos.x, rectTransform.rect.width / 2,
                    Screen.width - (rectTransform.rect.width / 2));
                _pos.y = Mathf.Clamp(_pos.y, rectTransform.rect.height / 2,
                    Screen.height - (rectTransform.rect.height / 2));

                rectTransform.transform.position = _pos;
            }
        }

        public void GetHighlighterFromBox()
        {
            SelectedItemGrid = null;
            inventoryHighlight.SetParent(mainItemGrid);
            isMovingBox = false;
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