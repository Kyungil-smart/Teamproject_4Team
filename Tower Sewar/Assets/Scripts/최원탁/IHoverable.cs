using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHoverable
{
    // 오브젝트에 닿으면 아웃라인 표시
    void OnHoverEnter(); // Ray가 처음 닿았을 때
    void OnHoverExit();  // Ray가 벗어났을 때
}

