using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static FTL.WaveScriptable;

namespace FTL
{
    public class Zone : MonoBehaviour
    {
        [SerializeField]
        private Button smallObjective;
        [SerializeField]
        private Button mediumObjective;
        [SerializeField]
        private Button largeObjective;
        [SerializeField]
        private ParticleSystem particles;

        private Color currentColor = Color.white;

        public bool IsFreeToEnable()
        {
            return !smallObjective.gameObject.activeSelf && !mediumObjective.gameObject.activeSelf && !largeObjective.gameObject.activeSelf;
        }

        [System.Obsolete]
        public void OnClick(string type)
        {
            VisualEffectPlay(type);
            ScorePoints(type);
        }

        [System.Obsolete]
        public void VisualEffectPlay(string type)
        {
            if (!string.IsNullOrEmpty(type))
                GetObjective(type).gameObject.SetActive(false);
            else
                SetActiveButtons(false);

            particles.startColor = currentColor;

            particles.Play();
        }

        public void ScorePoints(string type)
        {
            GameManager.Instance.ScorePoint(GetType(type));
        }


        public void RevealObjective(ObjectiveType type)
        {
            Button button = GetObjective(type);
            currentColor = GetRandomColor();

            button.image.color = currentColor;
            button.gameObject.SetActive(true);
        }

        public void SetActiveButtons(bool state)
        {
            smallObjective.gameObject.SetActive(state);
            mediumObjective.gameObject.SetActive(state);
            largeObjective.gameObject.SetActive(state);
        }

        private Button GetObjective(ObjectiveType type)
        {
            switch (type)
            {
                case ObjectiveType.SmallObjective:
                    return smallObjective;
                case ObjectiveType.MediumObjective:
                    return mediumObjective;
                case ObjectiveType.LargeObjective:
                    return largeObjective;
                default:
                    return null;
            }
        }

        private Button GetObjective(string type)
        {
            switch (type)
            {
                case "small":
                    return smallObjective;
                case "medium":
                    return mediumObjective;
                case "large":
                    return largeObjective;
                default:
                    return null;
            }
        }

        private ObjectiveType GetType(string type)
        {
            switch (type.ToLower())
            {
                case "small":
                    return ObjectiveType.SmallObjective;
                case "medium":
                    return ObjectiveType.MediumObjective;
                case "large":
                    return ObjectiveType.LargeObjective;
                default:
                    return ObjectiveType.Invalid;
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
