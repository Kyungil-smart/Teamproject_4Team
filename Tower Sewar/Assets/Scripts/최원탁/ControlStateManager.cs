using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlStateManager : MonoBehaviour
{
    public enum ControlState
    {   
        GamePlay, //인게임
        TowerUI, // 타워설치 및 UI조작
        PauseMenu // ESC 메뉴?
    }

    // 현재 상태
    public ControlState CurrentState {  get; private set; }

    // 현재 상태를 필드변수로 만듬
    private ControlState _previousState;

    private void Start()
    {
        Debug.Log("ControlStateManager Start");
        SetState(ControlState.TowerUI);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            SetState(ControlState.GamePlay);

        if (Input.GetKeyDown(KeyCode.F2))
            SetState(ControlState.TowerUI);

        if (Input.GetKeyDown(KeyCode.Escape))
            SetState(ControlState.PauseMenu);
    }

    public void SetState(ControlState newState)
    {
        // 같은 상태로 설정하는경우 무시
        if (CurrentState == newState) return;

        // PauseMenu로 들어갈땐 이전상황 기억
        if (newState == ControlState.PauseMenu)
        {
            _previousState = CurrentState;
        }

        CurrentState = newState;
        ApplyState();
    }
    
    public void ReturnFromPause()
    {
        // pauseMenu에서 나올 때 이전 상태복귀
        SetState(_previousState);
    }

    private void ApplyState()
    {
        switch (CurrentState)
        {
            case ControlState.GamePlay:
                // 마우스 락 + 커서 숨김
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                // 게임 정상 진행
                Time.timeScale = 1f;
                break;

            case ControlState.TowerUI:
                // 마우스 언락 + 커서 표시
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                // 게임 정상진행
                Time.timeScale = 1f;
                break;

            case ControlState.PauseMenu:
                // 마우스 언락 + 커서 표시
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                // 게임 완전 정지
                Time.timeScale = 0f;
                break;
        }
        
    }
}
