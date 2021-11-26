using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class SkillData : ScriptableObject
{
    [SerializeField] string _name="skill";
    [SerializeField] GameObject _button;

    [SerializeField] Sprite _image ;
    [SerializeField] int  _cooldown;
    [SerializeField] int _useInRound;
    [SerializeField] int _delayRoundToActive;
    [SerializeField] bool _isEnable=true;
    [SerializeField] SkillType _skillType;

    public string Name { get => _name; set => _name = value; }
    public Sprite Image { get => _image; set => _image = value; }
    public int Cooldown { get => _cooldown; set => _cooldown = value; }
    public SkillType SkillType { get => _skillType; }
    public bool IsEnable { get => _isEnable; set => _isEnable = value; }
    public int UseInRound { get => _useInRound; set => _useInRound = value; }
    public int DelayRoundToActive { get => _delayRoundToActive; set => _delayRoundToActive = value; }
    public GameObject Button { get => _button; set => _button = value; }
}
