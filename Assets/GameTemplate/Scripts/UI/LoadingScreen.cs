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

        bool m_LoadingScreenRunning;

        Coroutine m_FadeOutCoroutine;

        ISceneService _sceneService;

        [Inject]
        public void Construct(ISceneService sceneService)
        {
            _sceneService = sceneService;
            _sceneService.OnBeforeSceneLoad += OpenLoadingScreen;
            _sceneService.OnSceneLoaded += CloseLoadingScreen;
        }

        public void OpenLoadingScreen()
        {
            SetCanvasVisibility(true);
            m_LoadingScreenRunning = true;
            if (m_LoadingScreenRunning)
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
            if (m_LoadingScreenRunning)
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
            m_LoadingScreenRunning = false;

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