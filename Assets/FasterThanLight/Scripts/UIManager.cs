using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FTL;
using UnityEngine.UI;
using TMPro;

namespace FTL.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private Slider progressSlider;
        [SerializeField]
        private TextMeshProUGUI points;
        [SerializeField]
        private TextMeshProUGUI currentWavePoints;
        [SerializeField]
        private TextMeshProUGUI endgameScore;
        [SerializeField]
        private GameObject gameOverScreen;
        [SerializeField]
        private GameObject startGameScreen;
        [SerializeField]
        private GameObject buttonContainer;

        private void UpdateScoreUI(int newScore)
        {
            points.text = (int.Parse(points.text) + newScore).ToString();
            progressSlider.value = progressSlider.value + newScore;
        }

        private void OnWaveChangedUI(float progressBar, int lostPoints)
        {
            progressSlider.value = lostPoints;
            progressSlider.maxValue = progressBar;
            OnProgressBarValueChanged();
        }

        private void Start()
        {
            GameManager.Instance.OnScoreUpdated += UpdateScoreUI;
            GameManager.Instance.OnWaveChanged += OnWaveChangedUI;
            GameManager.Instance.OnGameOver += OnGameOverUI;
        }

        private void OnDestroy()
        {
            GameManager.Instance.OnScoreUpdated -= UpdateScoreUI;
            GameManager.Instance.OnWaveChanged -= OnWaveChangedUI;
            GameManager.Instance.OnGameOver -= OnGameOverUI;
        }

        public void OnProgressBarValueChanged()
        {
            currentWavePoints.text = progressSlider.value.ToString() + "/" + progressSlider.maxValue.ToString();
        }

        private void OnGameOverUI()
        {
            gameOverScreen.SetActive(true);
            buttonContainer.SetActive(false);
            endgameScore.text = "Score: " + points.text;
        }

        public void RestartGame()
        {
            progressSlider.value = 0;
            points.text = "0";

            buttonContainer.SetActive(true);
            gameOverScreen.SetActive(false);

            GameManager.Instance.RestartGame();
        }

        public void StartGame()
        {
            startGameScreen.SetActive(false);
            GameManager.Instance.RestartGame();
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}
