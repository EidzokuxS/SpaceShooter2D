using UnityEngine;

namespace SpaceShooter
{
    public class LevelConditionScore : MonoBehaviour, ILevelCondition
    {
        #region Properties

        [SerializeField] private int _score;

        private bool _isScoreReached;

        bool ILevelCondition.IsCompleted
        {
            get
            {
                if (Player.Instance != null && Player.Instance.ActiveShip != null)
                {
                    if (Player.Instance.Score >= _score)
                    {
                        _isScoreReached = true;
                    }
                }
                return _isScoreReached;
            }
        }

        #endregion
    }

}
