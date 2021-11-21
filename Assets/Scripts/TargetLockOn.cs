using UnityEngine;

namespace AS
{
    public class TargetLockOn : MonoBehaviour
    {
        public Transform currentEnemy;
        public GameObject ParticleFX;
        private GameObject Highlight;
        int ignoreLayers = 1 << 3;

        public void ChooseTarget()
        {
            ClearTarget();
            RaycastHit hit;
            Ray ray = FindObjectOfType<Camera>().ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ignoreLayers))
            {
                EnemyStats enemyStats = hit.transform.GetComponent<EnemyStats>();
                if (!enemyStats.IsDead)
                {
                    currentEnemy = hit.transform;
                    Highlight = Instantiate(ParticleFX, currentEnemy.position, currentEnemy.rotation);
                }
            }
        }
        public void ClearTarget()
        {
            currentEnemy = null;
            Destroy(Highlight);
            
            //Debug.Log($"currentEnemy = {currentEnemy}",this);
        }
    }
}
