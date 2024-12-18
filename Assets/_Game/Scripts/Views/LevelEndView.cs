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
        RankSystem _rankSystem;

        [Inject]
        public void Construct(Statistics Statistics, RankSystem rankSystem)
        {
            _statistics = Statistics;
            _rankSystem = rankSystem;
        }

        private void Start()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        const string mSpace = "<mspace=30>";
        public void InitializeAndShow()
        {
            Debug.Log("LevelEndView initialized");
            txtPackagePacked.text = mSpace + _statistics.DeliveredBoxCount;
            txtStolenItems.text = mSpace + _statistics.StolenItemsCount;
            txtMoneyEarned.text = mSpace + _statistics.MoneyCount;

            txtEmptySpots.text = mSpace + _statistics.TotalEmptyCountCount;
            txtWrongTape.text = mSpace + _statistics.TotalWrongTapeCount;
            txtWrongLabel.text = mSpace + _statistics.TotalWrongLabelCount;

            int totalPoint = _statistics.TotalPoint;
            txtTotal.text = mSpace + totalPoint;
            
            _rankSystem.AddPoints(totalPoint);

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