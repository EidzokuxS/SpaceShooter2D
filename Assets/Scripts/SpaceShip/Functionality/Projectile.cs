using UnityEngine;

namespace SpaceShooter
{
    public class Projectile : Entity
    {
        #region Properties

        [SerializeField] SelfDestruction _explosionPrefab;
        [SerializeField] private float speed;
        [SerializeField] private float lifeTime;

        [SerializeField] private int damage;
        public int Damage { get; set; }

        private Destructible _parent;


        #endregion

        #region UnityEvents

        private void Start()
        {
            _parent = transform.GetComponentInParent<Destructible>();

            Destroy(gameObject, lifeTime);
        }

        private void Update()
        {
            transform.position += transform.up * speed * Time.deltaTime;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, speed * Time.deltaTime);

            if (hit)
            {
                Destructible destructible = hit.collider.transform.parent.GetComponent<Destructible>();

                if (destructible != null && destructible != _parent)
                {
                    destructible.ApplyDamage(damage);
                }

                OnProjectileDestruction(hit.collider, hit.point);
            }
        }

        #endregion

        #region Private API

        private void OnProjectileDestruction(Collider2D collider, Vector2 position)
        {
            Destroy(gameObject);
        }

        #endregion
    }
}

