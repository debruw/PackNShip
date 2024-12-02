using DG.Tweening;
using GameTemplate._Game.Scripts.Inventory;
using UnityEngine;

namespace GameTemplate._Game.Scripts
{
    public class TapeSpawner : MonoBehaviour
    {
        public GameObject tapePrefab;
        public Transform spawnPoint;

        GameObject spawnedTape;

        public void SpawnTape(int size)
        {
            //if (spawnedTape != null) return;

            spawnedTape = Instantiate(tapePrefab, spawnPoint);
            RectTransform rectTransform = spawnedTape.GetComponent<RectTransform>();
            Vector2 sizeVec = rectTransform.sizeDelta;
            sizeVec.x = size * ItemGrid.tileSizeWidth;
            rectTransform.sizeDelta = sizeVec;
            rectTransform.anchoredPosition = new Vector2(sizeVec.x / 2, rectTransform.anchoredPosition.y);
            rectTransform.DOAnchorPosX(-sizeVec.x / 2, .5f);
        }
    }
}