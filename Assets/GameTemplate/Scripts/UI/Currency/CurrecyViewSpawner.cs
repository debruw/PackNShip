using System.Collections.Generic;
using GameTemplate.Systems.Currencies;
using UnityEngine;
using VContainer;

namespace GameTemplate.UI.Currency
{
    public class CurrecyViewSpawner : MonoBehaviour
    {
        [SerializeField] private Transform currencyParent;
        [SerializeField] private GameObject CurrencyUIPrefab;
        public List<CurrencyView> currencyPanels = new List<CurrencyView>();
        
        ICurrencyService _CurrencyManager;
        
        [Inject]
        public void Construct(ICurrencyService currencyManager)
        {
            _CurrencyManager = currencyManager;
            
            List<Systems.Currencies.Currency> currencies = _CurrencyManager.GetCurrencies();

            for (int i = 0; i < currencies.Count; i++)
            {
                currencyPanels.Add(Instantiate(CurrencyUIPrefab, currencyParent).GetComponent<CurrencyView>());
                currencyPanels[i].transform.SetParent(currencyParent);
                currencyPanels[i].Initialize(currencies[i].currencyImage, currencies[i].currencyAmount, currencies[i].isBuyable, currencies[i].CurrencyId);
            }
        }
    }
}