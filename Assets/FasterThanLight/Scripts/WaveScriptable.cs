using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTL
{
    [CreateAssetMenu(fileName = "Wave1", menuName = "Wave Data/Wave")]
    public class WaveScriptable : ScriptableObject
    {
        public List<Objective> objectives = new List<Objective>();

        [System.Serializable]
        public class Objective
        {
            public Objective(float po, float sc)
            {
                points = po;
                scale = sc;
            }

            public float scale;
            public float points;
        }

        public int pointsToAdvance;

        public float refreshRateOfCircle;
    }
}