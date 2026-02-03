using UnityEngine;

public class Turret_Rotate : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed = 30.0f;
    [SerializeField] private Turret _turret;

    [SerializeField] private bool _canLookAt = true;

    void Awake()
    {
        _turret = GetComponentInParent<Turret>();
    }

    void Update()
    {
        if (_turret == null)
        {
            Debug.Log("터렛이 존재하지 않습니다.");
            return;
        }

        if (_turret.IsEnemy && _turret._currentTarget != null)
        {
            Vector3 targetPos = _turret._currentTarget.position;

            if (_canLookAt)
            {
                transform.LookAt(targetPos + (Vector3.up * 0.7f));
            }
            else
            {
                targetPos.y = transform.position.y;
                transform.LookAt(targetPos);
            }
        }
        else
        {
            if (transform.localEulerAngles.x != 0 || transform.localEulerAngles.z != 0)
            {
                Quaternion setRotate = Quaternion.Euler(0, transform.localEulerAngles.y, 0);
                transform.localRotation = Quaternion.RotateTowards(transform.localRotation, setRotate, _rotateSpeed * Time.deltaTime);
            }

            transform.Rotate(Vector3.up * _rotateSpeed * Time.deltaTime);
        }
    }
}