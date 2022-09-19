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
        [SerializeField] private float _mass;

        /// <summary>
        /// Pushing forward force
        /// </summary>
        [SerializeField] private float _thrust;

        /// <summary>
        /// Rotation force
        /// </summary>
        [SerializeField] private float _mobility;

        /// <summary>
        /// Max linear speed
        /// </summary>
        [SerializeField] private float _maxLinearVelocity;
        public float MaxLinearVelocity { get { return _maxLinearVelocity; } set { _maxLinearVelocity = value; } }

        /// <summary>
        /// Max rotation speed degree/sec
        /// </summary>
        [SerializeField] private float _maxAngularVelocity;
        public float MaxAngularVelocity { get { return _maxAngularVelocity; } set { _maxAngularVelocity = value; } }

        /// <summary>
        /// Saved rigidbody index
        /// </summary>
        private Rigidbody2D _rigid;

        [Header("Turrets")]
        [SerializeField] private Turret[] _turrets;
        [SerializeField] private int _maxEnergy;
        [SerializeField] private int _maxAmmo;
        [SerializeField] private int _energyRegenPerSec;

        private float _currentEnergy;
        private int _currentAmmo;


        #region Bonuses

        private float _indestructibleBonusTimer;
        public float IndestructibleBonusTimer { get { return _indestructibleBonusTimer; } set { _indestructibleBonusTimer = value; } }

        private float _speedUpBonusTimer;
        public float SpeedUpBonusTimer { get { return _speedUpBonusTimer; } set { _speedUpBonusTimer = value; } }

        public bool IsIndestructibleBonusActive;
        public bool IsSpeedBonusActive;

        private float _baseLinearVelocity;
        private float _baseAngularVelocity;

        #endregion

        #endregion

        #region Unity Events

        protected override void Start()
        {
            base.Start();

            _rigid = GetComponent<Rigidbody2D>();
            _rigid.mass = _mass;

            _rigid.inertia = 1;

            InitAmmunition();

            _baseAngularVelocity = _maxAngularVelocity;
            _baseLinearVelocity = _maxLinearVelocity;
        }

        private void Update()
        {
            EnableIndestructible();

            EnableSpeedUp();
        }

        private void FixedUpdate()
        {
            UpdateRigidbody();

            UpdateEnergyRegen();
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
            _rigid.AddForce(_thrust * ThrustControl * Time.fixedDeltaTime * transform.up, ForceMode2D.Force);

            _rigid.AddForce((_thrust / _maxLinearVelocity) * Time.fixedDeltaTime * -_rigid.velocity, ForceMode2D.Force);

            _rigid.AddTorque(TorqueControl * _mobility * Time.fixedDeltaTime, ForceMode2D.Force);

            _rigid.AddTorque(-_rigid.angularVelocity * (_mobility / _maxAngularVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);
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

        private void UpdateEnergyRegen()
        {
            _currentEnergy += (float)_energyRegenPerSec * Time.fixedDeltaTime;

            _currentEnergy = Mathf.Clamp(_currentEnergy, 0, _maxEnergy);
        }

        private void EnableIndestructible()
        {
            if (IsIndestructibleBonusActive == true)
            {
                IsIndestructible = true;

                _indestructibleBonusTimer -= Time.deltaTime;

                if (_indestructibleBonusTimer <= 0)
                {
                    IsIndestructible = false;
                    IsIndestructibleBonusActive = false;
                }
            }
        }
        private void EnableSpeedUp()
        {
            if (IsSpeedBonusActive == true)
            {
                _speedUpBonusTimer -= Time.deltaTime;

                if (_speedUpBonusTimer <= 0)
                {
                    _maxAngularVelocity = _baseAngularVelocity;
                    _maxLinearVelocity = _baseLinearVelocity;

                    IsSpeedBonusActive = false;
                }
            }
        }

        #endregion
    }
}

