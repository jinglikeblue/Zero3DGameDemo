using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Knight
{
    public class KnightAttackStateController:BaseKnightStateController
    {
        int _atkTimes = 0;
        public KnightAttackStateController(Knight knight) : base(knight, EKnightState.ATTACK)
        {
        }

        public override bool CheckSwitchEnable(EKnightState fromState)
        {
            return true;
        }

        public override void OnEnter(EKnightState fromState, object data)
        {
            _atkTimes = 1;
            Knight.ASM.SyncParameters();
        }

        public override void OnExit(EKnightState toState)
        {

        }

        public override void OnUpdate(EKnightState currentState)
        {            
            var stateInfo = Knight.ASM.Animator.GetCurrentAnimatorStateInfo(0);            
            if (stateInfo.fullPathHash == Knight.ASM.AttackHash && stateInfo.normalizedTime >= _atkTimes)
            {
                _atkTimes++;
                Knight.ASM.SyncParameters();
                if (Knight.VO.action == 0)
                {
                    if(Knight.VO.speed > 0)
                    {
                        Knight.FSM.SwitchState(EKnightState.MOVE);
                    }
                    else
                    {
                        Knight.FSM.SwitchState(EKnightState.IDLE);
                    }
                }
            }
        }
    }
}
