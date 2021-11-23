using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AS
{
    public class GameService : MonoBehaviour
    {
        public RoundData roundData;
        public Canvas healthBarCanvas;
     
        void Awake()
        {
            ServiceLocatorMonoBehavior.GetService<GameService>();
            EnemyTanksFabriOnStart();
            PlayerFabricOnStart();
        }
        private void PlayerFabricOnStart()
        {
            var player = FindObjectOfType<PlayerStats>();
            if (player == null)
            {             
                    var playerTank = PlayerStats.CreatePlayer(roundData.TransformForInstantiatePlayer.Transform, healthBarCanvas.transform);   
            }
        }
        private void EnemyTanksFabriOnStart()
        {
            var tank = FindObjectOfType<EnemyStats>();
            if (tank == null)
            {
                for (int i = 0; i < roundData.EnemyCntOnStart; i++)
                {
                    var enemy = EnemyStats.CreateTank(roundData.TransformForInstantiateEnemy[i].Transform, healthBarCanvas.transform);

                }
            }
        }
    }
}
