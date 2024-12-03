using System;
using UnityEngine;

namespace GameTemplate._Game.Scripts
{
    public class Statistics : MonoBehaviour
    {
        int _deliveredBoxCount,
            _stolenItemsCount,
            _moneyCount,
            _totalEmptyCount,
            _totalWrongTapeCount,
            _totalWrongLabelCount;

        private int _totalPoint;

        public int DeliveredBoxCount
        {
            get { return _deliveredBoxCount; }
        }

        public int StolenItemsCount
        {
            get { return _stolenItemsCount; }
        }

        public int MoneyCount
        {
            get
            {
                _moneyCount = _deliveredBoxCount * 50;
                return _moneyCount;
            }
        }

        public int TotalEmptyCountCount
        {
            get { return _totalEmptyCount; }
        }

        public int TotalWrongTapeCount
        {
            get { return _totalWrongTapeCount; }
        }

        public int TotalWrongLabelCount
        {
            get { return _totalWrongLabelCount; }
        }
        
        public int TotalPoint
        {
            get
            {
                _totalPoint += _deliveredBoxCount * 50;
                _totalPoint += _stolenItemsCount * 10;
                _totalPoint -= _totalEmptyCount * 1;
                _totalPoint -= _totalWrongTapeCount * 5;
                _totalPoint -= _totalWrongLabelCount * 5;
                
                return _totalPoint;
            }
        }

        private void Awake()
        {
            Box.OnBoxDelivered += OnBoxDelivered;
        }

        private void OnDestroy()
        {
            Box.OnBoxDelivered -= OnBoxDelivered;
        }

        private void OnBoxDelivered(BoxStatistic boxStatistic)
        {
            _deliveredBoxCount++;

            _totalEmptyCount += boxStatistic.EmptyCount;

            if (!boxStatistic.IsRightTape) _totalWrongTapeCount++;

            if (!boxStatistic.IsRightLabel) _totalWrongLabelCount++;
        }
    }
}