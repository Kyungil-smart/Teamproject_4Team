using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterBehavior : MonoBehaviour
{
    //몬스터 data 참조
    MonsterData _monsterData;

    //맵의 wayPoint 참조
    WayPoint _wayPoint;


    //몬스터 현재체력 //임시 serializeField로 해둠
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

    Transform _aimPoint;


    private void Awake()
    {
    }


    // Start is called before the first frame update
    void Start()
    {
        Init();

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
            // TODO : 골드 추가 했습니다. - 제갈도원 -
            DataManager.Instance.PlayerGold += _dropGold;
            Die();
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
        }

        if (_wayPoint != null)
        {
            //경로설정
            _pathPoints = _wayPoint.PathPoints;
            _pathIndex = 0;
            //바라보는방향설정
            Vector3 dir = _pathPoints[_pathIndex] - transform.position;
            transform.forward = dir.normalized;
            //초기위치 설정
            transform.position = _pathPoints[0];
            //Debug.Log($"transform x {transform.position.x:0.00} y{transform.position.y:0.00} z{transform.position.z:0.00}");

        }

        //애니메이션 시작시간 랜덤
        Animator animator = GetComponent<Animator>();
        float rand = Random.Range(0f, 1f);
        animator.Play(0, 0, rand);

        SetAimPoint();

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
            // TODO : 플레이어 체력 감소 추가 -제갈도원-
            DataManager.Instance.PlayerLife -= 1;
            if(DataManager.Instance.PlayerLife <= 0)
            {
                // 체력 0이되면 타이틀 가는걸로.
                DataManager.Instance.Init();
                GameSceneManager.Instance.LoadTitle();
            }
            Die();

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

    //몬스터 객체는 사망시 애니메이션 연출용 객체생성후, 바로 destroy .
    void Die()
    {
        MonsterSpawner.Instance.DieAnimation(_monsterData, transform); //사망 애니메이션

        Enemy_Sound_Manager.instance?.PlaySfx(); // 몬스터 사망 사운드
        Enemy_VFX_Manager.instance?.Death(transform); //몬스터 죽음 VFX

        //몬스터 destroy
        MonsterSpawner.Instance.RemoveMonster(gameObject);
        Destroy(gameObject);

    }

    public void SetMonsterData(MonsterData data)
        { _monsterData = data; }
    public void SetWayPoint(WayPoint wayPoint)
        { _wayPoint = wayPoint; }

    //asdfasdf
    public Transform GetAimPoint()
    {
        return _aimPoint != null ? _aimPoint : transform;
    }

    void SetAimPoint()
    {
        if (_aimPoint == null)
        {
            GameObject ap = new GameObject("AimPoint");
            ap.transform.SetParent(transform);
            _aimPoint = ap.transform;
        }

        Collider col = GetComponentInChildren<Collider>();
        if (col != null)
        {
            Vector3 v = new Vector3(col.bounds.center.x * transform.localScale.x, col.bounds.center.y * transform.localScale.y, col.bounds.center.z * transform.localScale.z);
            _aimPoint.position = transform.position + v;
            //Debug.Log($"t x {_aimPoint.position.x:0.00} y{_aimPoint.position.y:0.00} z{_aimPoint.position.z:0.00}");
        }
    }
    
}
