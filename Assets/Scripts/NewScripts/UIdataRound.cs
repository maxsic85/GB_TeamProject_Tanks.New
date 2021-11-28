using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AS
{
    [CreateAssetMenu]
    public class UIdataRound : ScriptableObject
    {
        [SerializeField] private GameObject saveBtn;
        [SerializeField] private GameObject loadBtn;

        public GameObject SaveBtn { get => saveBtn; set => saveBtn = value; }
        public GameObject LoadBtn { get => loadBtn; set => loadBtn = value; }
    }
}
