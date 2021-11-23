using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AS
{
    public class GameInitialisation 
    {
        public GameInitialisation(RoundData roundData, Canvas healthBarCanvas)
        {
            IcreteEnemyForRound Fabric = new GameFabricEnemy(roundData,healthBarCanvas);


        }
    }
}
