using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AS
{
    public class GameInitialisation 
    {
        public GameInitialisation(Controllers controllers,RoundData roundData, Canvas healthBarCanvas,TargetLockOn targetLockOn,CombatHandler combatHandler)
        {
            IcreteEnemyForRound Fabric = new GameFabricEnemy(roundData,healthBarCanvas);
            var InputHandler = new InputHandler(targetLockOn,combatHandler);

            controllers.Add(InputHandler);

        }
    }
}
