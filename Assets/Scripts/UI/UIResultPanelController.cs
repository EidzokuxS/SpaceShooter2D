using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceShooter
{
    public class UIResultPanelController : SingletonBase<UIResultPanelController>
    {
        #region Properties

        [SerializeField] private TMP_Text _killCountText;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _timeText;

        [SerializeField] private TMP_Text _resultText;

        [SerializeField] private TMP_Text _buttonText;

        private bool _isSuccess;

        #endregion

        #region Unity Events
        private void Start()
        {
            gameObject.SetActive(false);
        }
        #endregion

        #region Public API

        public void ShowResults(PlayerStatistics levelResults, bool success)
        {
            gameObject.SetActive(true);

            _isSuccess = success;

            _resultText.text = success ? "Win" : "Lose";
            _buttonText.text = success ? "Next" : "Restart";

            if (levelResults.Time >= 300)
            {
                _killCountText.text = "Kills: " + levelResults.KillCount.ToString();
                _scoreText.text = "Score: " + levelResults.Score.ToString();
                _timeText.text = "Time: " + levelResults.Time.ToString();
            }
            if (levelResults.Time >= 150 && levelResults.Time < 300)
            {
                _killCountText.text = "Kills: " + levelResults.KillCount.ToString();
                _scoreText.text = "Score: " + levelResults.Score.ToString();
                _timeText.text = "Time: " + levelResults.Time.ToString() + " Score multiplier X2";
            }
            if (levelResults.Time < 150)
            {
                _killCountText.text = "Kills: " + levelResults.KillCount.ToString();
                _scoreText.text = "Score: " + levelResults.Score.ToString();
                _timeText.text = "Time: " + levelResults.Time.ToString() + " Score multiplier X3";
            }

            Time.timeScale = 0f;


        }
        public void OnMenuButtonPressed()
        {
            Time.timeScale = 1f;

            LevelSequenceController.Instance.LevelStatistics.Reset();

            SceneManager.LoadScene(LevelSequenceController.MainMenuSceneName);
        }

        public void OnNextButtonPressed()
        {
            gameObject.SetActive(false);

            Time.timeScale = 1f;

            if (_isSuccess)
            {
                LevelSequenceController.Instance.NextLevel();
            }
            else
            {
                Time.timeScale = 1f;
                LevelSequenceController.Instance.RestartLevel();
            }
        }
        #endregion
    }

}
