using UnityEngine;

namespace AS
{
    public class ShotHandler : MonoBehaviour
    {
        [SerializeField] private GameObject _projectile;
        [SerializeField] private float _projectileSpeed = 35;

        private TargetLockOn _targetLockOn;
        private CombatHandler _combatHandler;

        private Vector3 _target;

        private void Start()
        {
            _targetLockOn = GetComponentInParent<TargetLockOn>();
            _combatHandler = FindObjectOfType<CombatHandler>();
        }

        public void Shot()
        {
            if (GetComponentInParent<PlayerStats>())
            {
                if (_targetLockOn.currentEnemy) _target = _targetLockOn.currentEnemy.transform.position;
            }

            if (GetComponentInParent<EnemyStats>())
            {
                _target = _combatHandler._currentAIUnitTarget.transform.position;
            }

            Vector3 dir = _target - transform.position;
            dir.Normalize();
            dir.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(dir);
            transform.rotation = targetRotation;

            GameObject shell = Instantiate(_projectile, transform.position, transform.rotation);
            shell.GetComponent<Rigidbody>().AddForce(transform.forward * _projectileSpeed, ForceMode.Impulse);
        }
    }
}