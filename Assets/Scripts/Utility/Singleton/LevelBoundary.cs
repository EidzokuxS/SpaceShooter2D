using UnityEngine;

namespace SpaceShooter
{
    public class LevelBoundary : SingletonBase<LevelBoundary>
    {
        #region Properties

        [SerializeField] private float _radius;
        public float Radius => _radius;

        public enum Mode
        {
            Limit,
            Teleport
        }

        [SerializeField] private Mode _limitMode;
        public Mode LimitMode => _limitMode;
        #endregion

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            UnityEditor.Handles.color = Color.green;
            UnityEditor.Handles.DrawWireDisc(transform.position, transform.forward, _radius);
        }
#endif
    }

}

