using System;
using EnemyLogic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Building
{
    public class Tower : MonoBehaviour, IUpgradable
    {
        private const int EnemyLayerMask = 1 << 9;

        [SerializeField, Range(3f, 10f)] 
        private float _attackRange = 3f;

        [SerializeField] 
        private Transform _turret = default;

        [SerializeField] 
        private LaserBeam _laser = default;

        [SerializeField] 
        private float _damagePerSecond = 10f;

        [SerializeField] 
        private ParticleSystem _prepare = default;
        
        [SerializeField] 
        private ParticleSystem _laserDamage = default;

        [SerializeField] 
        private float _timeForPrepare = 1f;

        private float _bonusDamagePerSecond;
        public BuildingDescription _description { get; private set; }

        public float ResultDamagePerSecond => 
            _damagePerSecond + _bonusDamagePerSecond;
        
        private float _timeElapsed;
        private TargetPoint _target;
        private readonly Collider[] _targetResults = new Collider[10];

        private Rebuilding _rebuilding;

        private void Awake()
        {
            _prepare.Stop();
            _laserDamage.Stop();

            _rebuilding = GetComponent<Rebuilding>();
            _description = GetComponent<BuildingDescription>();
            Price = _description.Description.UpgradePrice;
        }

        private void Update()
        {
            if (_rebuilding.Rebuild)
                return;
            
            if (_target != null)
            {
                _turret.LookAt(_target.Position);
                
                if (CanShoot())
                {
                    Shoot();
                    _laserDamage.transform.position = _target.Position;
                    _laserDamage.gameObject.SetActive(true);
                }
                else
                {
                    _laserDamage.gameObject.SetActive(false);
                    _timeElapsed += Time.deltaTime;
                }

                var distance = Vector3.Distance(transform.position, _target.Position);
                if (distance > _attackRange + 1f)
                    _target = null;
                
                return;
            }
            
            _laser.Deactivate();
            FindTarget();
        }

        private bool CanShoot() => 
            _timeElapsed >= _timeForPrepare;

        private void Shoot()
        {
            var point = _target.Position;
            _laser.Activate();

            var distance = Vector3.Distance(_turret.position, point);
            _laser.Shoot(new Vector3(0, 0, distance * 2));
            _target.Health.ApplyDamage(ResultDamagePerSecond * Time.deltaTime);
        }

        private void FindTarget()
        {
            var hits = Physics.OverlapSphereNonAlloc(transform.localPosition, _attackRange, _targetResults,
                EnemyLayerMask);

            if (hits > 0)
            {
                var target = _targetResults[Random.Range(0, hits)];
                _target = target.GetComponent<TargetPoint>();
                _timeElapsed = 0f;
                
                _prepare.Play();
                return;
            }

            _laserDamage.gameObject.SetActive(false);
            _target = null;
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            var position = transform.localPosition;
            position.y += 0.1f;
            Gizmos.DrawWireSphere(position, _attackRange);

            if (_target != null)
            {
                Gizmos.DrawLine(position, _target.Position);
            }
        }

        public void Upgrade()
        {
            _rebuilding.StartRebuild();
            var description = _description.Description;
            _bonusDamagePerSecond += description.UpgradeBonus;
            description.Value = ResultDamagePerSecond;
            description.Update();
        }

        public int Price { get; set; }
    }
}