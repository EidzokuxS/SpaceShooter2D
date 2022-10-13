using UnityEngine.SceneManagement;

namespace SpaceShooter
{
    public class LevelSequenceController : SingletonBase<LevelSequenceController>
    {
        #region Properties

        public static string MainMenuSceneName = "main_menu";

        public static SpaceShip PlayerShip { get; set; }

        public Episodes CurrentEpisode { get; private set; }

        public int CurrentLevel { get; private set; }

        public bool LastLevelResult { get; private set; }

        public PlayerStatistics LevelStatistics { get; private set; }

        #endregion

        #region Public API
        public void StartEpisode(Episodes e)
        {
            CurrentEpisode = e;
            CurrentLevel = 0;

            //reset stats before episode start
            LevelStatistics ??= new PlayerStatistics();

            LevelStatistics.Reset();

            SceneManager.LoadScene(e.Levels[CurrentLevel]);

        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
        }

        public void NextLevel()
        {
            LevelStatistics.Reset();

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
            LastLevelResult = success;
            CalculateLevelStatistic();

            UIResultPanelController.Instance.ShowResults(LevelStatistics, success);

        }

        private void CalculateLevelStatistic()
        {
            if (LevelController.Instance.LevelTime >= 300)
            {
                LevelStatistics.Score = Player.Instance.Score;
                LevelStatistics.KillCount = Player.Instance.KillCount;
                LevelStatistics.Time = (int)LevelController.Instance.LevelTime;
            }

            if (LevelController.Instance.LevelTime >= 150 && LevelController.Instance.LevelTime < 300)
            {
                LevelStatistics.Score = Player.Instance.Score * Player.Instance.ScoreModifierFast;
                LevelStatistics.KillCount = Player.Instance.KillCount;
                LevelStatistics.Time = (int)LevelController.Instance.LevelTime;
            }

            if (LevelController.Instance.LevelTime < 150)
            {
                LevelStatistics.Score = Player.Instance.Score * Player.Instance.ScoreModifierExtraFast;
                LevelStatistics.KillCount = Player.Instance.KillCount;
                LevelStatistics.Time = (int)LevelController.Instance.LevelTime;
            }

            #endregion
        }
    }
}

