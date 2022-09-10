using UnityEngine;

namespace SpaceShooter
{
    public class CameraController : MonoBehaviour
    {
        #region Properties

        [SerializeField] private Camera m_Camera;

        [SerializeField] private Transform m_Target;

        [SerializeField] private float m_InterpolationLinear;

        [SerializeField] private float m_InterpolationAngular;

        [SerializeField] private float m_CameraZOffset;

        [SerializeField] private float m_ForwardOffset;

        #endregion

        #region UnityEvents

        private void FixedUpdate()
        {
            if (m_Target == null || m_Camera == null) return;

            Vector2 camPos = m_Camera.transform.position;
            Vector2 targetpos = m_Target.position + m_Target.transform.up * m_ForwardOffset;

            Vector2 newCamPos = Vector2.Lerp(camPos, targetpos, m_InterpolationLinear * Time.fixedDeltaTime);

            m_Camera.transform.position = new Vector3(newCamPos.x, newCamPos.y, m_CameraZOffset);

            if (m_InterpolationAngular > 0)
            {
                m_Camera.transform.rotation = Quaternion.Slerp(m_Camera.transform.rotation,
                                                               m_Target.rotation, m_InterpolationAngular * Time.deltaTime);
            }
        }

        #endregion

    }
}

