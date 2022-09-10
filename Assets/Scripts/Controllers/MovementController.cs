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

        [SerializeField] private SpaceShip m_TargetShip;

        [SerializeField] private VirtualJoystick m_MobileJoystick;

        [SerializeField] private ControlMode m_ControlMode;

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

            if (m_ControlMode == ControlMode.Keyboard)
            {
                m_MobileJoystick.gameObject.SetActive(false);
            }
            else
            {
                m_MobileJoystick.gameObject.SetActive(true);
            }

        }

        private void Update()
        {
            if (m_TargetShip == null) return;

            if (m_ControlMode == ControlMode.Keyboard)
                ControlKeyboard();

            if (m_ControlMode == ControlMode.Mobile)
                ControlMobile();
        }

        #endregion

        #region Private API
        private void ControlMobile()
        {
            var dir = m_MobileJoystick.Value;
            m_TargetShip.ThrustControl = dir.y;
            m_TargetShip.TorqueControl = -dir.x;
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

            m_TargetShip.ThrustControl = thrust;
            m_TargetShip.TorqueControl = torque;
        }
        #endregion

    }
}
