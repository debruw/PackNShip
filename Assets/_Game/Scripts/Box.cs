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
        [SerializeField] private GameObject packButton;
        public Sprite ClosedBox;
        public TextMeshProUGUI WarningText;

        ItemGrid itemGrid;
        public bool isClosed, isTaped, isLabeled;

        private void Awake()
        {
            itemGrid = GetComponentInChildren<ItemGrid>();
        }

        public void PackButtonClick()
        {
            if (itemGrid.IsEmpty())
            {
                ShowWarning("Not Full!");
                return;
            }

            GetComponent<Image>().sprite = ClosedBox;
            itemGrid.gameObject.SetActive(false);
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
            return true;
        }

        public bool PutLabel(Transform labelTransform)
        {
            if (!isTaped)
            {
                ShowWarning("Not Taped!");
                return false;
            }

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
    }
}