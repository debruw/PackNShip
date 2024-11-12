using System;
using UnityEngine;

namespace GameTemplate.Systems.Level
{
    public class LevelPrefab : MonoBehaviour, IDisposable
    {
        public static event Action<bool> OnGameFinished;

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}