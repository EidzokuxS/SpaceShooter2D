using UnityEngine;

namespace SpaceShooter
{
    public class Player : SingletonBase<Player>
    {
        #region Properties

        [SerializeField] private int _livesAmount;
        [SerializeField] private SpaceShip _ship;
        public SpaceShip ActiveShip => _ship;

        [SerializeField] private int _scoreModifierFast;
        public int ScoreModifierFast => _scoreModifierFast;
        [SerializeField] private int _scoreModifierExtraFast;
        public int ScoreModifierExtraFast => _scoreModifierExtraFast;

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
            _currentLives = _livesAmount;
            _ship.EventOnDeath.AddListener(OnShipDestruction);

            if (_currentLives == _livesAmount)
                Respawn();
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

