using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance {get; private set;}

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // 메뉴 선택 메서드
    public void SelectMenu()
    {
        // 메뉴 선택 시 동작 기능 구현
    }

    // 현재 보유 골드 불러오기
    public void GetGold()
    {
        
    }
}