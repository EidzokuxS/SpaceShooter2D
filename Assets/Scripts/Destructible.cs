using UnityEngine;
using UnityEngine.Events;


namespace SpaceShooter
{
    /// <summary>
    /// Destructible object. Object that can have hitpoints.
    /// </summary>
    public class Destructible : Entity
    {
        #region Properties

        [SerializeField] private UnityEvent _eventOnDeath;
        public UnityEvent EventOnDeath => _eventOnDeath;

        /// <summary>
        /// Object ignores damage
        /// </summary>
        [SerializeField] private bool _indestructible;
        public bool IsIndestructible => _indestructible;

        /// <summary>
        /// Basic value of HP
        /// </summary>
        [SerializeField] private int _hitPoints;

        /// <summary>
        /// Current HP
        /// </summary>
        private int _currentHitPoints;
        public int HitPoints => _currentHitPoints;

        #endregion

        #region Unity Events

        protected virtual void Start()
        {
            _currentHitPoints = _hitPoints;
        }

        #endregion

        #region Public API

        /// <summary>
        /// Apply damage to the object
        /// </summary>
        /// <param name="damage">value of damage applying</param>
        public void ApplyDamage(int damage)
        {
            if (_indestructible == true) return;

            _currentHitPoints -= damage;

            if (_currentHitPoints <= 0)
                OnDestruction();

        }

        #endregion

        #region Private API
        /// <summary>
        /// rewritable event of object destruction when HP is 0 or lower.
        /// </summary>
        protected virtual void OnDestruction()
        {
            Destroy(gameObject);
            _eventOnDeath?.Invoke();
        }
        #endregion
    }
}
