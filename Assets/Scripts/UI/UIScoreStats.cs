using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class UIScoreStats : MonoBehaviour
    {
        #region Properties

        [SerializeField] private Text _text;

        private int _lastScore;
        #endregion

        #region Unity Events

        private void Update()
        {
            UpdateScore();
        }

        #endregion

        #region Private API


        private void UpdateScore()
        {
            if (Player.Instance != null)
            {
                int currentScore = Player.Instance.Score;

                if (_lastScore != currentScore)
                {
                    _lastScore = currentScore;
                    _text.text = "Score: " + _lastScore.ToString();
                }
            }
        }

        #endregion
    }
}

