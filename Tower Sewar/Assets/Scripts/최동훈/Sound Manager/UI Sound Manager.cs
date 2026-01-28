using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISoundManager : MonoBehaviour
{
    private static UISoundManager instance;

    void Awake()
    {
        // 메니저 생성
        if (instance == null)
        {
            instance = gameObject.GetComponent<UISoundManager>();
            DontDestroyOnLoad(gameObject);
        }

        // 오브젝트 파괴
        else
        {
            Destroy(gameObject);
        }
    }
}
