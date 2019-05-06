using UnityEngine;
using DG.Tweening;

public class Knight : MonoBehaviour
{
    [SerializeField]
    KnightModel _model;

    [SerializeField]
    Animator _animator;

    [SerializeField]
    CharacterController _controller;

    [Header("移动速度")]
    public float moveSpeed;

    [Header("跑动速度")]
    public float runSpeed;    

    Vector3 _moveDir = Vector3.zero;    

    private void Awake()
    {
        //_model.SetSwordShow(false);
    }

    void Start()
    {

    }


    void Update()
    {

    }

    public void Move(Vector3 dir)
    {
        _moveDir = dir;
        Rotation();
    }

    void Rotation()
    {
        if (_moveDir != Vector3.zero)
        {
            Quaternion q = Quaternion.FromToRotation(Vector3.forward, _moveDir);
            Vector3 angle = new Vector3(0, q.eulerAngles.y, 0);
            if (transform.eulerAngles != angle)
            {
                transform.DORotate(angle, 0.3f);
            }
        }
    }

    private void FixedUpdate()
    {
        int moveState = 0;
        Vector3 dir = _moveDir.normalized;
        
        if (_moveDir != Vector3.zero)
        {
            if (_moveDir.magnitude > 0.9f)
            {
                moveState = 2;
                _controller.SimpleMove(dir * runSpeed);
            }
            else
            {
                moveState = 1;
                _controller.SimpleMove(dir * moveSpeed);
            }
        }        
        _animator.SetInteger("move_state", moveState);
    }
}
