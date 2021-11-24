using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AS
{
    public class ProgressChecker
    {

        public int Wins
        {
            get => Loses;
            private set
            {
                if (value < 3 && value >= 0)
                    Loses = value;
            }
        }

        public int Loses 
        {
            get => Loses;
            private set
            {
                if (value < 3 && value >= 0)
                    Loses = value;
            }
        }

        public ProgressChecker()
        {
            Reset();
        }

        public void AddWin()
        {
            Wins ++;
            ChangeDifficulty();
        }

        public void AddLoose()
        {
            Loses++;
        }

        public void Reset()
        {
            Loses = 0;
            Wins = 0;
        }

        private void ChangeDifficulty()
        {
            
        }


    }
}
