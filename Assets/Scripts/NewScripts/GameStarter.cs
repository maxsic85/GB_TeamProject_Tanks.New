using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace AS
{
    public class GameStarter : MonoBehaviour
    {
        public TargetLockOn targetLockOn;
        [SerializeField] private CombatHandler combatHandler;
        public RoundData roundData;
        public UIdataRound uIdataRound;
        public Button button;
        public Canvas healthBarCanvas;
        Controllers _controllers;
        public ISavePlayerPosition savePlayerPosition;


        void Awake()
        {
            ServiceLocator.SetService<GameStarter>(this);
            savePlayerPosition = new SaveDataRep();
            _controllers = new Controllers();
          
            new GameInitialisation(_controllers, roundData, healthBarCanvas, targetLockOn, combatHandler, savePlayerPosition, button);
        }

        void Update()
        {
           
            _controllers.Execute(Time.deltaTime);

        }


    }
}
