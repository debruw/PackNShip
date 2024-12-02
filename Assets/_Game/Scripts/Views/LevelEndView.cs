using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using VContainer;

namespace GameTemplate._Game.Scripts.Views
{
    public class LevelEndView : MonoBehaviour
    {
        public TextMeshProUGUI txtPackagePacked;
        public TextMeshProUGUI txtStolenItems;
        public TextMeshProUGUI txtMoneyEarned;
        public TextMeshProUGUI txtEmptySpots;
        public TextMeshProUGUI txtWrongTape;
        public TextMeshProUGUI txtWrongLabel;

        public TextMeshProUGUI txtTotal;

        CanvasGroup _canvasGroup;

        private Statistics _statistics;

        [Inject]
        public void Construct(Statistics Statistics)
        {
            _statistics = Statistics;
        }

        private void Start()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        const string mSpace = "<mspace=30>";
        public void InitializeAndShow()
        {
            Debug.Log("LevelEndView initialized");
            txtPackagePacked.text = mSpace + _statistics.DeliveredBoxCount.ToString("000");
            txtStolenItems.text = mSpace + _statistics.StolenItemsCount.ToString("000");
            txtMoneyEarned.text = mSpace + _statistics.MoneyCount.ToString("000");

            txtEmptySpots.text = mSpace + _statistics.TotalEmptyCountCount.ToString("000");
            txtWrongTape.text = mSpace + _statistics.TotalWrongTapeCount.ToString("000");
            txtWrongLabel.text = mSpace + _statistics.TotalWrongLabelCount.ToString("000");

            txtTotal.text = mSpace + _statistics.TotalPoint.ToString("0000");

            ActivatePanel();
        }

        void ActivatePanel()
        {
            _canvasGroup.DOFade(1, .25f);
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }
    }
}