using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private ControlStateManager controlStateManager;
    private TileRaycaster tileRaycaster;

    [SerializeField] private GameObject pauseMenuPanel;

    // 현재 게임이 일시정지 상태인지 외부에서도 알 수 있게 프로퍼티로 선언되었습니다.
    public bool IsPaused { get; private set; }

    private void Awake()
    {
        controlStateManager = FindObjectOfType<ControlStateManager>();
        tileRaycaster = FindObjectOfType<TileRaycaster>();

        // [추가] 패널이 연결 안 되어 있으면 범인을 바로 찾을 수 있게 로그를 찍습니다.
        if (pauseMenuPanel == null)
            Debug.LogError("⚠️ PauseManager에 'PauseMenuPanel'이 연결되지 않았습니다! Inspector에서 드래그해서 넣어주세요.");
    }

    public void TogglePause()
    {
        // [수정] 이 로그가 콘솔창에 찍히는지 꼭 확인하세요!
        Debug.Log($"<color=orange>현재 정지 상태: {IsPaused} -> 변경 시도</color>");

        if (IsPaused) ResumeGame();
        else PauseGame();
    }

    public void TestClick()
    {
        Debug.Log("<color=lime>✅ 버튼 클릭 확인됨!</color>");
    }

    // [기존 코드 보완] 게임을 일시정지하는 로직
    private void PauseGame()
    {
        Debug.Log("⏸ 게임 정지");
        Time.timeScale = 0f;
        IsPaused = true;

        // [추가] 일시정지 화면을 켭니다.
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(true);

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

        // [추가] 일시정지 화면을 끕니다.
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(false);

        if (tileRaycaster != null) tileRaycaster.enabled = true;
        if (controlStateManager != null) controlStateManager.SetState(ControlStateManager.ControlState.GamePlay);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}