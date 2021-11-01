using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AS
{
    public class ShotHandler : MonoBehaviour
    {
        [SerializeField]
        private GameObject _projectile;        
        private float projectileSpeed = 35;

        TargetLockOn targetLockOn;
        CombatHandler combatHandler;

        Vector3 target;
        private void Start()
        {
            targetLockOn = GetComponentInParent<TargetLockOn>();
            combatHandler = FindObjectOfType<CombatHandler>();
        }
        public void Shot()
        {
            if (GetComponentInParent<PlayerStats>())
            {
                target = targetLockOn.currentEnemy.transform.position;
            }
            if (GetComponentInParent<EnemyStats>())
            {
                target = combatHandler.currentAIUnitTarget.transform.position;                 
            }
            Vector3 dir = target - transform.position;
            dir.Normalize();
            dir.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(dir);
            transform.rotation = targetRotation;

            GameObject shell = Instantiate(_projectile, transform.position, transform.rotation);            
            shell.GetComponent<Rigidbody>().AddForce(transform.forward * projectileSpeed, ForceMode.Impulse);
        }
    }
}
