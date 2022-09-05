using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SpaceShooter
{
    /// <summary>
    /// Destructible object. Object that can have hitpoints.
    /// </summary>
    public class Destuctible : Entity
    {
        #region Properties
        /// <summary>
        /// Object ignores damage
        /// </summary>
        [SerializeField] private bool m_Indestructible;
        public bool IsIndestructible => m_Indestructible;

        /// <summary>
        /// Basic value of HP
        /// </summary>
        [SerializeField] private int m_HitPoints;

        /// <summary>
        /// Current HP
        /// </summary>
        private int m_currentHitPoints;
        public int HitPoints => m_currentHitPoints;

        #endregion

        #region Unity Events

        protected virtual void Start()
        {
            m_currentHitPoints = m_HitPoints;
        }

        #endregion

        #region Public API

        /// <summary>
        /// Apply damage to the object
        /// </summary>
        /// <param name="damage">value of damage applying</param>
        public void ApplyDamage(int damage)
        {
            if (m_Indestructible == true) return;

            m_currentHitPoints -= damage;

            if (m_currentHitPoints <= 0)
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
        }
        #endregion
    }
}
