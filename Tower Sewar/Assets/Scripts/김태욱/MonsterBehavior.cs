using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterBehavior : MonoBehaviour
{
    //몬스터 data 참조
    [SerializeField]
    MonsterData _ghostData;

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
        Move();
    }

    //몬스터 초기화
    void Init()
    {
        _hp = _ghostData.Hp;
        _velocity = _ghostData.MoveSpeed;
        _dropGold = _ghostData.DropGold;
        //TODO: Maps[0] -> Maps[현재맵]으로 변경해야한다.
        _pathPoints = _wayPoint.Maps[0].PathPoints;
        _pathIndex = 0;
    }

    //몬스터 이동
    void Move()
    {
        if (_pathPoints == null) return;


        //끝까지 도착했으면 도착에 따른 처리
        if(_pathIndex >= _pathPoints.Count)
        {
            //TODO: 도착처리
            //Debug.Log("도착!!!!!!!!!!!!!!!!!!");
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
            Vector3 dir = _pathPoints[_pathIndex] - transform.position;
            transform.forward = dir.normalized;
        }

    }
}
