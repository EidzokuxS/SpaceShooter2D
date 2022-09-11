using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class SyncTransform : MonoBehaviour
    {
        #region Properties

        [SerializeField] private Transform m_Target;

        #endregion

        #region UnityEvents

        private void Update()
        {
            transform.position = new Vector3(m_Target.position.x, m_Target.position.y, transform.position.z);
        }

        #endregion
    }
}

