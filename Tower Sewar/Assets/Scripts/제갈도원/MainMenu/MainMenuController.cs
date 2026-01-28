using UnityEngine;
using UnityEngine.Serialization;

public class MainMenuController : MonoBehaviour
{
    [FormerlySerializedAs("_mainPanel")]
    [Header("UI Panels")] 
    [SerializeField] private GameObject mainPanel;
    [FormerlySerializedAs("_howToPanel")] [SerializeField] private GameObject howToPanel;
    [FormerlySerializedAs("_creditPanel")] [SerializeField] private GameObject creditPanel;

    // Start 버튼
    public void StartButton()
    {
        GameSceneManager.Instance.LoadScene();
    }

    // HowTo 열기
    public void HowToButton()
    {
        mainPanel.SetActive(false);
        howToPanel.SetActive(true);
    }

    // Credit 열기
    public void CreditButton()
    {
        mainPanel.SetActive(false);
        creditPanel.SetActive(true);
    }

    public void BackButton()
    {
        howToPanel.SetActive(false);
        creditPanel.SetActive(false);
        mainPanel.SetActive(true);
    }


    // 게임 종료
    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("Exit");
    }
}