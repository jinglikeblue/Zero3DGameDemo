using GameKit;
using IL.Zero;
using UnityEngine;

namespace IL
{
    class GamePanel : AView
    {
        GameStage _stage;
        Joystick _moveJoystick;
        Touchpad _signTouchpad;

        protected override void OnInit()
        {
            _stage = StageMgr.Ins.Switch<GameStage>();
            _moveJoystick = GetChildComponent<Joystick>("Joystick");
            //_moveJoystick.uiCamera = GameObject.Find("UICamera").GetComponent<Camera>();
            _signTouchpad = GetChildComponent<Touchpad>("Touchpad");
        }

        protected override void OnDestroy()
        {
            
        }

        protected override void OnEnable()
        {
            _moveJoystick.onValueChange += OnMoveValueChange;
            _signTouchpad.onValueChange += OnSignValueChange;
        }

        protected override void OnDisable()
        {
            _moveJoystick.onValueChange -= OnMoveValueChange;
            _signTouchpad.onValueChange -= OnSignValueChange;
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
