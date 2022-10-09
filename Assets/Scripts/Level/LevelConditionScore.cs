using UnityEngine;

namespace SpaceShooter
{
    public class LevelConditionScore : MonoBehaviour, ILevelCondition
    {
        #region Properties

        [SerializeField] private int _score;

        private bool _IsScoreReached;

        bool ILevelCondition.IsCompleted
        {
            get
            {
                if (Player.Instance != null && Player.Instance.ActiveShip != null)
                {
                    if (Player.Instance.Score >= _score)
                    {
                        _IsScoreReached = true;
                    }
                }
                return _IsScoreReached;
            }
        }

        #endregion

        #region Unity Events

        #endregion

        #region Private API

        #endregion
    }

}
