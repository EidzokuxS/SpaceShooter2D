using UnityEngine;

namespace SpaceShooter
{
    public class PowerUpStats : PowerUp
    {
        #region Properties

        public enum EffectType
        {
            AddAmmo,
            AddEnergy
        }


        [SerializeField] private EffectType _effectType;

        [SerializeField] private float _value;
        #endregion

        #region Private API
        protected override void OnPickedUp(SpaceShip ship)
        {
            if (_effectType == EffectType.AddAmmo)
                ship.AddAmmo((int)_value);
            if (_effectType == EffectType.AddEnergy)
                ship.AddEnergy((int)_value);
        }
        #endregion

    }
}

