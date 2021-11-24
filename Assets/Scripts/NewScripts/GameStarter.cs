using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AS
{
    public class GameStarter : MonoBehaviour
    {
        public TargetLockOn targetLockOn;
        [SerializeField] private CombatHandler combatHandler;
        public RoundData roundData;
        public Canvas healthBarCanvas;
        Controllers _controllers;



        void Awake()
        {
            ServiceLocator.SetService<GameStarter>(this);
            _controllers = new Controllers();
            new GameInitialisation(_controllers, roundData, healthBarCanvas, targetLockOn, combatHandler);
        }

        void Update()
        {
           
            _controllers.Execute(Time.deltaTime);

        }


    }
}
