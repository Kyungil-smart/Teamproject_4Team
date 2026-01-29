using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRaycaster : MonoBehaviour
{
    [SerializeField] private float rayDistance = 2f;

    [SerializeField] private float rayHeightOffset = 0.5f;

    public float RayDistance => rayDistance;
    public float RayHeightOffset => rayHeightOffset;

    [SerializeField] private LayerMask tileLayer;

    public GameObject CurrentHoverObject {  get; private set; }

    public GameObject SelectedObject { get; private set; }

    private RaycastHit hit;

    private void Update()
    {
        HandleRaycast();
        HandleClick();
    }

    private void HandleRaycast()
    {

        Vector3 rayOrigin = transform.position + Vector3.up * rayHeightOffset;
        Ray ray = new Ray(rayOrigin, transform.forward);

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
        // 레이 항상 표시 (두껍게)
        Gizmos.color = Color.red;

        Vector3 rayOrigin = transform.position + Vector3.up * rayHeightOffset;
        Vector3 rayEnd = rayOrigin + transform.forward * rayDistance;

        // 중심선
        Gizmos.DrawLine(rayOrigin, rayEnd);

        // 두께 표현용 보조선 (좌우)
        Gizmos.DrawLine(rayOrigin + transform.right * 0.05f, rayEnd + transform.right * 0.05f);
        Gizmos.DrawLine(rayOrigin - transform.right * 0.05f, rayEnd - transform.right * 0.05f);

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
