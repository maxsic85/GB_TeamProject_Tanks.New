using UnityEngine;

namespace AS
{
    public class TankView : MonoBehaviour
    {
        
               
        [SerializeField] private Transform _healthTag;

        public Vector3 HealthBarPosition => _healthTag.position;
    }
}