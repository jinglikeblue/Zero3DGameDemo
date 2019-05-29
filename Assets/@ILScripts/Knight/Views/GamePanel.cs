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
        Button _btnSetting;

        Button _btnAtk;
        Button _btnDef;

        protected override void OnInit()
        {
            _btnSetting = GetChildComponent<Button>("BtnSetting");
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
            _btnSetting.onClick.AddListener(ShowSettingWin);
            _moveJoystick.onValueChange += OnMoveValueChange;
            _signTouchpad.onValueChange += OnSignValueChange;


            PointerDownEventListener.Get(_btnAtk.gameObject).onEvent += (e)=> {
                _stage.Knight.Attack(true);
            };

            PointerUpEventListener.Get(_btnAtk.gameObject).onEvent += (e) => {
                _stage.Knight.Attack(false);
            };

            PointerDownEventListener.Get(_btnDef.gameObject).onEvent += (e) => {
                _stage.Knight.Block(true);
            };

            PointerUpEventListener.Get(_btnDef.gameObject).onEvent += (e) => {
                _stage.Knight.Block(false);
            };

            Global.Ins.audioDevice.StopAll();
            Global.Ins.audioDevice.Play(ResMgr.Ins.Load<AudioClip>(AssetBundleName.AUDIO, "BattleBGM"), true);            
        }

        protected override void OnDisable()
        {
            _btnSetting.onClick.RemoveListener(ShowSettingWin);
            _moveJoystick.onValueChange -= OnMoveValueChange;
            _signTouchpad.onValueChange -= OnSignValueChange;            
        }

        private void ShowSettingWin()
        {
            UIWinMgr.Ins.Open<SettingWin>();            
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
