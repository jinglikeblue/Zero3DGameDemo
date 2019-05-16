using UnityEngine;
using DG.Tweening;
using IL.Zero;
using Zero;
using System;

namespace Knight
{
    public class Knight : AView
    {

        KnightASM _asm;       
        
        CharacterController _controller;

        [Header("移动速度")]
        public float moveSpeed = 2;

        [Header("跑动速度")]
        public float runSpeed = 5;

        Vector3 _moveDir = Vector3.zero;

        float _moveSpeed = 0f;

        protected override void OnInit()
        {
            _asm = new KnightASM(GetComponent<Animator>());            
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
            _asm.StepCheck();

            if (_moveSpeed >= 1)
            {
                Vector3 dir = _moveDir.normalized;
                _controller.SimpleMove(dir * _moveSpeed);
            }
        }

        public void Move(Vector3 dir)
        {
            _moveDir = dir;
            UpdateMoveSpeed();
            Rotation();
        }

        void UpdateMoveSpeed()
        {
            if (_moveDir != Vector3.zero)
            {
                if (_moveDir.magnitude > 0.9f)
                {
                    _moveSpeed = runSpeed;
                }
                else
                {
                    _moveSpeed = moveSpeed;
                }
            }
            else
            {
                _moveSpeed = 0;
            }
            _asm.Move(_moveSpeed);            
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

        public void Attack()
        {
            _asm.Attack(1);
            //_animator.SetInteger("DeathType", 1);
            //_animator.SetInteger("Action", 1);
        }

        public void Block()
        {
            //_animator.SetInteger("move_state", 4);
        }
    }
}