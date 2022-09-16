using UnityEngine;

namespace SpaceShooter
{
    public class Player : SingletonBase<Player>
    {
        #region Properties

        [SerializeField] private int _livesAmount;
        [SerializeField] private SpaceShip _ship;
        [SerializeField] private GameObject _playerShipPrefab;
        public SpaceShip ActiveShip => _ship;

        [SerializeField] private CameraController _cameraController;
        [SerializeField] private MovementController _movementController;

        private int _currentLives;

        #endregion

        #region Unity Events

        private void Start()
        {
            _currentLives = _livesAmount;
            _ship.EventOnDeath.AddListener(OnShipDestruction);
        }

        #endregion

        #region Private API

        private void OnShipDestruction()
        {
            _currentLives--;
            _ship.EventOnDeath.RemoveListener(OnShipDestruction);

            if (_currentLives > 0)
                Invoke(nameof(Respawn), 2);
        }

        private void Respawn()
        {
            var newPlayerShip = Instantiate(_playerShipPrefab);

            _ship = newPlayerShip.GetComponent<SpaceShip>();
            _ship.EventOnDeath.AddListener(OnShipDestruction);

            _cameraController.SetTarget(_ship.transform);
            _movementController.SetTargetShip(_ship);
        }
        #endregion

    }
}

