using System;
using DG.Tweening;
using GameTemplate._Game.Scripts.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameTemplate._Game.Scripts
{
    public class Box : MonoBehaviour
    {
        public static Action<BoxStatistic> OnBoxDelivered;

        [SerializeField] private GameObject packButton;
        public Sprite ClosedBox;
        public TextMeshProUGUI WarningText;

        ItemGrid _itemGrid;
        public bool isClosed, isTaped, isLabeled;
        public bool isTapeRight, isLabelRight;
        Vector2Int boxSize;

        public bool IsEmpty => _itemGrid.IsEmpty();

        private void Awake()
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

            isTaped = true;
            tapeTransform.SetParent(transform);

            isTapeRight = Mathf.Approximately((tapeTransform.GetComponent<RectTransform>().rect.width /
                                               ItemGrid.tileSizeWidth), boxSize.x);

            return true;
        }

        public bool PutLabel(Transform labelTransform, Label label)
        {
            if (!isTaped)
            {
                ShowWarning("Not Taped!");
                return false;
            }

            isLabelRight = label._itemData.itemType == _itemGrid.GetItemInside().itemType;

            isLabeled = true;
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
            //empty count
            //wrong tape
            //wrong label
            BoxStatistic boxStatistic = new BoxStatistic();
            boxStatistic.EmptyCount = _itemGrid.GetEmptyCount();
            boxStatistic.IsRightTape = isTapeRight;
            boxStatistic.IsRightLabel = isLabelRight;

            OnBoxDelivered?.Invoke(boxStatistic);
        }

        public ItemData GetItemInside()
        {
            return _itemGrid.GetItemInside();
        }

        public void SetSize(Vector2Int vector2Int)
        {
            boxSize = vector2Int;
        }
    }
}