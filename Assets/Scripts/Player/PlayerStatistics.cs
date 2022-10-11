using UnityEngine;

namespace SpaceShooter
{
    public class PlayerStatistics : MonoBehaviour
    {
        #region Properties

        public int KillCount { get; set; }
        public int Score { get; set; }
        public int Time { get; set; }


        #endregion

        #region Public API

        public void Reset()
        {
            KillCount = 0;
            Score = 0;
            Time = 0;
        }

        #endregion
    }
}


