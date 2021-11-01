using UnityEngine;

namespace AS
{
    public class ShotHandler : MonoBehaviour
    {
        [SerializeField] private GameObject _projectile;
        [SerializeField] private float _projectileSpeed = 35;

        private TargetLockOn _targetLockOn;
        private CombatHandler _combatHandler;
        
        private void Start()
        {
            _targetLockOn = GetComponentInParent<TargetLockOn>();
            _combatHandler = FindObjectOfType<CombatHandler>();
        }

        public void Shot()
        {
            var targetPos = Vector3.zero;
            
            if (GetComponentInParent<PlayerStats>())
            {
                targetPos = _targetLockOn.currentEnemy.transform.position;
            }

            if (GetComponentInParent<EnemyStats>())
            {
                targetPos = _combatHandler._currentAIUnitTarget.transform.position;
            }

            Vector3 dir = targetPos - transform.position;
            dir.Normalize();
            dir.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(dir);
            transform.rotation = targetRotation;

            GameObject shell = Instantiate(_projectile, transform.position, transform.rotation);
            shell.GetComponent<Rigidbody>().AddForce(transform.forward * _projectileSpeed, ForceMode.Impulse);
        }
    }
}