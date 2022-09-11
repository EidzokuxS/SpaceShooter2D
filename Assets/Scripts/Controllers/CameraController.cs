using UnityEngine;

namespace SpaceShooter
{
    public class CameraController : MonoBehaviour
    {
        #region Properties

        [SerializeField] private Camera _camera;

        [SerializeField] private Transform _target;

        [SerializeField] private float _interpolationLinear;

        [SerializeField] private float _interpolationAngular;

        [SerializeField] private float _cameraZOffset;

        [SerializeField] private float _forwardOffset;

        #endregion

        #region UnityEvents

        private void FixedUpdate()
        {
            if (_target == null || _camera == null) return;

            Vector2 camPos = _camera.transform.position;
            Vector2 targetpos = _target.position + _target.transform.up * _forwardOffset;

            Vector2 newCamPos = Vector2.Lerp(camPos, targetpos, _interpolationLinear * Time.fixedDeltaTime);

            _camera.transform.position = new Vector3(newCamPos.x, newCamPos.y, _cameraZOffset);

            if (_interpolationAngular > 0)
            {
                _camera.transform.rotation = Quaternion.Slerp(_camera.transform.rotation,
                                                               _target.rotation, _interpolationAngular * Time.deltaTime);
            }
        }

        #endregion

        #region Public API


        public void SetTarget(Transform newTarget)
        {
            _target = newTarget;
        }

        #endregion

    }
}

