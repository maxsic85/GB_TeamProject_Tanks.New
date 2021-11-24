using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AS
{
    public class VictoryWindow : MonoBehaviour
    {

        public void NewRound()
        {
            SceneManager.LoadScene("SampleScene");
           
        }
    }
}
