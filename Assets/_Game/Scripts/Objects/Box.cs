using System;
using DG.Tweening;
using GameTemplate._Game.Scripts.Inventory;
using TMPro;
using UnityEngine;
using VContainer.Unity;

namespace GameTemplate._Game.Scripts
{
    public class Box : MonoBehaviour, IStartable
    {
        public static Action<BoxStatistic, Transform> OnBoxDelivered;

        [SerializeField] private GameObject packButton;
        public TextMeshProUGUI WarningText;
        public RectTransform CapWidth0, CapWidth1;
        public RectTransform CapHeight0, CapHeight1;
        public RectTransform BoxCenter;

        ItemGrid _itemGrid;
        public bool isClosed;
        public bool isTapeRight, isLabelRight;
        Vector2Int boxSize;
        RectTransform _rectTransform;

        public static Action OnBoxDestroyed;

        public bool IsEmpty => _itemGrid.IsEmpty();

        [HideInInspector] public InventoryController _inventoryController;

        public void Start()
        {
            
        }

        public void PackButtonClick()
        {
            if (_itemGrid.IsEmpty())
            {
                ShowWarning("Not Full!");
                return;
            }

            GetComponent<Animator>().SetTrigger("Close");
            _itemGrid.gameObject.SetActive(false);
            packButton.SetActive(false);
            isClosed = true;
        }

        public bool PutTape(Transform tapeTransform)
        {
            if (!isClosed)
            {
                ShowWarning("Not Closed!");
                return false;
            }

            //isTaped = true;
            tapeTransform.SetParent(transform);

            isTapeRight = Mathf.Approximately((tapeTransform.GetComponent<RectTransform>().rect.width /
                                               GlobalVariables.tileSizeWidth), boxSize.x);

            return true;
        }

        public bool PutLabel(Transform labelTransform, Label label)
        {
            /*if (!isTaped)
            {
                ShowWarning("Not Taped!");
                return false;
            }*/

            if (!isClosed)
            {
                ShowWarning("Not Closed!");
                return false;
            }

            isLabelRight = label.CheckItems(_itemGrid.GetItemsInside());

            //isLabeled = true;
            labelTransform.SetParent(transform);
            return true;
        }

        public void ShowWarning(string warning)
        {
            WarningText.text = warning;
            WarningText.DOFade(1, 0);
            WarningText.DOFade(0, .5f).SetDelay(1f);
        }

        public void DeliverBox()
        {
            transform.DOMoveY(transform.position.y - 1080, 3f).OnComplete(DestroyBox);
            //empty count
            //wrong tape
            //wrong label
            BoxStatistic boxStatistic = new BoxStatistic();
            boxStatistic.EmptyCount = _itemGrid.GetEmptyCount();
            boxStatistic.IsRightTape = isTapeRight;
            boxStatistic.IsRightLabel = isLabelRight;
            boxStatistic.BoxValue = _itemGrid.gridSizeWidth * 10;

            OnBoxDelivered?.Invoke(boxStatistic, transform);
        }

        public void SetSize(Vector2Int vector2Int)
        {
            _itemGrid = GetComponentInChildren<ItemGrid>();
            _rectTransform = GetComponent<RectTransform>();
            
            boxSize = vector2Int;
            Vector2 size = vector2Int;
            size.x *= GlobalVariables.tileSizeWidth;
            size.y *= GlobalVariables.tileSizeHeight;
            _rectTransform.sizeDelta = size;
            // 300 200 100
            // 150 100 50
            CapWidth0.sizeDelta = new Vector2(size.x / 2 - 5, CapWidth0.sizeDelta.y);
            CapWidth1.sizeDelta = new Vector2(size.x / 2 - 5, CapWidth1.sizeDelta.y);
            
            CapHeight0.sizeDelta = new Vector2(CapHeight0.sizeDelta.x, size.y / 2 - 5);
            CapHeight1.sizeDelta = new Vector2(CapHeight1.sizeDelta.x, size.y / 2 - 5);

            BoxCenter.sizeDelta = new Vector2(8f * boxSize.x, 8f * boxSize.y);
        }

        public void DestroyBox()
        {
            _inventoryController.GetHighlighterToFillerGrid();
            _inventoryController.isMovingBox = false;
            Destroy(gameObject);
        }

        public bool BeginDrag()
        {
            if (_inventoryController.SelectedItem != null)
                return false;

            _inventoryController.isMovingBox = true;
            return true;
        }

        public void EndDrag()
        {
            if (_inventoryController.SelectedItem != null)
                return;

            _inventoryController.isMovingBox = false;
        }

        private void OnDestroy()
        {
            OnBoxDestroyed?.Invoke();
        }
    }
}