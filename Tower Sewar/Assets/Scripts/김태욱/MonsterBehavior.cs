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

    //몬스터 종류
    string _monsterType;

    //몬스터 현재체력
    [SerializeField]
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

        transform.localScale = _monsterData.Scale;

        if (_monsterData != null)
        {
            _hp = _monsterData.Hp;
            _velocity = _monsterData.MoveSpeed;
            _dropGold = _monsterData.DropGold;
            _monsterType = _monsterData.Name;
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
            //Debug.Log("도착!!!!!!!!!!!!!!!!!!");
            // player체력을 깎아야함!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            //끝까지 살아남은 몬스터는 죽는모션 없이 일단은 바로 없애는걸로 하자. 
            MonsterSpawner.Instance.RemoveMonster(gameObject);
            Destroy(gameObject);

            return;
        }
        
        //속도에따른 위치갱신
        transform.position = Vector3.MoveTowards(transform.position, _pathPoints[_pathIndex], _velocity * Time.deltaTime);

        //목적지에 도착했으면 목표지점을 다음 목적지로 변경
        if (Vector3.Distance(transform.position, _pathPoints[_pathIndex]) <= 0.05f)
        {
            _pathIndex++;

            //rotation도 바꿔줌
            if (_pathIndex >= _pathPoints.Count) return;
            Vector3 dir = _pathPoints[_pathIndex] - transform.position;
            transform.forward = dir.normalized;
        }

        

    }

    //몬스터에게 데미지를 입힘
    public void TakeDamage(float damage)
    {
        if(IsDead)
        {
            Debug.Log($"이미 죽었어용~! 체력 : {_hp}");
            return;
        }
        _hp -= damage;
    }

    //바로 destroy후 애니메이션 연출용 객체생성
    void Die()
    {
        //애니메이션 객체 생성
        if (_monsterType == "박쥐")
            MonsterSpawner.Instance.DieAnimationBat(transform.localScale, transform.position, transform.forward);
        else if (_monsterType == "토끼")
            MonsterSpawner.Instance.DieAnimationRabbit(transform.localScale, transform.position, transform.forward);
        else if (_monsterType == "유령")
            MonsterSpawner.Instance.DieAnimationGhost(transform.localScale, transform.position, transform.forward);
        else if (_monsterType == "슬라임")
            MonsterSpawner.Instance.DieAnimationSlime(transform.localScale, transform.position, transform.forward);

        //몬스터 destroy
        MonsterSpawner.Instance.RemoveMonster(gameObject);
        Destroy(gameObject);

    }

    //Die()호출을 하면 실행되는 루틴(방식변경으로 현재 안씀)
    IEnumerator DieRoutine()
    {

        //Die 트리거를 설정하고
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("Die");

        //한프레임 기다림(애니메이션 전환)
        yield return null;

        //죽는 애니메이션시간 가져오고 그만큼 기다림
        float dieTime = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(dieTime);

        //제거
        MonsterSpawner.Instance.RemoveMonster(gameObject);
        Destroy(gameObject);
    }
}
