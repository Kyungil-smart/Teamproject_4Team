using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIcontroller : MonoBehaviour
{
    // 타워 선택 UI (CenterPanel)
    [SerializeField] private List<GameObject> TowerSelectionPanel = new List<GameObject>();

    // 설치 확인 UI (CheckPanel)
    [SerializeField] private List<GameObject> BuildConfirmPanel = new List<GameObject>();

    // 업그레이드 확인 UI
    [SerializeField] private List<GameObject> UpgradeConfirmPanel = new List<GameObject>();

    //private TileRaycaster _raycaster;

    [SerializeField] private TileRaycaster _raycaster;
    private int _selectedTower = 0;

    //private void Start()
    //{
    //    _raycaster = GetComponent<TileRaycaster>();
    //}

    // ===============================
    // 타워 선택 UI
    // ===============================
    

    public void OpenTowerSelection()
    {
        foreach (GameObject g in TowerSelectionPanel)
            g.SetActive(true);

        Debug.Log("UI open");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseTowerSelection()
    {
        foreach (GameObject g in TowerSelectionPanel)
            g.SetActive(false);

        // ❗ 여기서 커서 잠그지 않는다 (중요)
    }

    // ===============================
    // 카드 버튼들
    // ===============================

    public void BulletTower()
    {
        _selectedTower = (int)TowerType.GunTower;
        Debug.Log($"{(TowerType)_selectedTower} Selected Tower");
        BuildSelect();
    }

    public void CannonTower()
    {
        _selectedTower = (int)TowerType.CannonTower;
        Debug.Log($"{(TowerType)_selectedTower} Selected Tower");
        BuildSelect();
    }

    // 카드 선택 완료 버튼
    public void BuildSelect()
    {
        Debug.Log($"{(TowerType)_selectedTower} Selected BuildTower");

        if (_raycaster != null)
            _raycaster.OnTowerSelectedFromUI();

        CloseTowerSelection();
    }

    // ===============================
    // 설치 확인 UI
    // ===============================

    public void OpenBuildConfirmUI()
    {
        foreach (GameObject g in BuildConfirmPanel)
            g.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseBuildConfirmUI()
    {
        foreach (GameObject g in BuildConfirmPanel)
            g.SetActive(false);
    }

    // ✔ 버튼
    public void OnConfirmButton()
    {
        Debug.Log("✔ 설치 버튼 클릭됨");

        if (_raycaster != null)
            _raycaster.ConfirmBuildFromUI(_selectedTower);

        CloseBuildConfirmUI();
        LockCursor();
    }

    // ✖ 버튼
    public void OnCancelButton()
    {
        Debug.Log("✖ 취소 버튼 클릭됨");

        if (_raycaster != null)
            _raycaster.CancelBuildFromUI();

        CloseBuildConfirmUI();
        LockCursor();
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // ===============================
    // 업그레이드 확인 UI
    // ===============================
    public void OpenUpgradeConfirmUI()
    {
        foreach (GameObject g in UpgradeConfirmPanel)
            g.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseUpgradeConfirmUI()
    {
        foreach (GameObject g in UpgradeConfirmPanel)
            g.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // ✔ 업그레이드 YES 버튼 (OnClick에서 호출됨)
    public void OnUpgradeYesButton()
    {
        Debug.Log("✔ 업그레이드 YES 클릭");

        if (_raycaster != null)
            _raycaster.OnUpgradeConfirm();
    }

    // ✖ 업그레이드 NO 버튼 (OnClick에서 호출됨)
    public void OnUpgradeNoButton()
    {
        Debug.Log("✖ 업그레이드 NO 클릭");

        if (_raycaster != null)
            _raycaster.OnUpgradeCancel();
    }

}

public enum TowerType
{
    GunTower,
    CannonTower
}
