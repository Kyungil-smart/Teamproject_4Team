using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public static MonsterSpawner Instance;

    //소환된 몬스터 관리를 위한 배열
    private List<GameObject> _monsterList;

    //몬스터 프리팹
    [SerializeField]
    MonsterPrefabList _monsterPrefabList;

    public int MonsterCount
    {
        get { return _monsterList.Count; }
    }

    private void Awake()
    {
        Instance = this;
        _monsterList = new List<GameObject>();
    }


    //Monster에서 destroy하기전 이 메서드를 호출하고 destroy한다.
    public void RemoveMonster(GameObject monster)
    {
        _monsterList.Remove(monster);
    }

    //일단은 몬스터마다 스폰메서드 만드는걸로.

    public void SpawnBat()
    {
        _monsterList.Add(Instantiate(_monsterPrefabList.List[0]));
    }
    public void SpawnGhost()
    {
        _monsterList.Add(Instantiate(_monsterPrefabList.List[4]));
    }
    public void SpawnRabbit()
    {
        _monsterList.Add(Instantiate(_monsterPrefabList.List[8]));
    }
    public void SpawnSlime()
    {
        _monsterList.Add(Instantiate(_monsterPrefabList.List[12]));
    }
}
