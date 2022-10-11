using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    public interface ILevelCondition
    {
        bool IsCompleted { get; }
    }

    public class LevelController : SingletonBase<LevelController>
    {
        #region Properties

        [SerializeField] private int _referenceTime;
        public int ReferenceTime => _referenceTime;

        [SerializeField] private UnityEvent _eventLevelCompleted;

        private ILevelCondition[] _conditions;

        private bool _isLevelCompleted;

        private float _levelTime;
        public float LevelTime => _levelTime;
        #endregion

        #region Unity Events

        private void Start()
        {
            _conditions = GetComponentsInChildren<ILevelCondition>();
        }

        private void Update()
        {
            if (!_isLevelCompleted)
            {
                _levelTime += Time.deltaTime;

                CheckLevelConditions();
            }
        }


        #endregion

        #region Private API

        private void CheckLevelConditions()
        {
            if (_conditions == null || _conditions.Length == 0)
                return;

            int numCompleted = 0;

            foreach (var v in _conditions)
            {
                if (v.IsCompleted)
                    numCompleted++;
            }

            if (numCompleted == _conditions.Length)
            {
                _isLevelCompleted = true;
                _eventLevelCompleted?.Invoke();

                LevelSequenceController.Instance.FinishCurrentLevel(true);
            }
        }

        #endregion


    }

}

