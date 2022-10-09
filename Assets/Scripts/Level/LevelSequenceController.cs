using UnityEngine.SceneManagement;

namespace SpaceShooter
{
    public class LevelSequenceController : SingletonBase<LevelSequenceController>
    {
        #region Properties

        public static string MainMenuSceneName = "main_menu";

        public Episodes CurrentEpisode { get; private set; }

        public int CurrentLevel { get; private set; }

        #endregion

        #region Public API
        public void StartEpisode(Episodes e)
        {
            CurrentEpisode = e;
            CurrentLevel = 0;

            //reset stats before episode start

            SceneManager.LoadScene(e.Levels[CurrentLevel]);

        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
        }

        public void NextLevel()
        {
            CurrentLevel++;

            if (CurrentLevel >= CurrentEpisode.Levels.Length)
            {
                SceneManager.LoadScene(MainMenuSceneName);
            }
            else
            {
                SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
            }
        }

        public void FinishCurrentLevel(bool success)
        {

        }

        #endregion
    }
}

