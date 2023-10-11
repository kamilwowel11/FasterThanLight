using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static FTL.WaveScriptable;

namespace FTL
{
    public class Zone : MonoBehaviour
    {
        private List<ClickHit> clickHitList = new List<ClickHit>();

        [SerializeField]
        private GameObject clickHitPrefab;

        [SerializeField]
        private Transform spawnClickHitTransform;

        [SerializeField]
        private ParticleSystem particles;

        private Color currentColor = Color.white;

        public void InitializeObjectives(List<Objective> objectives)
        {
            clickHitList = new List<ClickHit>();

            for (int i = 0; i < objectives.Count; i++)
            {
                ClickHit clickHit = Instantiate(clickHitPrefab, spawnClickHitTransform).GetComponent<ClickHit>();

                clickHit.OnInitialize(this, objectives[i].scale, objectives[i].points);

                clickHitList.Add(clickHit);
            }
        }

        public void DeinitializeObjectives()
        {
            if (clickHitList != null || clickHitList.Count == 0)
                return;

            foreach (var item in clickHitList)
            {
                Destroy(item);
            }
            clickHitList.Clear();
        }

        public bool IsFreeToEnable()
        {
            return !clickHitList.Any((x) => x.IsEnabled());
        }

        [System.Obsolete]
        public void OnClick(float scale, float points)
        {
            VisualEffectPlay();
            GameManager.Instance.ScorePoint((int)points);
        }

        [System.Obsolete]
        public void VisualEffectPlay()
        {
            particles.startColor = currentColor;
            particles.Play();
        }

        public void RevealObjective(int indexObjective)
        {
            currentColor = GetRandomColor();

            clickHitList[indexObjective].Reveal(currentColor);
        }

        public void HideButtons()
        {
            foreach (var clickHit in clickHitList)
            {
                clickHit.Hide();
            }
        }

        private Color GetRandomColor()
        {
            float r = Random.Range(0f, 1f);
            float g = Random.Range(0f, 1f);
            float b = Random.Range(0f, 1f);

            return new Color(r, g, b);
        }
    }
}
