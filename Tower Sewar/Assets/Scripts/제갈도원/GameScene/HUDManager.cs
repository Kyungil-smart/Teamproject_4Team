using System;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [Header("Left Area")]
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI monsterCountText;

    [Header("Center Area")]
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI timeText;
    
    [Header("Right Area")]
    [SerializeField] private GameObject stopPanel;
    
    private bool isPaused = false;

    private void Start()
    {
        // 시작할 때 StopPanel 숨기기
        if (stopPanel != null)
        {
            stopPanel.SetActive(false);
        }
    }

    // Stop 버튼에서 호출
    public void TogglePause()
    {
        isPaused = !isPaused;
        
        if (isPaused)
        {
            Time.timeScale = 0f;
            if (stopPanel != null) stopPanel.SetActive(true);
            Debug.Log("게임 일시정지");
        }
        else
        {
            Time.timeScale = 1f;
            if (stopPanel != null) stopPanel.SetActive(false);
            Debug.Log("게임 재개");
        }
    }

    // ResumeButton에서 호출 (Unity Event)
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        if (stopPanel != null) stopPanel.SetActive(false);
        Debug.Log("게임 재개");
    }

    // HomeButton에서 호출 (Unity Event)
    public void GoToTitle()
    {
        Time.timeScale = 1f;
        GameSceneManager.Instance.LoadTitle();
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

        // monsterCountText
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
    }
}