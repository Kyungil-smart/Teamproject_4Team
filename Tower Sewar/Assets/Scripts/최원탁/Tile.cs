using UnityEngine;

// 타워를 설치할 수 있는 "타일" 하나를 표현하는 스크립트
public class Tile : MonoBehaviour
{
    [Header("타워 설치 가능 여부 (기본 속성)")]

    // 이 타일이 원래 타워 설치가 가능한 땅인가?
    [SerializeField] private bool isBuildable = true;

    [Header("현재 상태")]

    // 현재 이 타일에 이미 타워가 설치되어 있는가?
    [SerializeField] private bool isOccupied = false;

    // --------------------
    // 외부에서 읽기 전용으로 사용하는 프로퍼티
    // --------------------

    // 지금 이 타일에 타워를 설치할 수 있는지?
    public bool CanBuild
    {
        get
        {
            // 설치 가능한 땅이면서, 아직 점유되지 않았을 때만 true
            return isBuildable && !isOccupied;
        }
    }

    // --------------------
    // 상태 변경용 메서드 (나중에 타워 설치 시 사용)
    // --------------------

    // 타워가 설치되었음을 알리는 함수
    public void SetOccupied()
    {
        isOccupied = true;
    }

    // (필요하면) 타워가 제거되었음을 알리는 함수
    public void ClearOccupied()
    {
        isOccupied = false;
    }
}
