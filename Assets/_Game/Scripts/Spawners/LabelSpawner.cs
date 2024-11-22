using DG.Tweening;
using GameTemplate._Game.Scripts.Inventory;
using UnityEngine;

namespace GameTemplate._Game.Scripts
{
    public class LabelSpawner : MonoBehaviour
    {
        public GameObject labelPrefab;
        public Transform spawnPoint;

        private GameObject spawnedLabel;

        public bool SpawnLabel(InventoryItem item)
        {
            if (spawnedLabel != null) return false;

            spawnedLabel = Instantiate(labelPrefab, spawnPoint);
            spawnedLabel.GetComponent<Label>().SetSprite(item.itemData.itemIcon);
            
            Vector2 size = item.SmallestSide == 1 ? new Vector2(80, 80) : new Vector2(100, 100);
            spawnedLabel.GetComponent<RectTransform>().sizeDelta = size;
            spawnedLabel.transform.DOLocalMoveY(-125f, 1f);
            return true;
        }
    }
}