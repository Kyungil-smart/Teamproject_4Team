using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        Init();
    }

    private void Init()
    {
        // GenerateManager<WaveManager>();
        GenerateManager<DataManager>();
        GenerateManager<SoundManager>();
        GenerateManager<InputManager>();
        GenerateManager<Title_BGM_Manager>();
        GenerateManager<UIManager>();
        GenerateManager<UI_SFX_Manager>();
        GenerateManager<Enemy_Sound_Manager>();
        GenerateManager<Enemy_VFX_Manager>();
        GenerateManager<Stage_Sound_Manager>();
        GenerateManager<Base_Sound_Manager>();
        GenerateManager<Base_VFX_Manager>();
    }

    private void GenerateManager<T>() where T : Component
    {
        if (FindObjectOfType<T>() != null) return;
        
        var go = new GameObject(typeof(T).Name);
        go.AddComponent<T>();
        DontDestroyOnLoad(go);
    }
}
