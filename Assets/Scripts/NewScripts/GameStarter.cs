using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AS
{
    public class GameStarter : MonoBehaviour
    {

       public RoundData roundData;
       public Canvas healthBarCanvas;
        Controllers _controllers;



        void Awake()
        {
            ServiceLocator.SetService<GameStarter>(this);
            new GameInitialisation(roundData,healthBarCanvas);
        }

        private void Update()
        {
            

        }


    }
}
