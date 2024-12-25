using System;
using GameTemplate.Systems.Currencies;
using GameTemplate.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.Upgrades
{
    public class UpgradeGridItem : MonoBehaviour, IStartable
    {
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI costText;
        public TextMeshProUGUI levelText;
        public Image iconImg;
        public GameObject LockGameObject;

        UpgradeSO _upgradeSO;

        CurrencyService _CurrencyService;
        UpgradeService _UpgradeService;

        [Inject]
        public void Construct(ICurrencyService currencyService, UpgradeService upgradeService)
        {
            _CurrencyService = currencyService as CurrencyService;
            _UpgradeService = upgradeService;
        }

        public void Start()
        {
        }

        public void Initialize(UpgradeSO upgradeSO)
        {
            Debug.Log("asdasda");
            _upgradeSO = upgradeSO;

            nameText.text = _upgradeSO.name;
            costText.text = _upgradeSO.upgradeCost.ToString();
            levelText.text = "Need lvl." + _upgradeSO.upgradeLevelRequirement;
            iconImg.sprite = _upgradeSO.upgradeIcon;

            if (UserPrefs.GetRank() < _upgradeSO.upgradeLevelRequirement)
            {
                LockGameObject.SetActive(true);
            }
        }

        public void BuyButtonClick()
        {
            if (_CurrencyService.GetCurrencyValue(CurrencyService.CurrencyType.Money) >= _upgradeSO.upgradeCost)
            {
                _CurrencyService.SpendCurrency(new CurrencyArgs((int)CurrencyService.CurrencyType.Money,
                    _upgradeSO.upgradeCost));
                _UpgradeService.BuyUpgrade(_upgradeSO);
            }
        }
    }
}