using UnityEngine;

namespace SpaceShooter
{
    public class PowerUpIndestructible : PowerUp
    {
        #region Properties

        [SerializeField] private float _invunerableTime;

        #endregion

        #region Private API
        protected override void OnPickedUp(SpaceShip ship)
        {
            ship.IndestructibleBonusTimer = _invunerableTime;
            ship.IsIndestructibleBonusActive = true;
        }
        #endregion

    }
}