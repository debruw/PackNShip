using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;

namespace GameTemplate.Systems.Currencies
{
    [CreateAssetMenu(fileName = "CurrencyData", menuName = "Scriptable Objects/Currency Manager")]
    public class CurrencyData : ScriptableObject, IStartable
    {
        public List<Currency> currencies = new List<Currency>();
        public void Start()
        {
            Debug.Log("Currency Data");
        }
    }
}