using UnityEngine;

namespace SpaceShooter
{
    public class MovementController : MonoBehaviour
    {
        #region Properties


        public enum ControlMode
        {
            Keyboard,
            Mobile
        }

        [SerializeField] private SpaceShip _targetShip;
        public void SetTargetShip(SpaceShip ship) => _targetShip = ship;

        [SerializeField] private VirtualJoystick _mobileJoystick;

        [SerializeField] private ControlMode _controlMode;

        [SerializeField] private PointerClickHold _mobileFirePrimary;
        [SerializeField] private PointerClickHold _mobileFireSecondary;
        [SerializeField] private PointerClickHold _mobilePauseButton;

        [SerializeField] private UIPauseMenuPanel _pauseMenuPanel;

        #endregion

        #region UnityEvents

        private void Start()
        {
            if (Application.isMobilePlatform)
            {
                _controlMode = ControlMode.Mobile;

                _mobileJoystick.gameObject.SetActive(true);

                _mobileFirePrimary.gameObject.SetActive(true);
                _mobileFireSecondary.gameObject.SetActive(true);
                _mobilePauseButton.gameObject.SetActive(true);
            }
            else
            {
                _controlMode = ControlMode.Keyboard;

                _mobileJoystick.gameObject.SetActive(false);

                _mobileFirePrimary.gameObject.SetActive(false);
                _mobileFireSecondary.gameObject.SetActive(false);
                _mobilePauseButton.gameObject.SetActive(false);
            }

        }

        private void Update()
        {
            if (_targetShip == null) return;

            if (_controlMode == ControlMode.Keyboard)
                ControlKeyboard();

            if (_controlMode == ControlMode.Mobile)
                ControlMobile();
        }

        #endregion

        #region Private API
        private void ControlMobile()
        {
            var dir = _mobileJoystick.Value;
            _targetShip.ThrustControl = dir.y;
            _targetShip.TorqueControl = -dir.x;

            if (_mobileFirePrimary.IsHold)
                _targetShip.Fire(TurretMode.Primary);
            if (_mobileFireSecondary.IsHold)
                _targetShip.Fire(TurretMode.Secondary);
            if (_mobilePauseButton.IsHold)
                _pauseMenuPanel.OnPauseButtonPressed();


        }

        private void ControlKeyboard()
        {
            float thrust = 0;
            float torque = 0;

            if (Input.GetKey(KeyCode.UpArrow))
                thrust = 1.0f;

            if (Input.GetKey(KeyCode.DownArrow))
                thrust = -1.0f;

            if (Input.GetKey(KeyCode.LeftArrow))
                torque = 1.0f;

            if (Input.GetKey(KeyCode.RightArrow))
                torque = -1.0f;

            if (Input.GetKey(KeyCode.E))
                _targetShip.Fire(TurretMode.Primary);
            if (Input.GetKey(KeyCode.R))
                _targetShip.Fire(TurretMode.Secondary);

            if (Input.GetKey(KeyCode.Escape))
                _pauseMenuPanel.OnPauseButtonPressed();


            _targetShip.ThrustControl = thrust;
            _targetShip.TorqueControl = torque;
        }
        #endregion

    }
}
