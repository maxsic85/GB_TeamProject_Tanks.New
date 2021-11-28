using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AS
{
    public class GameInitialisation 
    {
        public GameInitialisation(Controllers controllers,RoundData roundData, Canvas healthBarCanvas,TargetLockOn targetLockOn,CombatHandler combatHandler,ISavePlayerPosition savePlayerPosition,Button uIdataRound)
        {
            IcreteEnemyForRound Fabric = new GameFabricEnemy(roundData,healthBarCanvas);
            var InputHandler = new InputHandler(targetLockOn,combatHandler,savePlayerPosition, uIdataRound);
            SkillState skillState = new SkillState();

            controllers.Add(InputHandler);

        }
    }
}
