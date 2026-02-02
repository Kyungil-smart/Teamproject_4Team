using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public static MonsterSpawner Instance;

    //소환된 몬스터 관리를 위한 배열
    private List<GameObject> _monsterList;

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

    public void SpawnMonster(MonsterData data, WayPoint wayPoint)
    {
        if(data == null || wayPoint == null)
        {
            Debug.Log($"Spawn Monster Null 에러!");
            return;
        }

        GameObject o = Instantiate(data.MonsterPrefab);
        _monsterList.Add(o);
        MonsterBehavior m = o.GetComponent<MonsterBehavior>();
        m.SetMonsterData(data);
        m.SetWayPoint(wayPoint);
    }

    //죽는애니메이션 연출하는 객체 생성
    public void DieAnimation(MonsterData data,Transform t)
    {
        GameObject o = Instantiate(data.DeadMonsterPrefab);
        o.transform.localScale = t.localScale;
        o.transform.position = t.position;
        o.transform.forward = t.forward;
    }
}
