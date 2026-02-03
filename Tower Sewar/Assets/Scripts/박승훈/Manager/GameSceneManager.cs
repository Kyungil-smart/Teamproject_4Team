using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance {get; private set;}

    private static int nowStage;
    
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
    
    // 게임 씬 이동
    public void LoadGame()
    {
        nowStage = 1;
        Time.timeScale = 1f;
        SceneManager.LoadScene(nowStage);
    }

    public void LoadNextStage()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(CurrentSceneIndex() + 1);
    }

    public int CurrentSceneIndex()
    {
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
        return SceneManager.GetActiveScene().buildIndex;
    }
    
    // 타이틀 씬 이동
    public void LoadTitle()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
