using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SpaceShooter
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceShip : Destuctible
    {
        #region Properties

        [Header("Spaceship")]
        /// <summary>
        /// Mass for automatic setup in rigidbody
        /// </summary>
        [SerializeField] private float m_Mass;

        /// <summary>
        /// Pushing forward force
        /// </summary>
        [SerializeField] private float m_Thrust;

        /// <summary>
        /// Rotation force
        /// </summary>
        [SerializeField] private float m_Mobility;

        /// <summary>
        /// Max linear speed
        /// </summary>
        [SerializeField] private float m_MaxLinearvelocity;

        /// <summary>
        /// Max rotation speed degree/sec
        /// </summary>
        [SerializeField] private float m_MaxAngularVelocity;

        /// <summary>
        /// Saved rigidbody index
        /// </summary>
        private Rigidbody2D m_Rigid;

        #endregion

        #region Unity Events

        protected override void Start()
        {
            base.Start();

            m_Rigid = GetComponent<Rigidbody2D>();
            m_Rigid.mass = m_Mass;

            m_Rigid.inertia = 1;
        }

        #endregion

        #region Public API

        /// <summary>
        /// Linear thrust controller -1.0 to 1.0
        /// </summary>
        public float ThrustControl => m_Thrust;

        /// <summary>
        /// Rotation thrust controller -1.0 to 1.0
        /// </summary>
        public float TorqueControl => m_Mobility;

        #endregion
    }
}

