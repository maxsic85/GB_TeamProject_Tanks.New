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
            _targetLockOn = ServiceLocator.Resolve<GameStarter>().targetLockOn;
            _combatHandler = FindObjectOfType<CombatHandler>();
        }
        public void Shot(CharacterStats character, SkillData skillData)
        {
            switch (character.ShotType)
            {
                case ShotType.JustShot:
                    JustShot();
                    break;
                case ShotType.UseSkill:
                    UseSkill(character, skillData);
                    break;
                default:

                    break;
            }
        }
        private void UseSkill(CharacterStats character, SkillData skillData)
        {
            character.Skill.ExecuteSkill(skillData.SkillType);
            skillData.IsEnable = false;
            skillData.NumberRoundUse = ServiceLocator.Resolve<GameStarter>().roundData.RoundCount;
        }
        public void JustShot()
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

        public void RandomShot()
        {
            var targetPos = Vector3.zero;

            targetPos = _combatHandler.EnemyTeam[Random.Range(0, _combatHandler.EnemyTeam.Length)].transform.position;

            Vector3 dir = targetPos - transform.position;
            dir.Normalize();
            dir.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(dir);
            transform.rotation = targetRotation;

            GameObject shell = Instantiate(_projectile, transform.position, transform.rotation);
            shell.GetComponent<Rigidbody>().AddForce(transform.forward * _projectileSpeed, ForceMode.Impulse);

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