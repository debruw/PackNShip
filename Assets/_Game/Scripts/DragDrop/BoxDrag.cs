using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameTemplate._Game.Scripts
{
    public class BoxDrag : DragableUI
    {
        Box _box;

        private void Start()
        {
            _box = GetComponent<Box>();
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
            

            if (RaycastHandler.RaycastConveyor())
            {
                //TODO add count
                
                transform.DOMoveY(transform.position.y - 1080, 3f).OnComplete(() =>
                {
                    Destroy(gameObject);
                });
                this.enabled = false;
            }
        }
    }
}