using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieAnimationScript : MonoBehaviour
{

    void Update()
    {
        StartCoroutine(DieRoutine());
    }
    
    IEnumerator DieRoutine()
    {
        Animator animator = GetComponent<Animator>();

        //죽는 애니메이션시간 가져오고 그만큼 기다림
        float dieTime = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(dieTime);

        Destroy(gameObject);
    }
}
