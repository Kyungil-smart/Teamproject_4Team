using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public enum InfoType
    {
        Health,
        Gold,
        Goal,
        Stop,
        Wave,
        Time
    }

    public InfoType type;

    TextMeshProUGUI myText;

    private void Awake()
    {
        myText = GetComponent<TextMeshProUGUI>();
    }

    private void LateUpdate()
    {
        if (WaveManager._instance == null) return;

        switch (type)
        {
            case InfoType.Wave:
                myText.text = $"Wave : {WaveManager._instance.Wave + 1}";
                break;
            case InfoType.Time:
                float time = WaveManager._instance.WaveTimer;
                int minutes = (int)(time / 60);
                int seconds = (int)(time % 60);
                myText.text = $"{minutes:00}:{seconds:00}";
                break;

            case InfoType.Goal:
                // 현재 맵에 남은 몬스터 수는 알 수 있음
                int remaining = WaveManager._instance.NumsOfMonsters;
                myText.text = $"{remaining} / ??"; // 총 목표는 추가 구현 필요
                break;

            case InfoType.Health:
                myText.text = $"x {DataManager.Instance.PlayerLife}";
                break;
            case InfoType.Gold:
                myText.text = $": {DataManager.Instance.PlayerGold}";
                break;
        }
    }
}