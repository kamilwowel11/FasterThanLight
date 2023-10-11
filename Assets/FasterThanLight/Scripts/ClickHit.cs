using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FTL
{
    public class ClickHit : MonoBehaviour
    {
        [HideInInspector]
        public Zone parentZone;

        private float objectiveScale;
        private float objectivePoint;

        public Button clickButton;

        private void Start()
        {
            clickButton.onClick.AddListener(OnClick);
        }

        public void OnInitialize(Zone zone, float scale, float points)
        {
            parentZone = zone;
            objectivePoint = points;
            objectiveScale = scale;

            clickButton.transform.localScale = new Vector3(objectiveScale, objectiveScale, objectiveScale);
        }

        [System.Obsolete]
        private void OnClick()
        {
            parentZone.OnClick(objectiveScale, objectivePoint);
            Hide();
        }

        public void Reveal(Color color)
        {
            clickButton.image.color = color;
            clickButton.gameObject.SetActive(true);
        }

        public void Hide()
        {
            clickButton.gameObject.SetActive(false);
        }

        public bool IsEnabled()
        {
            return clickButton.isActiveAndEnabled;
        }

        private void OnDestroy()
        {
            clickButton.onClick.RemoveListener(OnClick);
        }
    }
}
