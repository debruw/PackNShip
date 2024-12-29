using GameTemplate.Systems.Currencies;
using GameTemplate.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.Upgrades
{
    public class UpgradeGridItem : MonoBehaviour
    {
        [FormerlySerializedAs("nameText")] public TextMeshProUGUI descriptionText;
        public TextMeshProUGUI costText;
        public TextMeshProUGUI levelText;
        public Image iconImg;
        public GameObject LockGameObject;

        UpgradeSO _upgradeSO;

        CurrencyService _CurrencyService;

        [Inject]
        public void Construct(ICurrencyService currencyService)
        {
            Debug.Log("Construct UpgradeGridItem");
            _CurrencyService = currencyService as CurrencyService;
        }

        public void Initialize(UpgradeSO upgradeSO)
        {
            _upgradeSO = upgradeSO;

            descriptionText.text = _upgradeSO.upgradeDescription;
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
            if (_CurrencyService.GetCurrencyValue(_upgradeSO.upgradeCurrency) >= _upgradeSO.upgradeCost)
            {
                _CurrencyService.SpendCurrency(new CurrencyArgs((int)CurrencyService.CurrencyType.Money,
                    _upgradeSO.upgradeCost));
                _upgradeSO.isBuyed = true;
                gameObject.SetActive(false);
            }
        }
    }
}