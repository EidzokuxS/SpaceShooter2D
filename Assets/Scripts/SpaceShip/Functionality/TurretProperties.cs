using UnityEngine;

namespace SpaceShooter
{
    public enum TurretMode
    {
        Primary,
        Secondary
    }

    [CreateAssetMenu]
    public sealed class TurretProperties : ScriptableObject
    {
        #region Properties

        [SerializeField] private TurretMode _mode;
        public TurretMode Mode => _mode;

        [SerializeField] private Projectile _projectilePrefab;
        public Projectile ProjectilePrefab => _projectilePrefab;

        [SerializeField] private float _fireRate;
        public float FireRate => _fireRate;

        [SerializeField] private int _energyUsage;
        public int EnergyUsage => _energyUsage;

        [SerializeField] private int _ammoUsage;
        public int AmmoUsage => _ammoUsage;

        [SerializeField] private AudioClip _launchSFX;
        public AudioClip LaunchSFX => _launchSFX;

        #endregion
    }
}

