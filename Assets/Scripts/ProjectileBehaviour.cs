using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AS
{
    public class ProjectileBehaviour : MonoBehaviour
    {
        [SerializeField]
        private int _damage = 15;
        public GameObject ImpactParticleFX;

        private float timer;
        private void LateUpdate()
        {
            timer = timer + Time.deltaTime;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player" && timer > 0.1)
            {
                GameObject temp = Instantiate(ImpactParticleFX, transform.position, transform.rotation);
                PlayerStats playerStats = other.GetComponent<PlayerStats>();
                playerStats.TakingDamage(_damage);
                playerStats.UpdatePlayerHealthSlider();
                Destroy(gameObject);
                if (playerStats.currentHealth <= 0)
                {
                    playerStats.currentHealth = 0;
                    playerStats.HandleDeath();
                }
            }
            else if (other.tag == "Enemy" && timer > 0.1)
            {
                GameObject temp = Instantiate(ImpactParticleFX, transform.position, transform.rotation);
                EnemyStats enemyStats = other.GetComponent<EnemyStats>();
                enemyStats.TakingDamage(_damage);
                enemyStats.UpdateEnemyHealthSlider();                
                Destroy(gameObject);
                {
                    if (enemyStats.currentHealth <= 0)
                    {
                        enemyStats.currentHealth = 0;
                        enemyStats.HandleDeath();
                    }
                }
            }
            else if (timer > 1)
            {
                GameObject temp = Instantiate(ImpactParticleFX, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}
