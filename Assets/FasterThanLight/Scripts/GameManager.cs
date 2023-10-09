using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static FTL.WaveScriptable;

namespace FTL
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [SerializeField]
        private List<WaveScriptable> waves = new List<WaveScriptable>();
        [SerializeField]
        private List<Zone> zones = new List<Zone>();

        public event Action<int> OnScoreUpdated;
        public event Action<float, int> OnWaveChanged;
        public event Action OnGameOver;

        private WaveScriptable currentWaveScriptable;
        private int currentWave = 0;
        private int currentProgressBar = 0;

        private float refreshRate = 1f;
        private float lastToggleTime = 0f;

        private bool stopGame = true;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            if (stopGame)
                return;

            if (Time.time - lastToggleTime >= refreshRate)
            {
                SpawnObjective();
                lastToggleTime = Time.time;
            }
        }

        public void ScorePoint(ObjectiveType type)
        {
            int score = GetScorePoints(type);
            currentProgressBar += score;

            OnScoreUpdated?.Invoke(score);

            if (IsItNextWave())
                ProgressNextWave();
        }

        private bool IsItNextWave()
        {
            if (IsItLastWave())
                return false;

            if (currentProgressBar >= waves[currentWave].pointsToAdvance)
                return true;

            return false;
        }

        private void ProgressNextWave()
        {
            int lostPoints = currentProgressBar - currentWaveScriptable.pointsToAdvance;
            currentProgressBar = 0;
            currentWave++;
            currentWaveScriptable = waves[currentWave];

            refreshRate = currentWaveScriptable.refreshRateOfCircle;

            OnWaveChanged?.Invoke(waves[currentWave].pointsToAdvance, lostPoints);
        }

        private bool IsItLastWave()
        {
            return waves.Count == (currentWave + 1);
        }

        private void SpawnObjective()
        {
            List<Zone> freeZones = new List<Zone>();

            for (int i = 0; i < zones.Count; i++)
            {
                if (zones[i].IsFreeToEnable())
                {
                    freeZones.Add(zones[i]);
                }
            }

            if (freeZones.Count == 0)
            {
                GameOver();
                return;
            }

            int randomIndex = UnityEngine.Random.Range(0, freeZones.Count);

            int randomObjective = UnityEngine.Random.Range(0, currentWaveScriptable.enemiesInWave.Length);
            ObjectiveType type = currentWaveScriptable.enemiesInWave[randomObjective];

            freeZones[randomIndex].RevealObjective(type);
        }

        private int GetScorePoints(ObjectiveType type)
        {
            switch (type)
            {
                case ObjectiveType.SmallObjective:
                    return 1;
                case ObjectiveType.MediumObjective:
                    return 2;
                case ObjectiveType.LargeObjective:
                    return 3;
                default:
                    return 0;
            }
        }

        private void GameOver()
        {
            stopGame = true;

            foreach (Zone zone in zones)
            {
                if (!zone.IsFreeToEnable())
                    zone.VisualEffectPlay("");
            }

            OnGameOver?.Invoke();
        }

        public void RestartGame()
        {
            currentProgressBar = 0;
            lastToggleTime = 0;

            foreach (Zone zone in zones)
            {
                zone.SetActiveButtons(false);
            }

            if (waves.Count > 0)
            {
                currentWave = 0;
                currentWaveScriptable = waves[currentWave];
                refreshRate = currentWaveScriptable.refreshRateOfCircle;

                OnWaveChanged?.Invoke(currentWaveScriptable.pointsToAdvance, 0);
            }
            stopGame = false;
        }
    }
}
