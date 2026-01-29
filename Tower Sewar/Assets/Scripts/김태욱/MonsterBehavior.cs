using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterBehavior : MonoBehaviour
{
    //몬스터 data 참조
    [SerializeField]
    MonsterData _monsterData;

    //맵의 wayPoint 참조
    [SerializeField]
    WayPoint _wayPoint;

    //몬스터 현재체력
    float _hp;
    //몬스터 현재속도
    float _velocity;
    //몬스터 드랍골드
    int _dropGold;
    //몬스터 이동경로
    List<Vector3> _pathPoints;
    //몬스터 현재 pathpoint index
    int _pathIndex;
    //몬스터가 죽었는지 
    public bool IsDead
    {
        get {
            if(_hp > 0) return false;
            return true;
        }
    }

    private void Awake()
    {
        Init();
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //이동처리
        Move();

        //죽음처리
        if (IsDead)
        {
            //골드지급!!!!!!!!!!!!!!!!!!!!!!!!!!
            // _dropGold
            MonsterSpawner.Instance.RemoveMonster(gameObject);
            Destroy(gameObject);
        }
    }

    //몬스터 초기화
    void Init()
    {
        if (_monsterData != null)
        {
            _hp = _monsterData.Hp;
            _velocity = _monsterData.MoveSpeed;
            _dropGold = _monsterData.DropGold;
        }
        //TODO: Maps[0] -> Maps[현재맵]으로 변경해야한다.
        if (_wayPoint != null)
        {
            //경로설정
            _pathPoints = _wayPoint.Maps[0].PathPoints;
            _pathIndex = 0;
            //바라보는방향설정
            Vector3 dir = _pathPoints[_pathIndex] - transform.position;
            transform.forward = dir.normalized;
            //초기위치 설정
            transform.position = _pathPoints[0];
        }

        //애니메이션 시작시간 랜덤
        Animator animator = GetComponent<Animator>();
        float rand = Random.Range(0f, 1f);
        animator.Play(0, 0, rand);
    }

    //몬스터 이동
    void Move()
    {
        if (_pathPoints == null) return;
        if (IsDead) return;

        //살아서 끝까지 도착했으면 도착에 따른 처리
        if(_pathIndex >= _pathPoints.Count)
        {
            //TODO: 도착처리
            //Debug.Log("도착!!!!!!!!!!!!!!!!!!");
            // player체력을 깎아야함!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            MonsterSpawner.Instance.RemoveMonster(gameObject);
            Destroy(gameObject);

            return;
        }
        //Debug.Log($"현재위치 x: {transform.position.x} y: {transform.position.y} z: {transform.position.z}");
        //Debug.Log($"목표위치 x: {_pathPoints[_pathIndex].x} y: {_pathPoints[_pathIndex].y} z: {_pathPoints[_pathIndex].z}");
        //Debug.Log($"이동거리 {_velocity * Time.deltaTime}");
        transform.position = Vector3.MoveTowards(transform.position, _pathPoints[_pathIndex], _velocity * Time.deltaTime);

        //목적지에 도착했으면 목표지점을 다음 목적지로 변경
        if (transform.position == _pathPoints[_pathIndex])
        {
            _pathIndex++;

            //rotation도 바꿔줌
            if (_pathIndex >= _pathPoints.Count) return;
            Vector3 dir = _pathPoints[_pathIndex] - transform.position;
            transform.forward = dir.normalized;
        }

    }

    //몬스터에게 데미지를 입힘
    public void Damage(float damage)
    {
        if(IsDead) return;
        _hp -= damage;
    }


    //내일 하자.. Die 메서드로 애니메이션이랑 죽음처리..
    IEnumerator Die()
    {
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("Die");

        yield return null;

        float dieTime = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(dieTime);

        MonsterSpawner.Instance.RemoveMonster(gameObject);
        Destroy(gameObject);
    }
}
