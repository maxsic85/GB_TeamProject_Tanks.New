using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Skills :ScriptableObject
{
    [SerializeField] private List<SkillData> skillDatas;

    public List<SkillData> SkillDatas { get => skillDatas; set => skillDatas = value; }
}
