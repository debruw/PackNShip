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

        public GameObject SpawnLabel(Order order)
        {
            spawnedLabel = _poolingService.GetGameObjectById(PoolID.LabelPrefab);
            spawnedLabel.transform.SetParent(spawnPoint);
            spawnedLabel.transform.localPosition = Vector3.zero;
            spawnedLabel.GetComponent<Label>().SetOrder(order);

            spawnedLabel.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, -125f), 1f).OnComplete(() =>
            {
                spawnedLabel.GetComponent<BoxCollider2D>().isTrigger = false;
            });
            return spawnedLabel;
        }
    }
}