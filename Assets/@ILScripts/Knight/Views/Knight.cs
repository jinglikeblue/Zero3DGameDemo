using UnityEngine;
using DG.Tweening;
using IL.Zero;
using Zero;
using System;
using Jing;

namespace Knight
{
    public class Knight : AView
    {
        /// <summary>
        /// 移动的速度
        /// </summary>
        const float MOVE_SPEED = 2;

        /// <summary>
        /// 跑动的速度
        /// </summary>
        const float RUN_SPEED = 5;

        KnightASM _asm;       
        
        CharacterController _controller;       

        Vector3 _moveDir = Vector3.zero;

        float _moveSpeed = 0f;

        FiniteStateMachine<EKnightState> _fsm = new FiniteStateMachine<EKnightState>();

        protected override void OnInit()
        {
            _asm = new KnightASM(GetComponent<Animator>());            
            _controller = GetComponent<CharacterController>();

            _fsm.RegistState(EKnightState.IDLE, null, null, null, null);
            _fsm.RegistState(EKnightState.MOVE, null, null, null, null);
            _fsm.RegistState(EKnightState.ATTACK, null, null, null, null);
            _fsm.RegistState(EKnightState.BLOCK, null, null, null, null);
            _fsm.SwitchState(EKnightState.IDLE);
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
                    _moveSpeed = RUN_SPEED;
                }
                else
                {
                    _moveSpeed = MOVE_SPEED;
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

        public void Attack(bool isHold)
        {
            _asm.Action(isHold?2:0);            
        }

        public void Block(bool isHold)
        {
            if (isHold)
            {
                _asm.Action(1);
            }
            else
            {
                _asm.Action(0);
            }
        }
    }
}