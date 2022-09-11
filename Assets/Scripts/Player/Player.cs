using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Player : MonoBehaviour
    {
        #region Properties

        [SerializeField] private int _livesAmount;
        [SerializeField] private SpaceShip _ship;
        [SerializeField] private GameObject _playerShipPrefab;

        [SerializeField] private CameraController _cameraController;
        [SerializeField] private MovementController _movementController;

        #endregion

        #region Unity Events

        private void Start()
        {
            _ship.EventOnDeath.AddListener(OnShipDestruction);
        }

        #endregion

        #region Private API

        private void OnShipDestruction()
        {
            _livesAmount--;

            if (_livesAmount > 0)
                Respawn();
        }

        private void Respawn()
        {
            var newPlayerShip = Instantiate(_playerShipPrefab);

            _ship = newPlayerShip.GetComponent<SpaceShip>();

            _cameraController.SetTarget(_ship.transform);
            _movementController.SetTargetShip(_ship);
        }
        #endregion

    }
}

