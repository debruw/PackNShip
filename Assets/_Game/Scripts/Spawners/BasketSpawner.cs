using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace GameTemplate._Game.Scripts
{
    public class BasketSpawner : MonoBehaviour
    {
        public GameObject basketPrefab;
        private GameObject basket;

        private void Awake()
        {
            SpawnBox();
        }

        void SpawnBox()
        {
            basket = Instantiate(basketPrefab, transform);
            basket.transform.DOLocalMoveY(0, 1f);
        }

        public async UniTask SpawnNewBasket()
        {
            await UniTask.Delay(2000);
            SpawnBox();
        }
    }
}