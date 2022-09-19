using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(CircleArea))]
    public class EntitySpawnerDebris : MonoBehaviour
    {
        #region Properties

        [SerializeField] private Destructible[] _debrisPrefabs;

        [SerializeField] private CircleArea _circleArea;

        [SerializeField] private int _debrisCount;

        [SerializeField] private float _randomSpeed;

        #endregion

        #region Unity Events

        private void Start()
        {
            for (int i = 0; i < _debrisCount; i++)
            {
                SpawnDebris();
            }
        }

        #endregion

        #region private API

        private void SpawnDebris()
        {
            int index = Random.Range(0, _debrisPrefabs.Length);

            GameObject debris = Instantiate(_debrisPrefabs[index].gameObject);

            debris.transform.position = _circleArea.GetRandomInsideZone();
            debris.GetComponent<Destructible>().EventOnDeath.AddListener(OnDebrisDestructed);

            Rigidbody2D rigidbody = debris.GetComponent<Rigidbody2D>();

            if (rigidbody != null && _randomSpeed > 0)
            {
                rigidbody.velocity = (Vector2)Random.insideUnitSphere * _randomSpeed;
            }

        }

        private void OnDebrisDestructed()
        {
            SpawnDebris();
        }

        #endregion
    }
}

