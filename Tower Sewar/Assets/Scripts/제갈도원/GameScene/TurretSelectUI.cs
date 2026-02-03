using UnityEngine;
using UnityEngine.UI;

public class TurretSelectUI : MonoBehaviour
{
    [Header("Turret Buttons")]
    [SerializeField] private Button[] turretButtons;
    [SerializeField] private Image[] buttonImages;
    
    [Header("Panels")]
    [SerializeField] private GameObject selectPanel;    // SelectTurretPanel
    [SerializeField] private GameObject checkPanel;     // CheckPanel
    
    [Header("Check Panel Buttons")]
    [SerializeField] private Button confirmButton;      // ✓ 버튼
    [SerializeField] private Button cancelButton;       // ✕ 버튼
    
    [Header("Select Highlight")]
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color selectedColor = Color.yellow;
    
    private int selectedIndex = -1;

    private void start()
    {
        // 터렛 버튼 클릭 이벤트
        for (int i = 0; i < turretButtons.Length; i++)
        {
            int index = i;
            turretButtons[i].onClick.AddListener(() => SelectTurret(index));
        }
        
        // CheckPanel 버튼 이벤트
        if (confirmButton != null)
            confirmButton.onClick.AddListener(OnConfirm);
        
        if (cancelButton != null)
            cancelButton.onClick.AddListener(OnCancel);
        
        // 시작 시 패널 숨기기
        if (selectPanel != null) selectPanel.SetActive(false);
        if (checkPanel != null) checkPanel.SetActive(false);
    }

    // 터렛 선택 → CheckPanel 열기
    public void SelectTurret(int index)
    {
        selectedIndex = index;
        
        // 하이라이트
        for (int i = 0; i < buttonImages.Length; i++)
        {
            buttonImages[i].color = normalColor;
        }
        buttonImages[index].color = selectedColor;
        
        Debug.Log($"터렛 {index + 1} 선택됨");
        
        // CheckPanel 열기
        if (checkPanel != null)
            checkPanel.SetActive(true);
    }

    // ✓ 확인 버튼
    public void OnConfirm()
    {
        if (selectedIndex < 0) return;
        
        Debug.Log($"터렛 {selectedIndex + 1} 설치!");
        
        // TODO: 터렛 설치 로직
        
        CloseAll();
    }

    // ✕ 취소 버튼
    public void OnCancel()
    {
        Debug.Log("취소");
        
        // CheckPanel만 닫고 SelectPanel은 유지? 아니면 둘 다 닫기?
        if (checkPanel != null)
            checkPanel.SetActive(false);
        
        // 선택 초기화
        selectedIndex = -1;
        for (int i = 0; i < buttonImages.Length; i++)
        {
            buttonImages[i].color = normalColor;
        }
    }

    // 패널 열기
    // public void OpenPanel()
    // {
    //     if (selectPanel != null) selectPanel.SetActive(true);
    //     if (checkPanel != null) checkPanel.SetActive(false);
    //     selectedIndex = -1;
    //     
    //     for (int i = 0; i < buttonImages.Length; i++)
    //     {
    //         buttonImages[i].color = normalColor;
    //     }
    // }

    // 모든 패널 닫기
    public void CloseAll()
    {
        if (selectPanel != null) selectPanel.SetActive(false);
        if (checkPanel != null) checkPanel.SetActive(false);
        selectedIndex = -1;
    }
}