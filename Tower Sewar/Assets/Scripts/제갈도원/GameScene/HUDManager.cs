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
    [SerializeField] private Button stopButton;
    
    private bool isPaused = false;

    private void Start()
    {
        if (stopButton != null)
        {
            stopButton.onClick.AddListener(TogglePause);
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        
        if (isPaused)
        {
            Time.timeScale = 0f;  // 게임 정지
            Debug.Log("게임 일시정지");
        }
        else
        {
            Time.timeScale = 1f;  // 게임 재개
            Debug.Log("게임 재개");
        }
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

        // monsterCountText - MonsterSpawner도 필요함 합치면 해결 될 듯
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