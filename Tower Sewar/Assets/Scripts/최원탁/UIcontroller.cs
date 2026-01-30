using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIcontroller : MonoBehaviour
{
    [SerializeField] private List<GameObject> TowerSelectionPanel = new List<GameObject>();
    private TileRaycaster _raycaster;
    private int _selectedTower = 0;

    private void Start()
    {
        _raycaster = GetComponent<TileRaycaster>();
    }

    public void OpenTowerSelection()
    {
        foreach (GameObject g in TowerSelectionPanel)
        {
            g.SetActive(true);
        }
    }
    
    public void CloseTowerSelection()
    {
        foreach (GameObject g in TowerSelectionPanel)
        {
            g.SetActive(false);
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void GunTower()
    {
        _selectedTower = (int)TowerType.GunTower;
        Debug.Log($"{(TowerType)_selectedTower} Selected Tower");
    }
    public void CannonTower()
    {
        _selectedTower = (int)TowerType.CannonTower;
        Debug.Log($"{(TowerType)_selectedTower} Selected Tower");
        
    }
    public void IceTower()
    {
        _selectedTower = (int)TowerType.IceTower;
        Debug.Log($"{(TowerType)_selectedTower} Selected Tower");
        
    }

    public void BuildSelect()
    {
        Debug.Log($"{(TowerType)_selectedTower} Selected BuildTower");
        CloseTowerSelection();
    }

    public void TowerUpgrade()
    {
        Debug.Log("TowerUpgrade");
        CloseTowerSelection();
    }
}

public enum TowerType
{
    GunTower,
    CannonTower,
    IceTower
}