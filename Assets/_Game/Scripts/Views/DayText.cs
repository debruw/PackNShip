using System;
using GameTemplate.Systems.Level;
using TMPro;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameTemplate._Game.Scripts.Views
{
    public class DayText : MonoBehaviour
    {
        private LevelService _levelService;

        [Inject]
        public void Construct(LevelService LevelService)
        {
            Debug.Log("Constructing day text");
            _levelService = LevelService;
            GetComponent<TextMeshProUGUI>().text = _levelService.UILevelId.ToString();
        }
    }
}