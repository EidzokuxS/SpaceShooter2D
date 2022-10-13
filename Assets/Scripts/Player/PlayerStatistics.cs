namespace SpaceShooter
{
    public class PlayerStatistics
    {
        #region Properties

        public int KillCount { get; set; }
        public int Score { get; set; }
        public int Time { get; set; }

        private int _totalKillCount;
        public int TotalKillCount => _totalKillCount;

        private int _totalScore;
        public int TotalScore => _totalScore;

        private int _totalTime;
        public int TotalTime => _totalTime;


        #endregion

        #region Public API

        public void Reset()
        {
            _totalKillCount += KillCount;
            _totalScore += Score;
            _totalTime += Time;

            KillCount = 0;
            Score = 0;
            Time = 0;
        }

        #endregion
    }
}


