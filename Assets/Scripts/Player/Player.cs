using UnityEngine;

namespace SpaceShooter
{
    public class Player : SingletonBase<Player>
    {
        #region Properties

        [SerializeField] private int _livesAmount;
        [SerializeField] private SpaceShip _ship;
        [SerializeField] private GameObject _playerShipPrefab;
        public SpaceShip ActiveShip => _ship;
        public int Score { get; private set; }
        public int KillCount { get; private set; }

        [SerializeField] private CameraController _cameraController;
        [SerializeField] private MovementController _movementController;

        private int _currentLives;

        #endregion

        #region Unity Events
        protected override void Awake()
        {
            base.Awake();

            if (_ship != null)
                Destroy(_ship.gameObject);


        }
        private void Start()
        {
            Respawn();
            _currentLives = _livesAmount;
            _ship.EventOnDeath.AddListener(OnShipDestruction);
        }

        #endregion

        #region Private API

        private void OnShipDestruction()
        {
            _currentLives--;
            _ship.EventOnDeath.RemoveListener(OnShipDestruction);

            if (_currentLives > 0)
                Invoke(nameof(Respawn), 2);
            else
                LevelSequenceController.Instance.FinishCurrentLevel(false);
        }

        private void Respawn()
        {
            if (LevelSequenceController.PlayerShip != null)
            {
                var newPlayerShip = Instantiate(LevelSequenceController.PlayerShip);

                _ship = newPlayerShip.GetComponent<SpaceShip>();
                _ship.EventOnDeath.AddListener(OnShipDestruction);

                _cameraController.SetTarget(_ship.transform);
                _movementController.SetTargetShip(_ship);
            }
        }
        #endregion

        #region Public API

        public void AddKill()
        {
            KillCount++;
        }

        public void ChangeScore(int amount)
        {
            if (Score + amount < 0)
            {
                Score = 0;
            }

            Score += amount;
        }

        #endregion
    }
}

