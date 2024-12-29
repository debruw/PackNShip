using System;
using GameTemplate.Systems.Pooling;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.Upgrades
{
    public class UpgradeObjectsSpawner : MonoBehaviour
    {
        UpgradeData _upgradeData;
        
        [Inject]
        public void Construct(UpgradeData upgradeData)
        {
            Debug.Log("Construct UpgradeService");

            _upgradeData = upgradeData;

            CheckSpawnableUpgrades();
        }

        private void CheckSpawnableUpgrades()
        {
            foreach (var upgradeSO in _upgradeData.upgrades)
            {
                if (upgradeSO.upgradeType == UpgradeSO.UpgradeType.Spawnable)
                {
                    GameObject newObject = Instantiate(upgradeSO.upgradeSpawnable, transform);
                }
            }
        }
    }
}