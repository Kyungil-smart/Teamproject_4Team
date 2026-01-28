using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject howToPanel;
    [SerializeField] private GameObject creditPanel;

    // Start 버튼
    public void StartButton()
    {
        GameSceneManager.Instance.LoadScene();
    }
    
    // HowTo 열기
    public void HowToButton()
    {
        howToPanel.SetActive(true);
    }

    // Credit 열기
    public void CreditButton()
    {
        creditPanel.SetActive(true);
    }

    // 게임 종료
    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("Exit");
    }
}