using UnityEngine;

public class InteractableTower : MonoBehaviour, IPlayerInteractable
{
    public void OnPlayerInteract()
    {
        // 마우스 락 해제
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
