using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class GravityWell : MonoBehaviour
    {
        #region Properties

        [SerializeField] private float _force;
        [SerializeField] private float _radius;

        #endregion

        #region UnityEvents

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.attachedRigidbody == null) return;

            Vector2 dir = transform.position - collision.transform.position;

            float dist = dir.magnitude;

            if (dist < _radius)
            {
                Vector2 force = dir.normalized * _force * (dist / _radius);

                collision.attachedRigidbody.AddForce(force, ForceMode2D.Force);
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            GetComponent<CircleCollider2D>().radius = _radius;
        }
#endif
        #endregion
    }
}

