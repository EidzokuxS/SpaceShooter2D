using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(CircleArea))]
    public class EntitySpawner : MonoBehaviour
    {
        #region Properties

        public enum SpawnMode
        {
            Start,
            Loop
        }

        [SerializeField] private Entity[] _entityPrefabs;

        [SerializeField] private CircleArea _circleArea;

        [SerializeField] private SpawnMode _spawnMode;

        [SerializeField] private int _spawnCount;

        [SerializeField] private float _spawnTime;

        private float _timer;

        #endregion

        #region Unity Events

        private void Start()
        {
            if (_spawnMode == SpawnMode.Start)
            {
                SpawnEntities();
            }

            _timer = _spawnTime;
        }

        private void Update()
        {
            if (_timer > 0)
            {
                _timer -= Time.deltaTime;
            }

            if (_spawnMode == SpawnMode.Loop && _timer <= 0)
            {
                SpawnEntities();

                _timer = _spawnTime;
            }
        }
        #endregion

        #region Public API

        #endregion

        #region Private API

        private void SpawnEntities()
        {
            for (int i = 0; i < _spawnCount; i++)
            {
                int index = Random.Range(0, _entityPrefabs.Length);

                GameObject entities = Instantiate(_entityPrefabs[index].gameObject);

                entities.transform.position = _circleArea.GetRandomInsideZone();
            }
        }

        #endregion
    }

}

