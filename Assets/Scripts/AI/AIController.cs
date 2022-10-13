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
            Patrol,
            FixedPatrol
        }

        [SerializeField] private AIBehaviour _AIBehaviour;

        [SerializeField] private AIPointPatrol _patrolPoint;
        [SerializeField] private int _amountOfPoints;

        [Range(0f, 1f)]
        [SerializeField] private float _navigatorLinear;

        [Range(0f, 1f)]
        [SerializeField] private float _navigatorAngular;

        [SerializeField] private float _randomSelectMovePointTime;

        [SerializeField] private float _findNewTargetTime;

        [SerializeField] private float _shootDelay;

        [SerializeField] private float _precisionCoefficient;

        [SerializeField] private float _evadeRayLenght;

        private SpaceShip _spaceShip;

        Vector2[] _patrolPoints;
        private Vector3 _movePosition;

        private Destructible _selectedTarget;

        private int _currentPoint;

        private Timer _randomizeDirectionTimer;
        private Timer _shootDelayTimer;
        private Timer _findNewTargetTimer;

        #endregion

        #region Unity Events

        private void Start()
        {
            _spaceShip = GetComponent<SpaceShip>();

            if (_patrolPoint == null)
            {
                _patrolPoint = FindObjectOfType<AIPointPatrol>();
            }

            InitTimers();

            if (_AIBehaviour == AIBehaviour.FixedPatrol)
                InitiateFixedPatrolPoints();
        }

        private void Update()
        {
            if (_patrolPoint != null)
            {
                UpdateTimers();
                UpdateAI();
            }
        }

        #endregion

        #region Public API

        public void SetPatrolBehaviour(AIPointPatrol point)
        {
            //_AIBehaviour = AIBehaviour.Patrol;
            _patrolPoint = point;
        }


        #endregion

        #region Private API

        private void UpdateAI()
        {
            if (_AIBehaviour != AIBehaviour.Null)
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
            _shootDelayTimer = new Timer(_shootDelay);
            _findNewTargetTimer = new Timer(_findNewTargetTime);
        }

        private void UpdateTimers()
        {
            _randomizeDirectionTimer.TimeUpdater(Time.deltaTime);
            _shootDelayTimer.TimeUpdater(Time.deltaTime);
            _findNewTargetTimer.TimeUpdater(Time.deltaTime);
        }

        #endregion

        private void ActionFindNewMovePosition()
        {
            if (_AIBehaviour != AIBehaviour.Null)
            {
                if (_selectedTarget != null)
                {
                    _movePosition = _selectedTarget.transform.position + _selectedTarget.transform.up * _precisionCoefficient;
                }
                else
                {
                    if (_patrolPoint != null)
                    {
                        bool isInsidePatrolZone = (_patrolPoint.transform.position - transform.position).sqrMagnitude < _patrolPoint.Radius * _patrolPoint.Radius;

                        if (isInsidePatrolZone == true)
                        {
                            if (_AIBehaviour == AIBehaviour.Patrol)
                            {
                                if (_randomizeDirectionTimer.IsFinished == true)
                                {
                                    Vector2 newPoint = Random.onUnitSphere * _patrolPoint.Radius + _patrolPoint.transform.position;
                                    _movePosition = newPoint;

                                    _randomizeDirectionTimer.Start(_randomSelectMovePointTime);
                                }
                            }

                            if (_AIBehaviour == AIBehaviour.FixedPatrol)
                            {
                                _movePosition = _patrolPoints[_currentPoint];

                                if (_randomizeDirectionTimer.IsFinished == true || transform.position == _movePosition)
                                {
                                    _currentPoint++;
                                    _randomizeDirectionTimer.Start(_randomSelectMovePointTime);
                                }

                                if (_currentPoint == _amountOfPoints)
                                {
                                    _currentPoint = 0;
                                }
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
            if (_findNewTargetTimer.IsFinished == true)
            {
                _selectedTarget = FindNearestDestructibleTarget();

                _findNewTargetTimer.Start(_shootDelay);
            }
        }
        private void ActionFire()
        {
            if (_selectedTarget != null)
            {
                if (_shootDelayTimer.IsFinished == true)
                {
                    _spaceShip.Fire(TurretMode.Primary);

                    _shootDelayTimer.Start(_shootDelay);
                }
            }
        }

        private void InitiateFixedPatrolPoints()
        {
            _currentPoint = 0;
            _patrolPoints = new Vector2[_amountOfPoints];

            for (int i = 0; i < _amountOfPoints; i++)
            {
                _patrolPoints[i] = Random.onUnitSphere * _patrolPoint.Radius + _patrolPoint.transform.position;
            }

            _movePosition = _patrolPoints[_currentPoint];
        }

        private Destructible FindNearestDestructibleTarget()
        {
            float maxDist = float.MaxValue;

            Destructible potentialTarget = null;

            foreach (var v in Destructible.AllDestructibles)
            {
                if (v.GetComponent<SpaceShip>() == _spaceShip) continue;

                if (v.TeamID == Destructible.TeamIdNeutral) continue;

                if (v.TeamID == _spaceShip.TeamID) continue;

                float dist = Vector2.Distance(_spaceShip.transform.position, v.transform.position);

                if (dist < maxDist)
                {
                    maxDist = dist;
                    potentialTarget = v;
                }
            }

            return potentialTarget;
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


