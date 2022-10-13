using TMPro;
using UnityEngine;

namespace SpaceShooter
{
    public class UITotalScore : MonoBehaviour
    {
        #region Properties

        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _killCountText;
        [SerializeField] private TMP_Text _timeText;

        #endregion

        #region Public API

        public void ShowTotalScore()
        {
            if (LevelSequenceController.Instance.LevelStatistics == null)
                return;

            _killCountText.text = "Kills: " + LevelSequenceController.Instance.LevelStatistics.TotalKillCount;
            _scoreText.text = "Score: " + LevelSequenceController.Instance.LevelStatistics.TotalScore;
            _timeText.text = "Time: " + LevelSequenceController.Instance.LevelStatistics.TotalTime;
        }

        #endregion
    }
}

