using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GameTemplate._Game.Scripts;
using GameTemplate.Systems.Currencies;
using GameTemplate.Systems.Pooling;
using GameTemplate.UI;
using TMPro;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameTemplate.Systems.FloatingText
{
    public class FloatingTextService : IStartable
    {
        PoolingService _poolingService;
        CurrencyService _currencyService;
        ApplicationCanvas _applicationCanvas;
        
        public void Start()
        {
            Debug.Log("start FloatingTextService");
        }

        [Inject]
        public void Construct(PoolingService poolingService, ICurrencyService currencyService, ApplicationCanvas applicationCanvas)
        {
            Debug.Log("Construct FloatingTextService");
            _poolingService = poolingService;
            _currencyService = currencyService as CurrencyService;
            _applicationCanvas = applicationCanvas;

            Box.OnBoxDelivered += OnBoxDelivered;
            Timer.OnTimesUp += OnTimesUp;
        }

        private void OnTimesUp()
        {
            Box.OnBoxDelivered -= OnBoxDelivered;
            Timer.OnTimesUp -= OnTimesUp;
        }

        private void OnBoxDelivered(BoxStatistic statistics, Transform boxTransform)
        {
            SpawnText("50$", Color.green, boxTransform.position);
            _currencyService.EarnCurrency(new CurrencyArgs((int)CurrencyService.CurrencyType.Money, 50));
        }

        public async UniTask SpawnText(string text, Color clr, Vector3 position)
        {
            Transform newText = _poolingService.GetGameObjectById(PoolID.FloatingText).transform;
            newText.SetParent(_applicationCanvas.transform);
            TextMeshProUGUI textMesh = newText.GetComponent<TextMeshProUGUI>();
            textMesh.text = text;
            textMesh.color = clr;
            newText.transform.position = position;
            newText.DOMoveY(newText.position.y + 100, 2f);

            await UniTask.Delay(2000);
            textMesh.DOFade(0, .25f);
        }
    }
}