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
            if (spawnedTape != null) return;

            spawnedTape = Instantiate(tapePrefab, spawnPoint);
            Vector2 sizeVec = spawnedTape.GetComponent<RectTransform>().sizeDelta;
            sizeVec.x = size * ItemGrid.tileSizeWidth;
            spawnedTape.GetComponent<RectTransform>().sizeDelta = sizeVec;
            spawnedTape.transform.DOLocalMoveX(-250f, 1f);
        }
    }
}