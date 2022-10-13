using UnityEngine;

namespace SpaceShooter
{
    public class LevelConditionPosition : MonoBehaviour, ILevelCondition
    {
        #region Properties

        [SerializeField] private LevelFinishPoint _point;

        private bool _isPointReached;

        bool ILevelCondition.IsCompleted
        {
            get
            {
                if (Player.Instance != null && Player.Instance.ActiveShip != null)
                {
                    if (_point.IsTriggered == true)
                        _isPointReached = true;
                }
                return _isPointReached;
            }
        }

        #endregion
    }

}
