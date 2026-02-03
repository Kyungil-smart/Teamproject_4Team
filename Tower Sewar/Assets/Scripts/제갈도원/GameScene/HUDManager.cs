using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour
{
    [Header("Left Area")] [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI monsterCountText;

    [Header("Center Area")] [SerializeField]
    private TextMeshProUGUI waveText;

    [SerializeField] private TextMeshProUGUI timeText;

    [Header("Right Area")] [SerializeField]
    private GameObject stopPanel;

    [Header("Victory Panel")] [SerializeField]
    private GameObject victoryPanel;

    private bool isVictory = false;

    [Header("Defeat Panel")] [SerializeField]
    private GameObject defeatPanel;

    private bool isDefeated = false;

    private bool isPaused = false;
    
    [SerializeField] private ControlStateManager controlStateManager;

    private void Awake()
    {
        controlStateManager = GetComponentInParent<ControlStateManager>();
    }
    
    private void Start()
    {
        // StopPanel 숨기기
        if (stopPanel != null)
            stopPanel.SetActive(false);

        // VictoryPanel 숨기기
        if (victoryPanel != null)
            victoryPanel.SetActive(false);

        // DefeatPanel 숨기기
        if (defeatPanel != null)
            defeatPanel.SetActive(false);

        isVictory = false;
        isDefeated = false;
    }

    private void LateUpdate()
    {
        if (WaveManager._instance == null) return;

        // Wave
        waveText.text = $"Wave : {WaveManager._instance.Wave + 1}";

        // Time
        float time = WaveManager._instance.WaveTimer;
        int minutes = (int)(time / 60);
        int seconds = (int)(time % 60);
        timeText.text = $"{minutes:00}:{seconds:00}";

        // MonsterCount
        try
        {
            monsterCountText.text = $"{WaveManager._instance.NumsOfMonsters}";
        }
        catch
        {
            monsterCountText.text = "0";
        }

        // Health & Gold
        healthText.text = $"x {DataManager.Instance.PlayerLife}";
        goldText.text = $": {DataManager.Instance.PlayerGold}";

        // 승리/패배 체크
        CheckDefeat();
    }

    // 현재 씬 재시작 (Stop, Defeat, Victory 모두 사용)
    public void RestartGame()
    {
        Time.timeScale = 1f;
        isDefeated = false;
        isVictory = false;
        isPaused = false;
        
        DataManager.Instance.InitLife();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    /// 타이틀 씬으로 이동 (Stop, Defeat, Victory 모두 사용)
    public void GoToTitle()
    {
        Time.timeScale = 1f;
        isDefeated = false;
        isVictory = false;
        isPaused = false;

        if (GameSceneManager.Instance != null)
        {
            GameSceneManager.Instance.LoadTitle();
        }
        else
        {
            Debug.LogError("GameSceneManager.Instance가 null입니다!");
        }
    }

    // StopPanel (일시정지) 관련
    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            if (stopPanel != null) stopPanel.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            Debug.Log("게임 일시정지");
        }
        else
        {
            Time.timeScale = 1f;
            if (stopPanel != null) stopPanel.SetActive(false);


            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            Debug.Log("게임 재개");
        }
    }


    /// ResumeButton (재개 버튼)에서 호출
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;

        if (stopPanel != null) stopPanel.SetActive(false);

        controlStateManager.SetState(ControlStateManager.ControlState.GamePlay);
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Debug.Log("게임 재개");
    }

    // 승리 패널 표시 (WaveManager에서 호출)
    public void ShowVictoryPanel()
    {
        if (isDefeated) return; // 이미 패배했으면 승리 안됨

        isVictory = true;

        Time.timeScale = 0f;

        if (victoryPanel != null)
            victoryPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Debug.Log("승리!");
        Stage_Sound_Manager.instance?.SettingSound("Clear");
    }

    // Defeat (패배) 관련
    private void CheckDefeat()
    {
        if (isDefeated || isVictory) return;

        if (DataManager.Instance.PlayerLife <= 0)
        {
            Base_Sound_Manager.instance.BaseSFX("Destroy");
            ShowDefeatPanel();
        }
    }

    private void ShowDefeatPanel()
    {
        isDefeated = true;

        Time.timeScale = 0f;

        if (defeatPanel != null)
            defeatPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Debug.Log("패배!");
        Stage_Sound_Manager.instance?.SettingSound("Fail");
        
    }
}
