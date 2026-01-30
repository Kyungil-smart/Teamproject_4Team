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
        GenerateManager<UIManager>();
    }

    private void GenerateManager<T>() where T : Component
    {
        if (FindObjectOfType<T>() != null) return;
        
        var go = new GameObject(typeof(T).Name);
        go.AddComponent<T>();
        DontDestroyOnLoad(go);
    }
}
