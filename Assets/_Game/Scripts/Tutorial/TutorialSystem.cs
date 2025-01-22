using System;
using System.Collections.Generic;
using GameTemplate._Game.Scripts;
using GameTemplate._Game.Scripts.Inventory;
using UnityEngine;

namespace _Game.Scripts.Tutorial
{
    public class TutorialSystem : MonoBehaviour
    {
        public GameObject missionUiPrefab;
        public List<string> MissionTexts = new List<string>();
        public List<TutorialMissionUI> tutorialMissionUIs = new List<TutorialMissionUI>();
        
        private int currentMissionIndex = 0;

        private void Awake()
        {
            ScannerDrag.OnScan += OnScan;
            SimpleBoxSpawner.OnBoxSpawn += OnBoxSpawn;
            InventoryController.OnItemDrop += OnItemDrop;
            InventoryController.OnFillingDrop += OnFillingDrop;
            Box.OnBoxClosed += OnBoxClosed;
            Box.OnTapePut += OnTapePut;
            Box.OnLabelPut += OnLabelPut;
            Box.OnBoxDelivered += OnBoxDelivered;

            SpawnMissions();
        }

        private void SpawnMissions()
        {
            for (int i = 0; i < MissionTexts.Count; i++)
            {
                TutorialMissionUI newMission =
                    Instantiate(missionUiPrefab, transform).GetComponent<TutorialMissionUI>();
                tutorialMissionUIs.Add(newMission);
                newMission.InitMission((i + 1) + ") " + MissionTexts[i]);
            }
        }

        private void OnDestroy()
        {
            ScannerDrag.OnScan += OnScan;
            SimpleBoxSpawner.OnBoxSpawn += OnBoxSpawn;
            InventoryController.OnItemDrop += OnItemDrop;
            InventoryController.OnFillingDrop += OnFillingDrop;
            Box.OnBoxClosed += OnBoxClosed;
            Box.OnTapePut += OnTapePut;
            Box.OnLabelPut += OnLabelPut;
            Box.OnBoxDelivered += OnBoxDelivered;
        }

        private void OnScan()
        {
            if (!tutorialMissionUIs[currentMissionIndex].isCompleted)
            {
                tutorialMissionUIs[currentMissionIndex].Complete();
                            currentMissionIndex++;
            }
        }

        private void OnBoxSpawn()
        {
            if (!tutorialMissionUIs[currentMissionIndex].isCompleted)
            {
                tutorialMissionUIs[currentMissionIndex].Complete();
                currentMissionIndex++;
            }
        }
        
        private void OnItemDrop()
        {
            if (!tutorialMissionUIs[currentMissionIndex].isCompleted)
            {
                tutorialMissionUIs[currentMissionIndex].Complete();
                currentMissionIndex++;
            }
        }

        private void OnFillingDrop()
        {
            if (!tutorialMissionUIs[currentMissionIndex].isCompleted)
            {
                tutorialMissionUIs[currentMissionIndex].Complete();
                currentMissionIndex++;
            }
        }

        private void OnBoxClosed()
        {
            if (!tutorialMissionUIs[currentMissionIndex].isCompleted)
            {
                tutorialMissionUIs[currentMissionIndex].Complete();
                currentMissionIndex++;
            }
        }

        private void OnTapePut()
        {
            if (!tutorialMissionUIs[currentMissionIndex].isCompleted)
            {
                tutorialMissionUIs[currentMissionIndex].Complete();
                currentMissionIndex++;
            }
        }

        private void OnLabelPut()
        {
            if (!tutorialMissionUIs[currentMissionIndex].isCompleted)
            {
                tutorialMissionUIs[currentMissionIndex].Complete();
                currentMissionIndex++;
            }
        }

        private void OnBoxDelivered(BoxStatistic boxStatistics, Transform boxTransform)
        {
            if (!tutorialMissionUIs[currentMissionIndex].isCompleted)
            {
                tutorialMissionUIs[currentMissionIndex].Complete();
                currentMissionIndex++;
            }
        }
    }
}