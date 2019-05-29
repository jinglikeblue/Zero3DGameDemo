using System;
using IL;
using IL.Zero;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zero;

namespace Knight
{
    public class MenuPanel : AView
    {
        Button _btnEnter;

        protected override void OnInit()
        {
            _btnEnter = GetChildComponent<Button>("BtnEnter");
        }

        protected override void OnEnable()
        {
            _btnEnter.onClick.AddListener(Enter);

            Global.Ins.audioDevice.StopAll();
            Global.Ins.audioDevice.Play(ResMgr.Ins.Load<AudioClip>(AssetBundleName.AUDIO, "MenuBGM"), true);
        }

        protected override void OnDisable()
        {
            _btnEnter.onClick.RemoveListener(Enter);
        }

        private void Enter()
        {
            UIPanelMgr.Ins.Switch<LoadingPanel>(new LoadingVO(typeof(GamePanel)));
        }

    }
}