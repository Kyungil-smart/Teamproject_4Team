using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance {get; private set;}
    [SerializeField] private int _playerGold;
    [SerializeField] private int _playerLife;
    
    // HUD 에서 골드, 목슴 연동 할 거
    public int PlayerGold { get; set; }
    public int PlayerLife { get; set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Init();
    }
    private void Start()
    {
    }

    public void Init()
    {
        PlayerLife = 10;
    }
}
