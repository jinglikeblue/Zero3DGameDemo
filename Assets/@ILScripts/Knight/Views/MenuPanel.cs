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
        Toggle _tLowResolution;
        Toggle _tNormalResolution;
        Toggle _tHighResolution;

        Toggle _t30fps;
        Toggle _t60fps;

        Toggle _tLowQuality;
        Toggle _tMiddleQuality;
        Toggle _tHighQuality;

        Button _btnEnter;

        Text _textResolution;

        protected override void OnInit()
        {
            _btnEnter = GetChildComponent<Button>("BtnEnter");

            _tLowResolution = GetChildComponent<Toggle>("ResolutionSet/List/TLow");
            _tNormalResolution = GetChildComponent<Toggle>("ResolutionSet/List/TNormal");
            _tHighResolution = GetChildComponent<Toggle>("ResolutionSet/List/THigh");

            _t30fps = GetChildComponent<Toggle>("FPSSet/List/T30");
            _t60fps = GetChildComponent<Toggle>("FPSSet/List/T60");

            _tLowQuality = GetChildComponent<Toggle>("QualitySet/List/TLow");
            _tMiddleQuality = GetChildComponent<Toggle>("QualitySet/List/TMiddle");
            _tHighQuality = GetChildComponent<Toggle>("QualitySet/List/THigh");

            _textResolution = GetChildComponent<Text>("TextResolution");


        }

        protected override void OnEnable()
        {
            PointerClickEventListener.Get(_tLowResolution.gameObject).onEvent += SetResolution;
            PointerClickEventListener.Get(_tNormalResolution.gameObject).onEvent += SetResolution;
            PointerClickEventListener.Get(_tHighResolution.gameObject).onEvent += SetResolution;

            PointerClickEventListener.Get(_t30fps.gameObject).onEvent += UpdateSetting;
            PointerClickEventListener.Get(_t60fps.gameObject).onEvent += UpdateSetting;

            PointerClickEventListener.Get(_tLowQuality.gameObject).onEvent += UpdateSetting;
            PointerClickEventListener.Get(_tMiddleQuality.gameObject).onEvent += UpdateSetting;
            PointerClickEventListener.Get(_tHighQuality.gameObject).onEvent += UpdateSetting;

            _btnEnter.onClick.AddListener(Enter);

            SyncUI();
            UpdateSetting(null);


            //ResMgr.Ins.LoadAsync(AssetBundleName.AUDIO, "MenuBGM", onLoadedBGM);
            AudioPlayer.Ins.PlayBGM(ResMgr.Ins.Load<AudioClip>(AssetBundleName.AUDIO, "MenuBGM"));
        }

        protected override void OnDisable()
        {
            PointerClickEventListener.Get(_tLowResolution.gameObject).onEvent -= SetResolution;
            PointerClickEventListener.Get(_tNormalResolution.gameObject).onEvent -= SetResolution;
            PointerClickEventListener.Get(_tHighResolution.gameObject).onEvent -= SetResolution;

            PointerClickEventListener.Get(_t30fps.gameObject).onEvent -= UpdateSetting;
            PointerClickEventListener.Get(_t60fps.gameObject).onEvent -= UpdateSetting;

            PointerClickEventListener.Get(_tLowQuality.gameObject).onEvent -= UpdateSetting;
            PointerClickEventListener.Get(_tMiddleQuality.gameObject).onEvent -= UpdateSetting;
            PointerClickEventListener.Get(_tHighQuality.gameObject).onEvent -= UpdateSetting;

            _btnEnter.onClick.RemoveListener(Enter);
        }

        private void Enter()
        {
            UIPanelMgr.Ins.Switch<LoadingPanel>(new LoadingVO(typeof(GamePanel)));            
        }

        private void SetResolution(PointerEventData obj)
        {
            UpdateSetting(obj);
            if (Debug.isDebugBuild)
            {
                GUILog.Clear();
                GUILog.Show(string.Format("{0} 分辨率设置为：{1}", DateTime.Now.ToString("HH:mm:ss"), Global.Ins.resolutionSize));
            }
        }

        void UpdateSetting(PointerEventData obj)
        {
            Global.Ins.fps = _t30fps.isOn ? 30 : 60;

            if (_tLowQuality.isOn)
            {
                Global.Ins.quality = 0;
            }
            else if (_tMiddleQuality.isOn)
            {
                Global.Ins.quality = 1;
            }
            else if (_tHighQuality.isOn)
            {
                Global.Ins.quality = 2;
            }

            if (_tLowResolution.isOn)
            {
                Global.Ins.resolution = 0;
                Global.Ins.resolutionSize = ScreenUtil.AdaptationResolution(Global.Ins.defaultResolution.x, Global.Ins.defaultResolution.y, 1280, 720, false);
            }
            else if (_tNormalResolution.isOn)
            {
                Global.Ins.resolution = 1;
                Global.Ins.resolutionSize = ScreenUtil.AdaptationResolution(Global.Ins.defaultResolution.x, Global.Ins.defaultResolution.y, 1280, 720, true);
            }
            else if(_tHighResolution.isOn)
            {
                Global.Ins.resolution = 2;
                Global.Ins.resolutionSize = Global.Ins.defaultResolution;                
            }

            Application.targetFrameRate = Global.Ins.fps;
            QualitySettings.SetQualityLevel(Global.Ins.quality);
            Screen.SetResolution(Global.Ins.resolutionSize.x, Global.Ins.resolutionSize.y, true);
        }

        void SyncUI()
        {
            if (Global.Ins.fps == 60)
            {
                _t60fps.isOn = true;
            }
            else
            {
                _t30fps.isOn = true;
            }

            switch (Global.Ins.quality)
            {
                case 0:
                    _tLowQuality.isOn = true;
                    break;
                case 1:
                    _tMiddleQuality.isOn = true;
                    break;
                case 2:
                    _tHighQuality.isOn = true;
                    break;
            }

            switch (Global.Ins.resolution)
            {
                case 0:
                    _tLowResolution.isOn = true;
                    break;                    
                case 1:
                    _tNormalResolution.isOn = true;
                    break;
                case 2:
                    _tHighResolution.isOn = true;
                    break;
            }

            _textResolution.text = string.Format("Resolution: {0} x {1}", Screen.width, Screen.height);
        }

    }
}