using System;
using GameKit;
using IL;
using IL.Zero;
using UnityEngine;
using UnityEngine.UI;
using Zero;

namespace Knight
{
    class GamePanel : AView
    {
        GameStage _stage;
        Joystick _moveJoystick;
        Touchpad _signTouchpad;
        Button _btnExit;

        Button _btnAtk;
        Button _btnDef;

        protected override void OnInit()
        {
            _btnExit = GetChildComponent<Button>("BtnExit");
            _stage = StageMgr.Ins.Switch<GameStage>();
            _moveJoystick = GetChildComponent<Joystick>("Joystick");
            //_moveJoystick.uiCamera = GameObject.Find("UICamera").GetComponent<Camera>();
            _signTouchpad = GetChildComponent<Touchpad>("Touchpad");

            _btnAtk = GetChildComponent<Button>("BtnAtk");
            _btnDef = GetChildComponent<Button>("BtnDef");
        }

        protected override void OnDestroy()
        {
            StageMgr.Ins.Clear();
        }

        protected override void OnEnable()
        {
            _btnExit.onClick.AddListener(Exit);
            _moveJoystick.onValueChange += OnMoveValueChange;
            _signTouchpad.onValueChange += OnSignValueChange;
            //_btnAtk.onClick.AddListener(DoAttack);
            //_btnDef.onClick.AddListener(DoBlock);


            PointerDownEventListener.Get(_btnAtk.gameObject).onEvent += (e)=> {
                _stage.Knight.Action(2);
            };

            PointerUpEventListener.Get(_btnAtk.gameObject).onEvent += (e) => {
                _stage.Knight.Action(0);
            };

            PointerDownEventListener.Get(_btnDef.gameObject).onEvent += (e) => {
                _stage.Knight.Action(1);
            };

            PointerUpEventListener.Get(_btnDef.gameObject).onEvent += (e) => {
                _stage.Knight.Action(0);
            };

            AudioPlayer.Ins.PlayBGM(ResMgr.Ins.Load<AudioClip>(AssetBundleName.AUDIO, "BattleBGM"));
        }

        protected override void OnDisable()
        {
            _btnExit.onClick.RemoveListener(Exit);
            _moveJoystick.onValueChange -= OnMoveValueChange;
            _signTouchpad.onValueChange -= OnSignValueChange;            
            //_btnAtk.onClick.RemoveListener(DoAttack);
            //_btnDef.onClick.RemoveListener(DoBlock);
        }

        private void DoAttack()
        {
            _stage.Knight.Action(1);
        }

        private void DoBlock()
        {
            _stage.Knight.Block();
        }

        private void Exit()
        {
            UIPanelMgr.Ins.Switch<LoadingPanel>(new LoadingVO(typeof(MenuPanel)));            
        }

        private void OnSignValueChange(Vector2 v)
        {
            _stage.SetSign(v);
        }

        private void OnMoveValueChange(Vector2 v)
        {
            _stage.SetMove(v);
        }
    }
}
