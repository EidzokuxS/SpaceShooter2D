using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Position limiter. Works in connection with LevelBoundary script if it's on the scene.
    /// Puts on objects that should be limited.
    /// </summary>
    public class LevelBoundaryLimiter : MonoBehaviour
    {
        #region Unity Events
        private void Update()
        {
            if (LevelBoundary.Instance == null) return;

            var lvlBoundary = LevelBoundary.Instance;
            var r = lvlBoundary.Radius;

            if(transform.position.magnitude > r)
            {
                if (lvlBoundary.LimitMode == LevelBoundary.Mode.Limit)
                {
                    transform.position = transform.position.normalized * r;
                }

                if (lvlBoundary.LimitMode == LevelBoundary.Mode.Teleport)
                {
                    transform.position = -transform.position.normalized * r;
                }
            }
        }
        #endregion
    }
}

