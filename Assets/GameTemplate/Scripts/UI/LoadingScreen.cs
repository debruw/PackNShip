using System;
using System.Collections;
using GameTemplate.Systems.Scene;
using UnityEngine;
using VContainer;

namespace GameTemplate.UI
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] CanvasGroup m_CanvasGroup;

        [SerializeField] float m_DelayBeforeFadeOut = 0.5f;

        [SerializeField] float m_FadeOutDuration = 0.1f;
        
        public GameObject CameraObject;

        bool m_LoadingScreenActive = true;

        Coroutine m_FadeOutCoroutine;

        ISceneService _sceneService;

        [Inject]
        public void Construct(ISceneService sceneService)
        {
            Debug.Log("Construct LoadingScreen");
            _sceneService = sceneService;
            _sceneService.OnBeforeSceneLoad += OpenLoadingScreen;
            _sceneService.OnSceneLoaded += CloseLoadingScreen;
        }

        private void OnDestroy()
        {
            _sceneService.OnBeforeSceneLoad -= OpenLoadingScreen;
            _sceneService.OnSceneLoaded -= CloseLoadingScreen;
        }

        public void OpenLoadingScreen()
        {
            SetCanvasVisibility(true);
            m_LoadingScreenActive = true;
            CameraObject.SetActive(true);
            Debug.LogError("open canvas");
            if (m_LoadingScreenActive)
            {
                if (m_FadeOutCoroutine != null)
                {
                    //Debug.Log("start loading screen");
                    StopCoroutine(m_FadeOutCoroutine);
                }
            }
        }

        public void CloseLoadingScreen()
        {
            Debug.LogError("close canvas");
            CameraObject.SetActive(false);
            if (m_LoadingScreenActive)
            {
                if (m_FadeOutCoroutine != null)
                {
                    //Debug.Log("stop loading screen");
                    StopCoroutine(m_FadeOutCoroutine);
                }

                m_FadeOutCoroutine = StartCoroutine(FadeOutCoroutine());
            }
        }

        void SetCanvasVisibility(bool visible)
        {
            m_CanvasGroup.alpha = visible ? 1 : 0;
            m_CanvasGroup.blocksRaycasts = visible;
        }

        IEnumerator FadeOutCoroutine()
        {
            yield return new WaitForSeconds(m_DelayBeforeFadeOut);
            m_LoadingScreenActive = false;

            float currentTime = 0;
            while (currentTime < m_FadeOutDuration)
            {
                m_CanvasGroup.alpha = Mathf.Lerp(1, 0, currentTime / m_FadeOutDuration);
                yield return null;
                currentTime += Time.deltaTime;
            }

            SetCanvasVisibility(false);
        }
    }
}