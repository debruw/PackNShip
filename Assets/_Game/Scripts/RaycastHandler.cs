using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameTemplate._Game.Scripts
{
    public class RaycastHandler : MonoBehaviour
    {
        static GraphicRaycaster _Raycaster;
        static PointerEventData _PointerEventData;
        static EventSystem _EventSystem;
        private void Start()
        {
            _Raycaster = GetComponent<GraphicRaycaster>();
            _EventSystem = GetComponent<EventSystem>();
        }

        public static Box RaycastBox()
        {
            _PointerEventData = new PointerEventData(_EventSystem);
            _PointerEventData.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();

            _Raycaster.Raycast(_PointerEventData, results);

            foreach (RaycastResult result in results)
            {
                if (result.gameObject.TryGetComponent(out Box box))
                {
                    return box;
                }
            }

            return null;
        }
        
        public static bool RaycastConveyor()
        {
            _PointerEventData = new PointerEventData(_EventSystem);
            _PointerEventData.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();

            _Raycaster.Raycast(_PointerEventData, results);

            foreach (RaycastResult result in results)
            {
                if (result.gameObject.TryGetComponent(out Conveyor conveyor))
                {
                    return true;
                }
            }

            return false;
        }
    }
}