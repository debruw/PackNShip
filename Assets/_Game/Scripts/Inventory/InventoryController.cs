using System;
using System.Collections.Generic;
using DG.Tweening;
using GameTemplate.Systems.Pooling;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;
using Random = UnityEngine.Random;

namespace GameTemplate._Game.Scripts.Inventory
{
    public class InventoryController : MonoBehaviour
    {
        public static Action OnItemDrop;
        public static Action OnFillingDrop;

        public ItemsDataList _itemsDataList;
        public ItemGrid fillerItemGrid;

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

        [SerializeField]
        private InventoryItem _selectedItem;

        [HideInInspector]
        public InventoryItem SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                if (SelectedItem != null)
                {
                    _selectedRectTransform = _selectedItem.GetComponent<RectTransform>();
                }
                else
                {
                    _selectedRectTransform = null;
                }
            }
        }

        private RectTransform _selectedRectTransform;

        public GameObject itemPrefab;
        public RectTransform canvasTransform;
        private InventoryHighlight inventoryHighlight;
        private PoolingService _poolingService;
        
        //these two value is needed for returning item to the original place when there is no room for it
        Vector2Int pickedGridPosition;
        ItemGrid pickedItemGrid;

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

            if (isMovingBox && SelectedItem == null)
                return;

            ItemIconDrag();

            if (Input.GetKeyDown(KeyCode.R))
            {
                RotateItem();
            }
            
            if (Input.GetMouseButtonUp(0))
            {
                LeftMouseButtonUp();
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
            
        }

        private void RotateItem()
        {
            if (SelectedItem == null) return;

            SelectedItem.Rotate();
        }

        public InventoryItem InsertRandomItem(ItemGrid grid, List<ItemData> orderItems)
        {
            InventoryItem newItem = CreateRandomItem(orderItems);

            // add random rotation to the object
            if (Random.Range(0, 10) < 5)
            {
                newItem.Rotate();
            }

            bool isInserted = InsertItem(newItem, grid);
            return isInserted ? newItem : null;
        }

        private bool InsertItem(InventoryItem itemToInsert, ItemGrid grid)
        {
            if (grid == null) return false;

            Vector2Int? posOnGrid = grid.FindSpaceForObject(itemToInsert);

            if (posOnGrid == null)
            {
                Destroy(itemToInsert.gameObject);
                return false;
            }

            grid.PlaceItem(itemToInsert, posOnGrid.Value.x, posOnGrid.Value.y);
            return true;
        }

        public List<InventoryItem> InsertItemsForTutorial(ItemGrid grid)
        {
            List<InventoryItem> items = new List<InventoryItem>();
            for (int i = 0; i < 2; i++)
            {
                InventoryItem newItem = CreateItem(i);
                items.Add(newItem);

                InsertItem(newItem, grid);
            }

            return items;
        }

        private Vector2Int oldPosition;
        private InventoryItem itemToHighlight;

        private void HandleHighlight()
        {
            Vector2Int positionOnTheGrid = GetTileGridPosition();

            if (oldPosition == positionOnTheGrid)
                return;

            oldPosition = positionOnTheGrid;
            if (SelectedItem == null)
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
                inventoryHighlight.Show(selectedItemGrid.BoundaryCheck(positionOnTheGrid.x, positionOnTheGrid.y,
                    SelectedItem.Width, SelectedItem.Height));

                //////inventoryHighlight.SetColor(selectedItemGrid.OverlapCheck(positionOnTheGrid.x,positionOnTheGrid.y,SelectedItem.Width, SelectedItem.Height,SelectedItem.itemData.structure));
                inventoryHighlight.SetSize(SelectedItem);
                inventoryHighlight.SetPosition(selectedItemGrid, SelectedItem, positionOnTheGrid.x,
                    positionOnTheGrid.y);
            }
        }

        private InventoryItem CreateRandomItem(List<ItemData> orderItems)
        {
            InventoryItem inventoryItem =
                _poolingService.GetGameObjectById(PoolID.ItemPrefab).GetComponent<InventoryItem>();

            RectTransform itemRectTransform = inventoryItem.GetComponent<RectTransform>();
            itemRectTransform = inventoryItem.GetComponent<RectTransform>();
            itemRectTransform.SetParent(canvasTransform);
            itemRectTransform.SetAsLastSibling();

            int selectedItemID = Random.Range(0, _itemsDataList.itemDatas.Count);
            while (orderItems.Contains(_itemsDataList.itemDatas[selectedItemID]))
            {
                selectedItemID = Random.Range(0, _itemsDataList.itemDatas.Count);
            }

            inventoryItem.Set(_itemsDataList.itemDatas[selectedItemID]);

            return inventoryItem;
        }

        private InventoryItem CreateItem(int itemId)
        {
            InventoryItem inventoryItem =
                _poolingService.GetGameObjectById(PoolID.ItemPrefab).GetComponent<InventoryItem>();

            RectTransform itemRectTransform = inventoryItem.GetComponent<RectTransform>();
            itemRectTransform = inventoryItem.GetComponent<RectTransform>();
            itemRectTransform.SetParent(canvasTransform);
            itemRectTransform.SetAsLastSibling();

            inventoryItem.Set(_itemsDataList.itemDatas[itemId]);

            return inventoryItem;
        }

        private void LeftMouseButtonDown()
        {
            var tileGridPosition = GetTileGridPosition();

            if (SelectedItem == null)
            {
                PickUpItem(tileGridPosition);
                selectedItemGrid.CheckBasket();
            }
        }

        // This function is called before selectedItemGrid check on update function
        
        private void LeftMouseButtonUp()
        {
            if (selectedItemGrid == null)
            {
                // if item is not dropped on a grid send it back to where its picked
                PlaceItemToWhereItsPicked();
                SelectedItem.transform.DOScale(1, .1f);
                SelectedItem = null;
                return;
            }
            
            var tileGridPosition = GetTileGridPosition();

            if (SelectedItem != null && tileGridPosition != null)
            {
                TryPlaceItem(tileGridPosition);
            }
        }

        private Vector2Int GetTileGridPosition()
        {
            Vector2 position = Input.mousePosition;

            if (SelectedItem != null)
            {
                position.x -= (SelectedItem.Width - 1) * GlobalVariables.tileSizeWidth / 2;
                position.y += (SelectedItem.Height - 1) * GlobalVariables.tileSizeHeight / 2;
                position = position + offset;
            }

            return selectedItemGrid.GetTileGridPosition(position);
        }

        private void TryPlaceItem(Vector2Int tileGridPosition)
        {
            bool isPlaced = selectedItemGrid.CheckPlaceable(SelectedItem, tileGridPosition.x, tileGridPosition.y);
            if (isPlaced)
            {
                if (selectedItemGrid.name.Contains("Box"))
                {
                    OnFillingDrop?.Invoke(); // For tutorial
                }
                else
                {
                    OnItemDrop?.Invoke(); // For tutorial
                }
                pickedGridPosition = Vector2Int.zero;
            }
            else
            {
                PlaceItemToWhereItsPicked();
            }

            SelectedItem.transform.DOScale(1, .1f);
            SelectedItem = null;
        }

        private void PickUpItem(Vector2Int tileGridPosition)
        {
            SelectedItem = selectedItemGrid.PickUpItem(tileGridPosition.x, tileGridPosition.y);
            Debug.Log(SelectedItem.gameObject.name);

            if (SelectedItem != null)
            {
                offset = SelectedItem.transform.position - Input.mousePosition;
                SelectedItem.transform.DOScale(1.1f, .1f);
                _selectedRectTransform.SetParent(transform);
                _selectedRectTransform.SetAsLastSibling();
                pickedGridPosition = tileGridPosition;
                pickedItemGrid = selectedItemGrid;
            }
        }

        private void PlaceItemToWhereItsPicked()
        {
            // place item to where its picked 
            Vector2Int? posOnGrid = pickedItemGrid.FindSpaceForObject(SelectedItem);
            pickedItemGrid.PlaceItem(SelectedItem, posOnGrid.Value.x, posOnGrid.Value.y);
        }

        private Vector2 _pos;

        private void ItemIconDrag()
        {
            if (SelectedItem != null)
            {
                _pos = Input.mousePosition + (Vector3)offset;
                _pos.x = Mathf.Clamp(_pos.x, _selectedRectTransform.rect.width / 2,
                    Screen.width - (_selectedRectTransform.rect.width / 2));
                _pos.y = Mathf.Clamp(_pos.y, _selectedRectTransform.rect.height / 2,
                    Screen.height - (_selectedRectTransform.rect.height / 2));

                _selectedRectTransform.transform.position = _pos;
            }
        }

        public void GetHighlighterToFillerGrid()
        {
            SelectedItemGrid = null;
            inventoryHighlight.SetParent(fillerItemGrid);
        }

        private InventoryItem CreateFilling()
        {
            InventoryItem inventoryItem =
                _poolingService.GetGameObjectById(PoolID.ItemPrefab).GetComponent<InventoryItem>();

            RectTransform itemRectTransform = inventoryItem.GetComponent<RectTransform>();
            itemRectTransform = inventoryItem.GetComponent<RectTransform>();
            itemRectTransform.SetParent(canvasTransform);
            itemRectTransform.SetAsLastSibling();

            inventoryItem.Set(_itemsDataList.fillingData);

            return inventoryItem;
        }

        public void InsertFilling(ItemGrid itemGrid)
        {
            InventoryItem newItem = CreateFilling();
            InsertItem(newItem, itemGrid);
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
            InventoryItem itemToInsert = CreateItemEditor(id);

            InsertItem(itemToInsert, grid);
        }

        private InventoryItem CreateItemEditor(int id)
        {
            InventoryItem inventoryItem = Instantiate(itemPrefab, transform).GetComponent<InventoryItem>();
            RectTransform rectTransform = inventoryItem.GetComponent<RectTransform>();

            rectTransform.SetParent(canvasTransform);
            rectTransform.SetAsLastSibling();

            int selectedItemID = id;
            inventoryItem.Set(_itemsDataList.itemDatas[selectedItemID]);
            return inventoryItem;
        }
#endif
    }
}