using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zero;

namespace Knight
{
    public class KnightASM
    {
        const string SPEED = "speed";
        const string DEATH_TYPE = "deathType";
        const string ACTION = "action";
        const string IDLE_TYPE = "idleType";

        struct ParametersVO
        {
            public float speed;
            public int deathType;
            public int action;
            public int idleType;
        }

        int actionTagHash;
        int idle1Hash;

        private Animator _a;

        ParametersVO _vo;

        public KnightASM(Animator animator)
        {
            _a = animator;
            _vo.speed = 0;
            _vo.deathType = 0;
            _vo.action = 0;
            _vo.idleType = 0;

            actionTagHash = Animator.StringToHash("Action");
            idle1Hash = Animator.StringToHash("Base Layer.Idle.Idle1");
        }

        public void StepCheck()
        {
            var stateInfo = _a.GetCurrentAnimatorStateInfo(0);          
            
            if(stateInfo.fullPathHash == idle1Hash)
            {
                var random = Random.Range(0, 10);                
                switch (random)
                {
                    case 2:
                        _vo.idleType = 2;
                        break;
                    case 3:
                        _vo.idleType = 3;
                        break;
                    case 4:
                        _vo.idleType = 4;
                        break;
                }
            }
            //else if (stateInfo.normalizedTime >= 0 && stateInfo.tagHash == actionTagHash)
            //{
            //    _vo.action = 0;
            //    SyncParameters();
            //    return;
            //}
        }

        void SyncParameters()
        {
            _a.SetFloat(SPEED, _vo.speed);
            _a.SetInteger(DEATH_TYPE, _vo.deathType);
            _a.SetInteger(ACTION, _vo.action);
            _a.SetInteger(IDLE_TYPE, _vo.idleType);
        }

        

        public void Move(float speed)
        {
            _vo.speed = speed;            
            SyncParameters();
        }

        public void Attack(int type)
        {
            _vo.speed = 0;
            _vo.idleType = 0;
            _vo.action = type;
            SyncParameters();
        }

        public void Death(int type)
        {
            _vo.deathType = type;
            SyncParameters();
        }

        public void Block(int block)
        {

        }
    }
}