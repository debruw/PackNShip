using DG.Tweening;
using GameTemplate._Game.Scripts.Inventory;
using GameTemplate.Systems.Pooling;
using UnityEngine;
using VContainer;

namespace GameTemplate._Game.Scripts
{
    public class LabelSpawner : MonoBehaviour
    {
        public Transform spawnPoint;

        private GameObject spawnedLabel;

        PoolingService _poolingService;

        [Inject]
        public void Construct(InventoryController inventoryController, PoolingService poolingService)
        {
            Debug.Log("Construct LabelSpawner");
            _poolingService = poolingService;
        }

        public bool SpawnLabel(InventoryItem item)
        {
            if (spawnedLabel != null) return false;

            spawnedLabel = _poolingService.GetGameObjectById(PoolID.LabelPrefab);
            spawnedLabel.transform.SetParent(spawnPoint);
            spawnedLabel.transform.localPosition = Vector3.zero;
            spawnedLabel.GetComponent<Label>().SetSprite(item.itemData);

            Vector2 size = item.SmallestSide == 1 ? new Vector2(80, 80) : new Vector2(100, 100);
            spawnedLabel.GetComponent<RectTransform>().sizeDelta = size;
            spawnedLabel.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, -125f), 1f);
            return true;
        }
    }
}