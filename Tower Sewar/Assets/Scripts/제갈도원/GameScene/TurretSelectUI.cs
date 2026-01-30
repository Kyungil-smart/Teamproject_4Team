using UnityEngine;
using UnityEngine.UI;

public class TurretSelectUI : MonoBehaviour
{
    [Header("Turret Buttons")]
    [SerializeField] private Button[] turretButtons;  // 터렛 버튼 3개
    [SerializeField] private Image[] buttonImages;    // 버튼 이미지 (선택 표시용)
    
    [Header("Control Buttons")]
    [SerializeField] private Button confirmButton;    // 확인 버튼
    [SerializeField] private Button cancelButton;     // 취소 버튼
    
    [Header("Panel")]
    [SerializeField] private GameObject panel;        // CenterPanel
    
    [Header("Select Highlight")]
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color selectedColor = Color.yellow;
    
    private int selectedIndex = -1;  // 선택된 터렛 인덱스

    private void Start()
    {
        // 터렛 버튼 클릭 이벤트 연결
        for (int i = 0; i < turretButtons.Length; i++)
        {
            int index = i;  // 클로저 문제 방지
            turretButtons[i].onClick.AddListener(() => SelectTurret(index));
        }
        
        // 확인/취소 버튼 이벤트 연결
        confirmButton.onClick.AddListener(OnConfirm);
        cancelButton.onClick.AddListener(OnCancel);
        
        // 시작 시 패널 숨기기
        panel.SetActive(false);
    }

    // 터렛 선택
    public void SelectTurret(int index)
    {
        selectedIndex = index;
        
        // 모든 버튼 색상 초기화
        for (int i = 0; i < buttonImages.Length; i++)
        {
            buttonImages[i].color = normalColor;
        }
        
        // 선택된 버튼 하이라이트
        buttonImages[index].color = selectedColor;
        
        Debug.Log($"터렛 {index + 1} 선택됨");
    }

    // 확인 버튼
    public void OnConfirm()
    {
        if (selectedIndex < 0)
        {
            Debug.Log("터렛을 선택해주세요!");
            return;
        }
        
        Debug.Log($"터렛 {selectedIndex + 1} 설치!");
        
        // TODO: 여기에 터렛 설치 로직 연결
        // 예: TurretManager.Instance.BuildTurret(selectedIndex);
        
        ClosePanel();
    }

    // 취소 버튼
    public void OnCancel()
    {
        Debug.Log("취소");
        ClosePanel();
    }

    // 패널 열기 (F 키에서 호출)
    public void OpenPanel()
    {
        panel.SetActive(true);
        selectedIndex = -1;
        
        // 색상 초기화
        for (int i = 0; i < buttonImages.Length; i++)
        {
            buttonImages[i].color = normalColor;
        }
    }

    // 패널 닫기
    public void ClosePanel()
    {
        panel.SetActive(false);
        selectedIndex = -1;
    }
}