using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace GameTemplate._Game.Scripts
{
    public class Barcode : MonoBehaviour
    {
        private Image _image;

        private void Start()
        {
            _image = GetComponent<Image>();
        }
        
        public async UniTask FlashRed()
        {
            _image.color = Color.red;
            await UniTask.Delay(200);
            _image.color = Color.white;
        }
    }
}