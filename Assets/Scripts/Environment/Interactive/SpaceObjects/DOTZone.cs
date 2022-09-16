using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class DOTZone : MonoBehaviour
    {
        #region Properties

        [SerializeField] private int _damage;
        [SerializeField] private float _damageRate;

        private Destructible destructible;
        private float timer;

        #endregion

        #region UnityEvents

        private void Update()
        {
            if (destructible == null) return;

            timer += Time.deltaTime;

            if (timer >= _damageRate)
            {
                if (destructible != null)
                {
                    destructible.ApplyDamage(_damage);
                }

                timer = 0;
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            destructible = collision.GetComponentInParent<Destructible>();
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.GetComponentInParent<Destructible>() == destructible)
                destructible = null;
        }
        #endregion
    }
}

