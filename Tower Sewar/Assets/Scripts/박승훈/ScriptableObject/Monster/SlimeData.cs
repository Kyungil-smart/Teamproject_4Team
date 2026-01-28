using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Slime Data", menuName = "Scriptable Object/Monster/Slime Data", order = 0)]
public class SlimeData : ScriptableObject
{
    [Header("기본 생성 데이터")]
    [SerializeField] private string _name;
    public string Name => _name;
    
    [Space(20)]
    [SerializeField] private float _hp;
    public float Hp => _hp;
    [SerializeField] private float _moveSpeed;
    public float MoveSpeed => _moveSpeed;
    [SerializeField] private int _dropGold;
    public int DropGold => _dropGold;
}
