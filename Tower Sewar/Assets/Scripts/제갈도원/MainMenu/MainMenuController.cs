using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public enum TitleType
{
    Main,
    HowTo,
    Credit
}

public class MainMenuController : MonoBehaviour
{
    [Header("Title")] [SerializeField] private TextMeshProUGUI titleText;

    [Header("UI Panels")] [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject howToPanel;
    [SerializeField] private GameObject creditPanel;

    private void Start()
    {
        Show(TitleType.Main);
    }

    // Start 버튼
    public void StartButton()
    {
        GameSceneManager.Instance.LoadScene();
    }

    public void HowToButton() => Show(TitleType.HowTo);
    public void CreditButton() => Show(TitleType.Credit);
    public void BackButton() => Show(TitleType.Main);
    
    private void Show(TitleType titleType)
    {
        mainPanel.SetActive(titleType == TitleType.Main);
        howToPanel.SetActive(titleType == TitleType.HowTo);
        creditPanel.SetActive(titleType == TitleType.Credit);
        
        switch (titleType)
        {
            case TitleType.Main: titleText.text = "타워세워"; break;
            case TitleType.HowTo: titleText.text = "HowTo"; break;
            case TitleType.Credit: titleText.text = "Credit"; break;
        }
    }

    // 게임 종료
    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("Exit");
    }
}