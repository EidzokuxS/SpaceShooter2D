using UnityEngine;


namespace SpaceShooter
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceShip : Destructible
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
        [SerializeField] private float m_MaxLinearVelocity;

        /// <summary>
        /// Max rotation speed degree/sec
        /// </summary>
        [SerializeField] private float m_MaxAngularVelocity;

        /// <summary>
        /// Saved rigidbody index
        /// </summary>
        private Rigidbody2D m_Rigid;

        [Header("Turrets")]
        [SerializeField] private Turret[] _turrets;
        [SerializeField] private int _maxEnergy;
        [SerializeField] private int _maxAmmo;
        [SerializeField] private int _energyRegenPerSec;

        private float _currentEnergy;
        private int _currentAmmo;
        #endregion

        #region Unity Events

        protected override void Start()
        {
            base.Start();

            m_Rigid = GetComponent<Rigidbody2D>();
            m_Rigid.mass = m_Mass;

            m_Rigid.inertia = 1;

            InitAmmunition();
        }

        private void FixedUpdate()
        {
            UpdateRigidbody();

            UpdateEenrgyRegen();
        }

        #endregion

        #region Public API

        /// <summary>
        /// Linear thrust controller -1.0 to 1.0
        /// </summary>
        public float ThrustControl { get; set; }

        /// <summary>
        /// Rotation thrust controller -1.0 to 1.0
        /// </summary>
        public float TorqueControl { get; set; }

        public void Fire(TurretMode mode)
        {
            for (int i = 0; i < _turrets.Length; i++)
            {
                if (_turrets[i].Mode == mode)
                    _turrets[i].Fire();
            }
        }


        public void AddEnergy(int amount)
        {
            _currentEnergy = Mathf.Clamp(_currentEnergy + amount, 0, _maxEnergy);
        }

        public void AddAmmo(int amount)
        {
            _currentAmmo += Mathf.Clamp(_currentAmmo + amount, 0, _maxAmmo); ;
        }

        public bool DrawEnergy(int amount)
        {
            if (amount == 0)
                return true;

            if (_currentEnergy >= amount)
            {
                _currentEnergy -= amount;
                return true;
            }

            return false;
        }

        public bool DrawAmmo(int amount)
        {
            if (amount == 0)
                return true;

            if (_currentAmmo >= amount)
            {
                _currentAmmo -= amount;
                return true;
            }

            return false;
        }

        public void AssignWeapon(TurretProperties properties)
        {
            for (int i = 0; i < _turrets.Length; i++)
            {
                _turrets[i].AssignLoadout(properties);
            }
        }

        #endregion

        #region Private API

        /// <summary>
        /// Method of applying forces to the ship.
        /// </summary>
        private void UpdateRigidbody()
        {
            m_Rigid.AddForce(m_Thrust * ThrustControl * Time.fixedDeltaTime * transform.up, ForceMode2D.Force);

            m_Rigid.AddForce((m_Thrust / m_MaxLinearVelocity) * Time.fixedDeltaTime * -m_Rigid.velocity, ForceMode2D.Force);

            m_Rigid.AddTorque(TorqueControl * m_Mobility * Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigid.AddTorque(-m_Rigid.angularVelocity * (m_Mobility / m_MaxAngularVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);
        }

        protected override void OnDestruction()
        {
            var DestructionEffect = GetComponentInChildren<EffectTrigger>();

            DestructionEffect.TriggerEffect(transform.position);

            base.OnDestruction();
        }

        private void InitAmmunition()
        {
            _currentAmmo = _maxAmmo;
            _currentEnergy = _maxEnergy;
        }

        private void UpdateEenrgyRegen()
        {
            _currentEnergy += (float)_energyRegenPerSec * Time.fixedDeltaTime;

            _currentEnergy = Mathf.Clamp(_currentEnergy, 0, _maxEnergy);
        }
        #endregion
    }
}

