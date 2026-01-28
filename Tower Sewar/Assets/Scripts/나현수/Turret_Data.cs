using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TowerLevelData
{
    public int upCost;
    public float damage;
    public float attackSpeed;
    public float range;
}

[CreateAssetMenu(fileName = "New Tower Data", menuName = "Scriptable Object/Tower Data")]
public class GunTowerData : ScriptableObject
{
    public string towerName;
    public int buildCost;
    public List<TowerLevelData> levelStats;
}