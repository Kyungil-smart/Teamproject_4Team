using UnityEngine;

public class InteractableTower : MonoBehaviour, IPlayerInteractable
{
    public void OnPlayerInteract()
    {
        Debug.Log("타워 상호작용됨");

        // 마우스 락 해제
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
