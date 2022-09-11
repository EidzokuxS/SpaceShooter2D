using System.Collections;
using System.Collections.Generic;
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

        #endregion

        #region UnityEvents
        private void Start()
        {
            /*if(Application.isMobilePlatform)
            {
                m_ControlMode = ControlMode.Mobile;
                m_MobileJoystick.gameObject.SetActive(true);
            }
            else
            {
                m_ControlMode = ControlMode.Keyboard;
                m_MobileJoystick.gameObject.SetActive(false);
            }*/

            if (_controlMode == ControlMode.Keyboard)
            {
                _mobileJoystick.gameObject.SetActive(false);
            }
            else
            {
                _mobileJoystick.gameObject.SetActive(true);
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

            _targetShip.ThrustControl = thrust;
            _targetShip.TorqueControl = torque;
        }
        #endregion

    }
}
