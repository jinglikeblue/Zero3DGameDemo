using IL;
using IL.Zero;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zero;

namespace Knight
{
    class LoadingPanel : AView
    {
        Image _bar;
        LoadingVO _vo;
        float _progress = 0f;
        protected override void OnInit()
        {
            _bar = GetChildComponent<Image>("LoadingBar/Bar");
            _bar.fillAmount = 0;
        }

        protected override void OnData(object data)
        {
            _vo = data as LoadingVO;
            StartCoroutine(SwitchDelay());
        }

        IEnumerator SwitchDelay()
        {
            yield return new WaitForSeconds(0.5f);            
            UIPanelMgr.Ins.SwitchASync(_vo.switchType, _vo.switchData, null, onProgress);
        }

        private void onProgress(float progress)
        {
            _progress = progress;
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
            if(_bar.fillAmount < _progress)
            {
                _bar.fillAmount += 0.1f;
                if(_bar.fillAmount > _progress)
                {
                    _bar.fillAmount = _progress;
                }
            }
        }
    }
}
