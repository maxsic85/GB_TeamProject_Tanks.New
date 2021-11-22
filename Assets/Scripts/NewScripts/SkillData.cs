using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class SkillData : ScriptableObject
{
    [SerializeField] string _name="skill";
    [SerializeField] Sprite _image ;
    [SerializeField] float  _cooldown;
    [SerializeField] bool _isEnable=true;
    [SerializeField] SkillType _skillType;

    public string Name { get => _name; set => _name = value; }
    public Sprite Image { get => _image; set => _image = value; }
    public float Cooldown { get => _cooldown; set => _cooldown = value; }
    public SkillType SkillType { get => _skillType; set => _skillType = value; }
    public bool IsEnable { get => _isEnable; set => _isEnable = value; }
}
