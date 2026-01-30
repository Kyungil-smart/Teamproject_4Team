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

    //몬스터 사망애니메이션 프리팹
    [SerializeField]
    MonsterPrefabList _monsterDieAnimationList;

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

    //죽는애니메이션 연출하는 객체 생성
    public void DieAnimationBat(Vector3 localScale, Vector3 position, Vector3 forward)
    {
        if (_monsterDieAnimationList.List[0] == null) return;
        GameObject o = Instantiate(_monsterDieAnimationList.List[0]);
        o.transform.localScale = localScale;
        o.transform.position = position;
        o.transform.forward = forward;
    }
    public void DieAnimationGhost(Vector3 localScale, Vector3 position, Vector3 forward)
    {
        if (_monsterDieAnimationList.List[4] == null) return;
        GameObject o = Instantiate(_monsterDieAnimationList.List[4]);
        o.transform.localScale = localScale;
        o.transform.position = position;
        o.transform.forward = forward;
    }
    public void DieAnimationRabbit(Vector3 localScale, Vector3 position, Vector3 forward)
    {
        if (_monsterDieAnimationList.List[8] == null) return;
        GameObject o = Instantiate(_monsterDieAnimationList.List[8]);
        o.transform.localScale = localScale;
        o.transform.position = position;
        o.transform.forward = forward;
    }
    public void DieAnimationSlime(Vector3 localScale, Vector3 position, Vector3 forward)
    {
        if (_monsterDieAnimationList.List[12] == null) return;
        GameObject o = Instantiate(_monsterDieAnimationList.List[12]);
        o.transform.localScale = localScale;
        o.transform.position = position;
        o.transform.forward = forward;
    }
}
