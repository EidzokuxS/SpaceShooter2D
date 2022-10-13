using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceShooter
{
    public class UIPauseMenuPanel : MonoBehaviour
    {

        #region Unity Events
        private void Start()
        {
            gameObject.SetActive(false);
        }
        #endregion

        #region Public API

        public void OnPauseButtonPressed()
        {
            Time.timeScale = 0.0f;
            gameObject.SetActive(true);
        }

        public void OnContinueButtonPressed()
        {
            Time.timeScale = 1.0f;
            gameObject.SetActive(false);
        }

        public void OnMenuButtonPressed()
        {
            Time.timeScale = 1.0f;
            gameObject.SetActive(false);

            SceneManager.LoadScene(LevelSequenceController.MainMenuSceneName);
        }

        #endregion
    }
}

