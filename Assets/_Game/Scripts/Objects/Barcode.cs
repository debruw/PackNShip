using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameTemplate._Game.Scripts
{
    public class Barcode : MonoBehaviour
    {
        private TextMeshProUGUI _text;

        private void Start()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }
        
        public async UniTask FlashRed()
        {
            _text.color = Color.red;
            await UniTask.Delay(200);
            _text.color = Color.white;
        }
    }
}