using System.Collections.Generic;
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
        public const int TeamIdNeutral = 0;


        [SerializeField] protected UnityEvent _eventOnDeath;
        public UnityEvent EventOnDeath => _eventOnDeath;

        /// <summary>
        /// Object ignores damage
        /// </summary>
        [SerializeField] protected bool _indestructible;
        public bool IsIndestructible { get { return _indestructible; } set { _indestructible = value; } }

        /// <summary>
        /// Basic value of HP
        /// </summary>
        [SerializeField] protected int _hitPoints;

        /// <summary>
        /// Current HP
        /// </summary>
        private int _currentHitPoints;
        public int HitPoints => _currentHitPoints;

        [SerializeField] private int _teamID;
        public int TeamID => _teamID;

        [SerializeField] private int _scoreValue;
        public int ScoreValue => _scoreValue;

        private static HashSet<Destructible> _allDestructibles;

        public static IReadOnlyCollection<Destructible> AllDestructibles => _allDestructibles;

        #endregion

        #region Unity Events

        protected virtual void Start()
        {
            OnEnable();
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

        protected virtual void OnEnable()
        {
            if (_allDestructibles == null)
                _allDestructibles = new HashSet<Destructible>();

            _allDestructibles.Add(this);
        }

        /// <summary>
        /// rewritable event of object destruction when HP is 0 or lower.
        /// </summary>
        protected virtual void OnDestruction()
        {
            _allDestructibles.Remove(this);
            Destroy(gameObject);
            _eventOnDeath?.Invoke();
        }
        #endregion
    }
}
