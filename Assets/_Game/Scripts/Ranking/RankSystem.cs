using Cysharp.Threading.Tasks;
using DG.Tweening;
using GameTemplate.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace GameTemplate._Game
{
    public class RankSystem : MonoBehaviour, IStartable
    {
        public Image rankFill;
        public TextMeshProUGUI rankText;
        public int CurrentRank = 0;
        public int CurrentExperience = 0;
        public GameObject particle;

        RankData _rankData;

        [Inject]
        public void Construct(RankData rankData)
        {
            _rankData = rankData;
            CurrentExperience = UserPrefs.GetExperience();
            CurrentRank = UserPrefs.GetRank();
            rankText.text = (CurrentRank + 1).ToString();
            rankFill.fillAmount = (float)CurrentExperience / _rankData.rankLimits[CurrentRank];
        }

        public void Start()
        {
        }

        [ContextMenu("Add 150 point")]
        public void Test()
        {
            AddPoints(150);
        }

        public async UniTask AddPoints(int points)
        {
            CurrentExperience += points;

            if (CurrentExperience > _rankData.rankLimits[CurrentRank])
            {
                //rank up
                CurrentRank++;
                UserPrefs.SetRank(CurrentRank);

                rankFill.DOFillAmount(1, 1f);
                await UniTask.Delay(1100);
                rankText.text = (CurrentRank + 1).ToString();
                rankText.transform.DOPunchScale(.2f * Vector3.one, .2f);
                particle.SetActive(true);

                rankFill.fillAmount = 0;
                CurrentExperience -= _rankData.rankLimits[CurrentRank - 1];
                UserPrefs.SetExperience(CurrentExperience);

                await UniTask.Delay(200);
                rankFill.DOFillAmount((float)CurrentExperience / _rankData.rankLimits[CurrentRank], 1f);
            }
            else
            {
                rankFill.DOFillAmount((float)CurrentExperience / _rankData.rankLimits[CurrentRank], 1f);

                await UniTask.Delay(1000);

                Debug.Log(CurrentExperience == _rankData.rankLimits[CurrentRank]);
                if (CurrentExperience == _rankData.rankLimits[CurrentRank])
                {
                    rankFill.fillAmount = 0;
                }
            }
        }
    }
}