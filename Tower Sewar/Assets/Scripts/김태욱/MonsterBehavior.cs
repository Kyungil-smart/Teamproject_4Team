using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehavior : MonoBehaviour
{
    [SerializeField]
    GhostData _ghostData;

    //몬스터 현재체력
    float _hp;
    //몬스터 현재속도
    float _velocity;
    //몬스터 드랍골드
    int _dropGold;

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
        
    }

    //몬스터 초기화
    void Init()
    {
        _hp = _ghostData.Hp;
        _velocity = _ghostData.MoveSpeed;
        _dropGold = _ghostData.DropGold;
    }
}
