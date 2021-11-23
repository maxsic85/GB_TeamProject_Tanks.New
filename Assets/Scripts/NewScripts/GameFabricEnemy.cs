using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AS
{
    public class GameFabricEnemy : IcreteEnemyForRound
    {
        public RoundData _roundData;
        private Canvas _healthBarCanvas;

        public GameFabricEnemy(RoundData roundData, Canvas healthBarCanvas)
        {
            _roundData = roundData;
            _healthBarCanvas = healthBarCanvas;
            EnemyTanksFabriOnStart();
            PlayerFabricOnStart();
        }
        public void PlayerFabricOnStart()
        {
            var playerTank = PlayerStats.CreatePlayer(_roundData.TransformForInstantiatePlayer.Transform, _healthBarCanvas.transform);
        }
        public void EnemyTanksFabriOnStart()
        {
            for (int i = 0; i < _roundData.EnemyCntOnStart; i++)
            {
                var enemy = EnemyStats.CreateTank(_roundData.TransformForInstantiateEnemy[i].Transform, _healthBarCanvas.transform);

            }
        }
    }
}
