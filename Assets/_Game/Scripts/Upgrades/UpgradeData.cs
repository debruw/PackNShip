using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Upgrades
{
    [CreateAssetMenu(fileName = "UpgradeData", menuName = "Scriptable Objects/Upgrade Data", order = 0)]
    public class UpgradeData : ScriptableObject
    {
        public List<UpgradeSO> upgrades = new List<UpgradeSO>();
    }
}