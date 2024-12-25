using GameTemplate.Systems.Currencies;
using GameTemplate.Utils;
using UnityEngine;

namespace _Game.Scripts.Upgrades
{
    [CreateAssetMenu(fileName = "Upgrade", menuName = "Scriptable Objects/Upgrade", order = 0)]
    public class UpgradeSO : ScriptableObject
    {
        public bool isBuyed
        {
            get => UserPrefs.GetUpgradeState(upgradeName.ToString());
            set => UserPrefs.SetUpgradeState(upgradeName, value);
        }

        public int upgradeLevelRequirement;
        public string upgradeName;
        public Sprite upgradeIcon;
        public CurrencyService.CurrencyType upgradeCurrency;
        public int upgradeCost;
    }
}