using UnityEngine;

namespace GameTemplate._Game
{
    [CreateAssetMenu(fileName = "RankData", menuName = "Scriptable Objects/Rank Data", order = 0)]
    public class RankData : ScriptableObject
    {
        public int[] rankLimits;
    }
}