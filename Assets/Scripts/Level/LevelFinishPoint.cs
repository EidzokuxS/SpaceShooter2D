using UnityEngine;

namespace SpaceShooter
{
    public class LevelFinishPoint : MonoBehaviour
    {
        #region Properties

        public bool IsTriggered { get; private set; }

        #endregion

        #region Unity Events

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision == Player.Instance.ActiveShip.GetComponentInChildren<Collider2D>())
                IsTriggered = true;
        }

        #endregion
    }
}

