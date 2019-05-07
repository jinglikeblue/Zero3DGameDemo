using UnityEngine;
using DG.Tweening;
using IL.Zero;
using Zero;
using System;

namespace Knight
{
    public class Knight : AView
    {
        [SerializeField]
        Animator _animator;

        [SerializeField]
        CharacterController _controller;

        [Header("移动速度")]
        public float moveSpeed = 2;

        [Header("跑动速度")]
        public float runSpeed = 5;

        Vector3 _moveDir = Vector3.zero;

        protected override void OnInit()
        {
            _animator = GetComponent<Animator>();
            _controller = GetComponent<CharacterController>();
        }

        protected override void OnEnable()
        {
            ILBridge.Ins.onUpdate += OnUpdate;
        }

        protected override void OnDisable()
        {
            ILBridge.Ins.onUpdate -= OnUpdate;
        }

        private void OnUpdate()
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
                if (gameObject.transform.eulerAngles != angle)
                {
                    gameObject.transform.DORotate(angle, 0.3f);
                }
            }
        }
    }
}