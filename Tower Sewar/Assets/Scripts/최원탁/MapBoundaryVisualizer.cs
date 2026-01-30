using UnityEngine;

[ExecuteAlways]
public class MapBoundaryVisualizer : MonoBehaviour
{
    [SerializeField] private Color gizmoColor = new Color(0f, 1f, 0f, 0.25f);

    private BoxCollider box; // 맵이라서 박스 콜라이더 갖고옴

    private void OnDrawGizmos()
    {
        if (box == null)
            box = GetComponent<BoxCollider>();

        if (box == null)
            return;

        Gizmos.color = gizmoColor;

        // 월드 좌표 기준
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawCube(box.center, box.size);
    }
}
