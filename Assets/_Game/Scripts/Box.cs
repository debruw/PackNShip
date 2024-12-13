using System;
using DG.Tweening;
using GameTemplate._Game.Scripts.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace GameTemplate._Game.Scripts
{
    public class Box : MonoBehaviour, IStartable
    {
        public static Action<BoxStatistic> OnBoxDelivered;

        [SerializeField] private GameObject packButton;
        public Sprite ClosedBox;
        public TextMeshProUGUI WarningText;

        ItemGrid _itemGrid;
        public bool isClosed;
        public bool isTapeRight, isLabelRight;
        Vector2Int boxSize;

        public static Action OnBoxDestroyed;

        public bool IsEmpty => _itemGrid.IsEmpty();

        [HideInInspector] public InventoryController _inventoryController;

        public void Start()
        {
            _itemGrid = GetComponentInChildren<ItemGrid>();
        }

        public void PackButtonClick()
        {
            if (_itemGrid.IsEmpty())
            {
                ShowWarning("Not Full!");
                return;
            }

            GetComponent<Image>().sprite = ClosedBox;
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
                                               ItemGrid.tileSizeWidth), boxSize.x);

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

            OnBoxDelivered?.Invoke(boxStatistic);
        }

        public void SetSize(Vector2Int vector2Int)
        {
            boxSize = vector2Int;
        }

        public void DestroyBox()
        {
            _inventoryController.GetHighlighterToMainGrid();
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