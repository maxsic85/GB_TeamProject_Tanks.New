using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AS
{
    public class DefeatWindow : MonoBehaviour
    {
        public void Restart()
        {
            SceneManager.LoadScene("SampleScene");

        }
    }
}
