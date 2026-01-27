using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovemont : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private float _moveSpeed;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 movemont = Vector3.zero;
        movemont.x = Input.GetAxisRaw("Horizontal");
        movemont.z = Input.GetAxisRaw("Vertical");

        if (movemont == Vector3.zero)
        {
            _animator.SetBool("IsRunning", false);
        }

        else
        {
            transform.rotation = Quaternion.LookRotation(movemont);
            transform.Translate(Vector3.forward * _moveSpeed * Time.deltaTime);
            _animator.SetBool("IsRunning", true);
        }
    }
}
