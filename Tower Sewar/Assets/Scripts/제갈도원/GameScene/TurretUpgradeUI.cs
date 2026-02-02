using UnityEngine;
using UnityEngine.UI;

public class TurretUpgradeUI : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField] private GameObject upgradePanel;
    
    [Header("Buttons")]
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button cancelButton;
    
    
    
    private Turret selectedTurret;
    
    private void Start()
    {
        if (confirmButton != null)
            confirmButton.onClick.AddListener(OnConfirmUpgrade);
        
        if (cancelButton != null)
            cancelButton.onClick.AddListener(OnCancel);
        
        if (upgradePanel != null)
            upgradePanel.SetActive(false);
    }
    
    public void OpenPanel(Turret turret)
    {
        selectedTurret = turret;
        upgradePanel.SetActive(true);
    }
    
    public void OnConfirmUpgrade()
    {
        if (selectedTurret == null) return;
        
        //  업그레이 로직 (현수 님 코드랑 붙이면 됨 )
        Debug.Log("업그레이드!");
        
        ClosePanel();
    }
    
    public void OnCancel()
    {
        ClosePanel();
    }
    
    public void ClosePanel()
    {
        upgradePanel.SetActive(false);
        selectedTurret = null;
    }
}