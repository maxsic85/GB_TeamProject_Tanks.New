using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace AS
{
    public class TankController : MonoBehaviour
    {
        [SerializeField] private TankView _tankView;
        [FormerlySerializedAs("_enemyHealth")] [SerializeField] private HealthBar _health;

        private void Start()
        {
            SetHealthBarPosition();
        }

        private void SetHealthBarPosition()
        {
           _health.transform.position = _tankView.HealthBarPosition;
        }
    }
}