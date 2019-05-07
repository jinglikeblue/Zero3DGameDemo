using System;
using GameKit;
using IL;
using IL.Zero;
using UnityEngine;
using UnityEngine.UI;

namespace Knight
{
    class GamePanel : AView
    {
        GameStage _stage;
        Joystick _moveJoystick;
        Touchpad _signTouchpad;
        Button _btnExit; 

        protected override void OnInit()
        {
            _btnExit = GetChildComponent<Button>("BtnExit");
            _stage = StageMgr.Ins.Switch<GameStage>();
            _moveJoystick = GetChildComponent<Joystick>("Joystick");
            //_moveJoystick.uiCamera = GameObject.Find("UICamera").GetComponent<Camera>();
            _signTouchpad = GetChildComponent<Touchpad>("Touchpad");
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
        }

        protected override void OnDisable()
        {
            _btnExit.onClick.RemoveListener(Exit);
            _moveJoystick.onValueChange -= OnMoveValueChange;
            _signTouchpad.onValueChange -= OnSignValueChange;
        }

        private void Exit()
        {
            UIPanelMgr.Ins.Switch<MenuPanel>();
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
