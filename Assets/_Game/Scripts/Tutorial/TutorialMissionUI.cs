using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace _Game.Scripts.Tutorial
{
    public class TutorialMissionUI : MonoBehaviour
    {
        public TextMeshProUGUI missionText;
        public GameObject checkImage;
        public bool isCompleted = false;

        public void InitMission(string text)
        {
            missionText.text = text;
        }

        public void Complete()
        {
            checkImage.SetActive(true);
            isCompleted = true;
        }
    }
}