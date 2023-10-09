using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTL
{
    [CreateAssetMenu(fileName = "Wave1", menuName = "Wave Data/Wave")]
    public class WaveScriptable : ScriptableObject
    {
        public enum ObjectiveType
        {
            SmallObjective,
            MediumObjective,
            LargeObjective,
            Invalid
        }

        public ObjectiveType[] enemiesInWave;

        public int pointsToAdvance;

        public float refreshRateOfCircle;
    }
}