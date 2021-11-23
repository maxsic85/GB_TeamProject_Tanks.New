using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AS
{
    public class GameFabricEnemy :IcreteEnemyForRound
    {
        public RoundData _roundData;
        private Canvas _healthBarCanvas;
     


       public GameFabricEnemy(RoundData roundData, Canvas healthBarCanvas)
        {
            //ServiceLocatorMonoBehavior.GetService<GameService>();
          
            _roundData = roundData;
            _healthBarCanvas = healthBarCanvas;
            EnemyTanksFabriOnStart();
            PlayerFabricOnStart();
        }
        public void PlayerFabricOnStart()
        {
            //var player = FindObjectOfType<PlayerStats>();
            //if (player == null)
            //{             
                    var playerTank = PlayerStats.CreatePlayer(_roundData.TransformForInstantiatePlayer.Transform, _healthBarCanvas.transform);   
           // }
        }
        public void EnemyTanksFabriOnStart()
        {
            //var tank = FindObjectOfType<EnemyStats>();
            //if (tank == null)
            //{
                for (int i = 0; i < _roundData.EnemyCntOnStart; i++)
                {
                    var enemy = EnemyStats.CreateTank(_roundData.TransformForInstantiateEnemy[i].Transform,_healthBarCanvas.transform);

                }
          // }
        }
    }
}
