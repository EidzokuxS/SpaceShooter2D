using UnityEngine;

namespace SpaceShooter
{
    public class Turret : MonoBehaviour
    {
        #region Properties

        [SerializeField] private TurretMode _mode;
        public TurretMode Mode => _mode;

        [SerializeField] private TurretProperties _turretProperties;

        private float _reloadTimer;

        public bool CanFire => _reloadTimer <= 0;

        private SpaceShip _weapon;

        #endregion

        #region Unity Events

        private void Start()
        {
            _weapon = transform.parent.GetComponent<SpaceShip>();
        }

        private void Update()
        {
            if (_reloadTimer > 0)
                _reloadTimer -= Time.deltaTime;
        }
        #endregion

        #region Public API

        public void Fire()
        {
            if (_turretProperties == null) return;

            if (_reloadTimer > 0) return;

            if (_weapon.DrawEnergy(_turretProperties.EnergyUsage) == false)
                return;

            if (_weapon.DrawAmmo(_turretProperties.AmmoUsage) == false)
                return;


            Projectile projectile = Instantiate(_turretProperties.ProjectilePrefab, transform.parent).GetComponent<Projectile>();
            projectile.transform.position = transform.position;
            projectile.transform.up = transform.up;

            _reloadTimer = _turretProperties.FireRate;

        }

        public void AssignLoadout(TurretProperties props)
        {
            if (_mode != props.Mode) return;

            _reloadTimer = 0;
            _turretProperties = props;
        }

        #endregion
    }

}
