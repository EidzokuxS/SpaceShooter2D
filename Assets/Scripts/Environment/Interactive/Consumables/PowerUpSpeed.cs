using UnityEngine;

namespace SpaceShooter
{
    public class PowerUpSpeed : PowerUp
    {
        #region Properties

        [SerializeField] private float _speedUpTime;
        [SerializeField] private float _speedBonus;

        #endregion

        #region Private API
        protected override void OnPickedUp(SpaceShip ship)
        {
            ship.SpeedUpBonusTimer = _speedUpTime;

            ship.MaxAngularVelocity *= _speedBonus;
            ship.MaxLinearVelocity *= _speedBonus;

            ship.IsSpeedBonusActive = true;
        }
        #endregion

    }
}