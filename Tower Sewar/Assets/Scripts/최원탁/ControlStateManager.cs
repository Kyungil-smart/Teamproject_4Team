using UnityEngine;

// 플레이어와 카메라의 조작 가능 상태를 중앙에서 관리하는 클래스
public class ControlStateManager : MonoBehaviour
{
    // 현재 게임의 조작 상태를 정의하는 열거형
    public enum ControlState
    {
        GamePlay,   // 정상 게임 플레이 상태
        TowerUI,    // 타워 설치 UI가 열린 상태
        PauseMenu   // 일시정지 상태
    }

    // 현재 상태를 저장하는 변수
    private ControlState currentState;

    // 플레이어 이동이 가능한지 여부
    // PlayerMovement에서 이 값을 조회한다
    public bool CanMove { get; private set; }

    // 카메라 시점 회전이 가능한지 여부
    // CameraRigController에서 이 값을 조회한다
    public bool CanLook { get; private set; }

    private void Start()
    {
        // 씬이 시작되면 기본적으로 게임 플레이 상태로 설정한다
        SetState(ControlState.GamePlay);
    }

    // 외부(UI, 웨이브 시스템 등)에서 상태를 변경할 때 호출하는 함수
    public void SetState(ControlState newState)
    {
        // 현재 상태를 갱신한다
        currentState = newState;

        // 상태에 따라 조작 가능 여부를 설정한다
        switch (currentState)
        {
            case ControlState.GamePlay:
                // 게임 플레이 중에는 이동과 시점 회전이 모두 가능하다
                CanMove = true;
                CanLook = true;
                break;

            case ControlState.TowerUI:
                // 타워 UI가 열려 있을 때는 이동은 막고
                // 카메라 회전은 허용한다 (쿼터뷰 시점 확인용)
                CanMove = false;
                CanLook = true;
                break;

            case ControlState.PauseMenu:
                // 일시정지 상태에서는 모든 조작을 막는다
                CanMove = false;
                CanLook = false;
                break;
        }
    }
}
