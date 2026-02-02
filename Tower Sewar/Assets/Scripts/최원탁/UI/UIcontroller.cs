using System.Collections.Generic;
using UnityEngine;

public class UIcontroller : MonoBehaviour
{
    // 타워 선택 UI (CenterPanel)
    [SerializeField] private List<GameObject> TowerSelectionPanel = new List<GameObject>();

    // 설치 확인 UI (CheckPanel)
    [SerializeField] private List<GameObject> BuildConfirmPanel = new List<GameObject>();

    private TileRaycaster _raycaster;
    private int _selectedTower = 0;

    private void Start()
    {
        _raycaster = GetComponent<TileRaycaster>();
    }

    // ===============================
    // 타워 선택 UI
    // ===============================

    public void OpenTowerSelection()
    {
        foreach (GameObject g in TowerSelectionPanel)
            g.SetActive(true);

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

    public void GunTower()
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

    public void IceTower()
    {
        _selectedTower = (int)TowerType.IceTower;
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
            _raycaster.ConfirmBuildFromUI();

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
}

public enum TowerType
{
    GunTower,
    CannonTower,
    IceTower
}
