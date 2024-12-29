using System;
using System.Collections;
using DG.Tweening;
using GameTemplate.Systems.Currencies;
using GameTemplate.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameTemplate.UI.Currency
{
    public class CurrencyView : MonoBehaviour
    {
        public TextMeshProUGUI currencyAmountText;
        public Image currencyImage;
        public GameObject AddButton;

        [SerializeField] private float punch = 0.2f;
        [SerializeField] private float punchDuration = 0.5f;
        [SerializeField] private int vibrato = 6;
        [SerializeField] private Transform punchAnimationParent;

        private string currencySign;
        private int _currencyId;
        private int currencyAmount;
        private Coroutine incrementRoutine;
        private Tween punchTween;

        public void Initialize(Sprite currencyIcon, int currencyAmount, bool isBuyable, int currencyId)
        {
            this.currencyImage.sprite = currencyIcon;
            currencyId = currencyId;
            AddButton.SetActive(isBuyable);
            SetCurrency(currencyId, currencyAmount);

            CurrencyService.OnCurrencyChanged += SetCurrency;
        }

        private void OnDestroy()
        {
            CurrencyService.OnCurrencyChanged -= SetCurrency;
        }

        public void SetCurrency(int currencyId, int newValue)
        {
            if (_currencyId != currencyId)
                return;
            
            PunchAnimation();
            currencyAmountText.text = NumberHelper.ToStringScientific(newValue);
        }

        private void PunchAnimation()
        {
            punchTween?.Kill(true);
            punchTween = punchAnimationParent.DOPunchScale(Vector3.one * punch, punchDuration, vibrato);
        }
    }
}