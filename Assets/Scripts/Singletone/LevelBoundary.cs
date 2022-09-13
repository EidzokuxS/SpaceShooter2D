using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class LevelBoundary : MonoBehaviour
    {
        #region Singletone
        public static LevelBoundary Instance;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("LevelBoundary has already been created at this scene!");
                Destroy(gameObject);
                return;
            }

            Instance = this;

            DontDestroyOnLoad(gameObject);
        }
        #endregion

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

