using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace GameTemplate.Systems.Pooling
{
    public class PoolElement : MonoBehaviour
    {
        private PoolID poolId;

        private bool goBackOnDisable;
        private bool isApplicationQuitting;

        public PoolID PoolId { get => poolId; set => poolId = value; }
        
        [Inject]
        private PoolingService _poolingService;

        public void Initialize(bool gBackOnDisable, PoolID poolId)
        {
            this.goBackOnDisable = gBackOnDisable;
            this.poolId = poolId;
        }

        private void OnDisable()
        {
            if(!isApplicationQuitting && goBackOnDisable)
            {
                GoBackToPool();
            }
        }

        private void OnApplicationQuit()
        {
            isApplicationQuitting = true;
        }

        [Button("GoBackToPool")]
        public void Deactivator()
        {
            gameObject.SetActive(false);
        }

        public void GoBackToPool()
        {
            _poolingService.GoBackToPool(this);
        }

        /*private void OnTransformParentChanged()
        {
            if(transform.parent != _poolingService.poolParent)
            {
                _poolingService.PoolElementParentChanged(this);
            }
        }*/
    }
}