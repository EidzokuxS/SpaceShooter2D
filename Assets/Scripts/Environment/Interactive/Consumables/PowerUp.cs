using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(CircleCollider2D))]
    public abstract class PowerUp : MonoBehaviour
    {
        #region Properties

        #endregion

        #region Unity Events

        private void OnTriggerEnter2D(Collider2D collision)
        {
            SpaceShip ship = collision.transform.parent.GetComponent<SpaceShip>();

            if (ship != null && Player.Instance.ActiveShip)
            {
                OnPickedUp(ship);
                Destroy(gameObject);
            }
        }

        #endregion

        #region Private API

        protected abstract void OnPickedUp(SpaceShip ship);

        #endregion
    }
}
