using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private ControlStateManager _controlStateManager;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(_controlStateManager.CurrentState != ControlStateManager.ControlState.GamePlay)
        {
            _animator.SetBool("IsRunning", false);
            return;
        }
        Move();
    }

    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(h, 0f, v);

        if (movement.sqrMagnitude <= 0.0001f)
        {
            _animator.SetBool("IsRunning", false);
            return;
        }

        movement.Normalize();

        transform.rotation = Quaternion.LookRotation(movement);

        transform.Translate(Vector3.forward * _moveSpeed * Time.deltaTime);

        _animator.SetBool("IsRunning", true);
    }
}
