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

        public void Shot(CharacterStats character)
        {
            switch (character.ShotType)
            {
                case ShotType.JustShot:
                    JustShot();
                    break;
                case ShotType.UseSkill:
                  
                       UseSkill(character);
                    break;
                default:
                    JustShot();
                    break;
            }

        }

        private void JustShot()
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

        private void UseSkill(CharacterStats character)
        {
            character.Skill.ExecuteSkill(character._SkillType);
        }


        public void ShotToAllEnemies()
        {
            var enemies = CombatHandler.Instance.EnemyTeam;
            foreach (var item in enemies)
            {
                var targetPos = Vector3.zero;
                targetPos = item.transform.position;
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
}