using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRaycaster : MonoBehaviour
{
    [SerializeField] private float rayDistance = 100f;

    [SerializeField] private LayerMask tileLayer;

    public GameObject CurrentHoverObject {  get; private set; }

    public GameObject SelectedObject { get; private set; }

    private RaycastHit hit;

    private void Update()
    {
        // 마우스 락 상태일 때는 타일 선택 안 함
        if (Cursor.lockState == CursorLockMode.Locked)
            return;
        HandleRaycast();
        HandleClick();
    }

    private void HandleRaycast()
    {
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);

        if (Physics.Raycast(ray, out hit, rayDistance, tileLayer))
        {
            CurrentHoverObject = hit.collider.gameObject;
        }

        else
        {
            CurrentHoverObject = null;
        }
    }

    private void HandleClick()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        if(CurrentHoverObject != null)
        {
            SelectedObject = CurrentHoverObject;
        }
    }

    private void OnDrawGizmos()
    {
        if (CurrentHoverObject != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(
                CurrentHoverObject.transform.position,
                Vector3.one);
        }

        if(SelectedObject != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(
                SelectedObject.transform.position,
                Vector3.one * 1.2f);
        }
    }
}
