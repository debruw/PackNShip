using DG.Tweening;
using GameTemplate._Game.Scripts.Inventory;
using GameTemplate.Systems.Pooling;
using UnityEngine;
using VContainer;

namespace GameTemplate._Game.Scripts
{
    public class TapeSpawner : MonoBehaviour
    {
        public Transform spawnPoint;

        GameObject spawnedTape;
        private int spawnedTapeCount;

        PoolingService _poolingService;

        [Inject]
        public void Construct(PoolingService poolingService)
        {
            Debug.Log("Construct TapeSpawner");
            _poolingService = poolingService;
            TapeDrag.OnTapeDestroyed += () => spawnedTapeCount--;
        }

        public void SpawnTape(int size)
        {
            if (spawnedTapeCount > 10)
                return;

            spawnedTapeCount++;
            spawnedTape = _poolingService.GetGameObjectById(PoolID.TapePrefab);

            RectTransform rectTransform = spawnedTape.GetComponent<RectTransform>();
            rectTransform.SetParent(spawnPoint);
            rectTransform.anchoredPosition = Vector2.zero;

            Vector2 sizeVec = rectTransform.sizeDelta;
            sizeVec.x = size * GlobalVariables.tileSizeWidth;

            rectTransform.sizeDelta = sizeVec;
            rectTransform.anchoredPosition = new Vector2(sizeVec.x / 2, rectTransform.anchoredPosition.y);

            rectTransform.DOAnchorPosX(-sizeVec.x / 2, .5f);
        }
    }
}