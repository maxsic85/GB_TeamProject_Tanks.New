using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AS
{
    [CreateAssetMenu]
    public class TransformsData : ScriptableObject
    {
       [SerializeField] private Transform transform;
        [SerializeField] private Transform transformPlayer2;
        public Transform Transform { get => transform; set => transform = value; }
        public Transform TransformPlayer2 { get => transformPlayer2; set => transformPlayer2 = value; }
    }
}
