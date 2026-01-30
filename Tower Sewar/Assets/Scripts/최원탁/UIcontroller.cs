using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIcontroller : MonoBehaviour
{
    [SerializeField] private GameObject TowerSelectionPanel;

    public void OpenTowerSelection()
    {
        TowerSelectionPanel.SetActive(true);
    }
    
    public void CloseTowerSelection()
    {
        TowerSelectionPanel.SetActive(false);
    }
}
