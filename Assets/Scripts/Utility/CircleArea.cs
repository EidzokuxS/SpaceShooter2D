#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;



namespace SpaceShooter
{
    public class CircleArea : MonoBehaviour
    {
        #region Properties

        [SerializeField] private float _radius;
        public float Radius => _radius;

        private static Color GizmoColor = new Color(0, 1, 0, 0.3f);


        #endregion

        #region Public API

        public Vector2 GetRandomInsideZone()
        {
            return (Vector2)transform.position + (Vector2)Random.insideUnitSphere * _radius;
        }

        #endregion

        #region Private API


#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Handles.color = GizmoColor;
            Handles.DrawSolidDisc(transform.position, transform.forward, _radius);
        }
#endif

        #endregion
    }
}

