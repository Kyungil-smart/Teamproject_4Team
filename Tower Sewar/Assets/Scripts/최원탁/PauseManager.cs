using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private ControlStateManager controlStateManager;
    private TileRaycaster tileRaycaster;

    
    // 현재 게임이 일시정지 상태인지 외부에서도 알 수 있게 프로퍼티로 선언되었습니다.
    public bool IsPaused { get; private set; }

    private void Awake()
    {
        controlStateManager = FindObjectOfType<ControlStateManager>();
        tileRaycaster = FindObjectOfType<TileRaycaster>();

    }

    public void TogglePause()
    {
        // [수정] 이 로그가 콘솔창에 찍히는지 꼭 확인하세요!
        Debug.Log($"<color=orange>현재 정지 상태: {IsPaused} -> 변경 시도</color>");

        if (IsPaused) ResumeGame();
        else PauseGame();
    }


    // [기존 코드 보완] 게임을 일시정지하는 로직
    private void PauseGame()
    {
        Debug.Log("⏸ 게임 정지");
        Time.timeScale = 0f;
        IsPaused = true;

        if (tileRaycaster != null) tileRaycaster.enabled = false;
        if (controlStateManager != null) controlStateManager.SetState(ControlStateManager.ControlState.PauseMenu);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void ResumeGame()
    {
        Debug.Log("▶ 게임 재개");
        Time.timeScale = 1f;
        IsPaused = false;

        if (tileRaycaster != null) tileRaycaster.enabled = true;
        if (controlStateManager != null) controlStateManager.SetState(ControlStateManager.ControlState.GamePlay);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}