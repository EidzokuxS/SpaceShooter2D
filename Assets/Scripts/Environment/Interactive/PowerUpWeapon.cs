using UnityEngine;

namespace SpaceShooter
{
    public class PowerUpWeapon : PowerUp
    {
        #region Properties

        [SerializeField] private TurretProperties _properties;

        #endregion

        #region Private API
        protected override void OnPickedUp(SpaceShip ship)
        {
            ship.AssignWeapon(_properties);
        }
        #endregion

    }
}