using UnityEngine;

namespace SpaceShooter
{
    public class Projectile : Entity
    {
        #region Properties
        private enum ProjectileType
        {
            Basic,
            Rocket,
            Plasma
        }

        [SerializeField] SelfDestruction[] _explosionPrefabs;
        [SerializeField] private float _speed;
        [SerializeField] private float _lifeTime;
        [SerializeField] private ProjectileType _projectileType;
        [SerializeField] private int _damage;

        [SerializeField] private float _splashRange;

        [SerializeField] private float _searchRange;

        private Destructible _parent;
        private Destructible _target;

        private RaycastHit2D hit;
        private Collider2D aimTarget;

        #endregion

        #region UnityEvents

        private void Start()
        {
            _parent = transform.GetComponentInParent<Destructible>();

            transform.parent = null;

            Destroy(gameObject, _lifeTime);
        }


        private void Update()
        {
            if (aimTarget == null)
                aimTarget = Physics2D.OverlapCircle(transform.position, _searchRange);

            hit = Physics2D.Raycast(transform.position, transform.up, _speed * Time.deltaTime);

            if (aimTarget != null && _target == null && aimTarget.GetComponent<Destructible>() != _parent)
            {
                _target = aimTarget.GetComponentInParent<Destructible>();
            }

            if (_target == _parent)
                _target = null;

            if (_target == null)
            {
                transform.position += _speed * Time.deltaTime * transform.up;
            }

            if (_target != null)
            {
                TargetLocked();
            }

            if (hit)
            {
                //_target = null;

                Destructible destructible = hit.collider.transform.parent.GetComponent<Destructible>();

                if (destructible != null && destructible != _parent)
                {
                    if (_projectileType == ProjectileType.Rocket || _projectileType == ProjectileType.Basic)
                    {
                        if (_projectileType == ProjectileType.Rocket)
                        {
                            Instantiate(_explosionPrefabs[0], transform.position, Quaternion.identity);
                        }

                        if (destructible != Player.Instance.ActiveShip)
                        {
                            Player.Instance.ChangeScore(destructible.ScoreValue * _damage / 10);
                        }

                        destructible.ApplyDamage(_damage);

                        if (destructible.HitPoints <= 0)
                            Player.Instance.AddKill();
                    }

                    if (_projectileType == ProjectileType.Plasma && _splashRange > 0)
                    {
                        var hitColliders = Physics2D.OverlapCircleAll(transform.position, _splashRange);
                        foreach (var hitCollider in hitColliders)
                        {
                            var destructibleInArea = hitCollider.GetComponentInParent<Destructible>();
                            if (destructibleInArea != null)
                            {
                                if (destructibleInArea != Player.Instance.ActiveShip)
                                {
                                    Player.Instance.ChangeScore(destructibleInArea.ScoreValue * _damage / 10);
                                }

                                destructibleInArea.ApplyDamage(_damage);
                                Instantiate(_explosionPrefabs[1], transform.position, Quaternion.identity);

                                if (destructibleInArea.HitPoints <= 0)
                                    Player.Instance.AddKill();
                            }
                        }
                    }
                }

                OnProjectileDestruction(hit.collider, hit.point);
            }
        }

        #endregion  

        #region Private API

        private void OnProjectileDestruction(Collider2D collider, Vector2 position)
        {
            Destroy(gameObject);
        }

        private void TargetLocked()
        {
            if (_target)
                transform.position = Vector2.MoveTowards(transform.position, _target.transform.position, _speed * Time.deltaTime);
        }
        #endregion
    }
}

