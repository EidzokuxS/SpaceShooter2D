using UnityEngine;

namespace SpaceShooter
{
    public class AsteroidShattering : Destructible
    {
        #region Properties

        public enum Size
        {
            Big,
            Medium,
            Small
        }

        [SerializeField] private Size size;
        [SerializeField] private float _randomSpeed;

        #endregion

        #region Unity Events

        private void Awake()
        {
            _eventOnDeath.AddListener(OnAsteroidDestroyed);
            SetSize(size);
        }

        private void OnDestroy()
        {
            _eventOnDeath.RemoveListener(OnAsteroidDestroyed);
        }
        #endregion

        #region Private API

        private void OnAsteroidDestroyed()
        {
            if (size == Size.Small)
            {
                Destroy(gameObject);
                return;
            }

            SpawnStones();
            Destroy(gameObject);
        }

        private void SpawnStones()
        {
            for (int i = 0; i < 2; i++)
            {
                AsteroidShattering asteroid = Instantiate(this, transform.position, Quaternion.identity);
                asteroid.SetSize(size - 1);
                asteroid.GetComponent<AsteroidShattering>().EventOnDeath.AddListener(OnAsteroidDestroyed);

                asteroid._hitPoints = Mathf.Clamp(_hitPoints / 2, 1, _hitPoints);

                Rigidbody2D rigidbody = asteroid.GetComponent<Rigidbody2D>();

                if (rigidbody != null && _randomSpeed > 0)
                {
                    rigidbody.velocity = (Vector2)Random.insideUnitSphere * _randomSpeed;
                }
            }
        }

        private void SetSize(Size size)
        {
            if (size < 0) return;

            transform.localScale = GetVectorFromSize(size);
            this.size = size;
        }

        private Vector3 GetVectorFromSize(Size size)
        {
            if (size == Size.Big) return new Vector3(1, 1, 1);
            if (size == Size.Medium) return new Vector3(0.75f, 0.75f, 0.75f);
            if (size == Size.Small) return new Vector3(0.6f, 0.6f, 0.6f);

            return Vector3.one;
        }
        #endregion
    }
}


