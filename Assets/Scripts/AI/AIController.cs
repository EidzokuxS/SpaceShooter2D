using UnityEngine;


namespace SpaceShooter
{
    [RequireComponent(typeof(SpaceShip))]
    public class AIController : MonoBehaviour
    {
        #region Properties

        public enum AIBehaviour
        {
            Null,
            Patrol
        }

        [SerializeField] private AIBehaviour _AIBehaviour;

        [SerializeField] private AIPointPatrol _patrolPoint;

        [Range(0f, 1f)]
        [SerializeField] private float _navigatorLinear;

        [Range(0f, 1f)]
        [SerializeField] private float _navigatorAngular;

        [SerializeField] private float _randomSelectMovePointTime;

        [SerializeField] private float _findNewTargetTime;

        [SerializeField] private float _shootDelay;

        [SerializeField] private float _evadeRayLenght;

        private SpaceShip _spaceShip;

        private Vector3 _movePosition;

        private Destructible _selectedTarget;

        private Timer _randomizeDirectionTimer;

        #endregion

        #region Unity Events

        private void Start()
        {
            _spaceShip = GetComponent<SpaceShip>();

            InitTimers();
        }

        private void Update()
        {
            UpdateTimers();

            UpdateAI();
        }

        #endregion

        #region Public API

        public void SetPatrolBehaviour(AIPointPatrol point)
        {
            _AIBehaviour = AIBehaviour.Patrol;
            _patrolPoint = point;
        }

        #endregion

        #region Private API

        private void UpdateAI()
        {
            if (_AIBehaviour == AIBehaviour.Patrol)
            {
                UpdateBehaviourPatrol();
            }
        }

        private void UpdateBehaviourPatrol()
        {
            ActionFindNewMovePosition();
            ActionAvoidCollision();
            ActionControlShip();
            ActionFindNewAttackTarget();
            ActionFire();
        }

        #region Timers

        private void InitTimers()
        {
            _randomizeDirectionTimer = new Timer(_randomSelectMovePointTime);
        }

        private void UpdateTimers()
        {
            _randomizeDirectionTimer.TimeUpdater(Time.deltaTime);
        }

        #endregion

        private void ActionFindNewMovePosition()
        {
            if (_AIBehaviour == AIBehaviour.Patrol)
            {
                if (_selectedTarget != null)
                {
                    _movePosition = _selectedTarget.transform.position;
                }
                else
                {
                    if (_patrolPoint != null)
                    {
                        bool isInsidePatrolZone = (_patrolPoint.transform.position - transform.position).sqrMagnitude < _patrolPoint.Radius * _patrolPoint.Radius;

                        if (isInsidePatrolZone == true)
                        {
                            if (_randomizeDirectionTimer.IsFinished == true)
                            {
                                Vector2 newPoint = Random.onUnitSphere * _patrolPoint.Radius + _patrolPoint.transform.position;
                                _movePosition = newPoint;

                                _randomizeDirectionTimer.Start(_randomSelectMovePointTime);
                            }
                        }
                        else
                        {
                            _movePosition = _patrolPoint.transform.position;
                        }
                    }
                }

            }
        }

        private void ActionAvoidCollision()
        {
            if (Physics2D.Raycast(transform.position, transform.up, +_evadeRayLenght) == true)
            {
                _movePosition = transform.position + transform.right * 100.0f;
            }
        }

        private void ActionControlShip()
        {
            _spaceShip.ThrustControl = _navigatorLinear;
            _spaceShip.TorqueControl = ComputeAliginTorqueNormalized(_movePosition, _spaceShip.transform) * _navigatorAngular;
        }
        private void ActionFindNewAttackTarget()
        {

        }
        private void ActionFire()
        {

        }

        private const float MAX_ANGLE = 45.0f;

        private static float ComputeAliginTorqueNormalized(Vector3 targetPosition, Transform ship)
        {
            Vector2 localTargetPosition = ship.InverseTransformPoint(targetPosition);

            float angle = Vector3.SignedAngle(localTargetPosition, Vector3.up, Vector3.forward);

            angle = Mathf.Clamp(angle, -MAX_ANGLE, MAX_ANGLE) / MAX_ANGLE;

            return -angle;
        }
        #endregion
    }
}


