using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AS
{
    [CreateAssetMenu]
    public class TransformsData : ScriptableObject
    {
       [SerializeField] private Transform transform;

        public Transform Transform { get => transform; set => transform = value; }
    }
}
