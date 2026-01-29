using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterPrefabList", menuName = "Scriptable Object/MonsterPrefabList", order = 0)]
public class MonsterPrefabList : ScriptableObject
{
    [SerializeField]
    GameObject[] _list;
    public GameObject[] List {  get { return _list; } }
}
