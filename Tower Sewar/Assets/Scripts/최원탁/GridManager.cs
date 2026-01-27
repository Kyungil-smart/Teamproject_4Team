using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private float cellSize = 2f;
    [SerializeField] private Vector3 girdOrigin = Vector3.zero;

    private Dictionary<Vector2Int, bool> occupiedCells
        = new Dictionary<Vector2Int, bool>();

    
}
