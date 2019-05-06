﻿using System;
using IL.Zero;
using Zero;

namespace IL
{
    class LoadingWin:AView
    {
        public event Action onSwitch;
        public event Action onOver;

        AnimationCallback _acb;

        protected override void OnInit()
        {
            base.OnInit();
            _acb = GetComponent<AnimationCallback>();            
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _acb.onCallback += OnCallback;
        }        

        protected override void OnDisable()
        {
            base.OnDisable();
            _acb.onCallback -= OnCallback;
        }

        private void OnCallback(string tag)
        {
            switch (tag)
            {
                case "switch":
                    onSwitch?.Invoke();
                    break;
                case "over":
                    onOver?.Invoke();
                    Destroy();
                    break;
            }
        }
    }
}
