using UnityEngine;

[CreateAssetMenu(fileName = "Gun Tower Data", menuName = "Scriptable Object/Tower/Gun Tower Data", order = 0)]
public class GunTowerData : ScriptableObject
{
    [Header("기본 생성 정보")]
    [SerializeField] private string _towerName;
    public string TowerName { get => _towerName; set => _towerName = value; }

    [SerializeField] private int _towerBuildCost;
    public int TowerBuildCost { get => _towerBuildCost; set => _towerBuildCost = value; }

    [Space(20)]
    [Header("타워 스탯")]
    [SerializeField] private int _towerUpCost;
    public int TowerUpCost { get => _towerUpCost; set => _towerUpCost = value; }

    [SerializeField] private float _towerAtt;
    public float TowerAtt { get => _towerAtt; set => _towerAtt = value; }

    [SerializeField] private float _towerAttDelay;
    public float TowerAttDelay { get => _towerAttDelay; set => _towerAttDelay = value; }

    [SerializeField] private float _towerRange;
    public float TowerRange { get => _towerRange; set => _towerRange = value; }
}