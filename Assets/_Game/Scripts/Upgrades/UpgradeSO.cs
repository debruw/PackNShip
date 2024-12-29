using System;
using GameTemplate.Systems.Currencies;
using GameTemplate.Utils;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.Scripts.Upgrades
{
    [CreateAssetMenu(fileName = "Upgrade", menuName = "Scriptable Objects/Upgrade", order = 0)]
    public class UpgradeSO : ScriptableObject
    {
        public enum UpgradeType
        {
            Time,
            MusicPlayer
        }

        public bool isBuyed
        {
            get => UserPrefs.GetUpgradeState(name);
            set
            {
                UserPrefs.SetUpgradeState(name, value);
                if (value)
                {
                    switch (upgradeType)
                    {
                        case UpgradeType.Time:
                            UserPrefs.SetLevelDuration(UserPrefs.GetLevelDuration() + upgradeValue);
                            break;
                        case UpgradeType.MusicPlayer:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
        }

        public UpgradeType upgradeType;
        public int upgradeLevelRequirement;
        public string upgradeDescription;
        public Sprite upgradeIcon;
        public CurrencyService.CurrencyType upgradeCurrency;
        public int upgradeCost;

        [EnableIf("@upgradeType == UpgradeType.Time")]
        public int upgradeValue;
    }
}