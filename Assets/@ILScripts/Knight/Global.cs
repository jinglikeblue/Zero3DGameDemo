using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zero;
using IL.Zero;
using IL;

namespace Knight
{
    public class Global : ASingleton<Global>
    {
        public int fps = 60;
        public int quality = 1;
        public int resolution = 1;
        public Vector2Int defaultResolution;
        public Vector2Int resolutionSize;

        public void RefreshConfig()
        {
            Application.targetFrameRate = fps;
            QualitySettings.SetQualityLevel(quality);
            switch (resolution) {
                case 0:
                    resolutionSize = ScreenUtil.AdaptationResolution(defaultResolution.x, defaultResolution.y, 1280, 720, false);
                    break;
                case 1:
                    resolutionSize = ScreenUtil.AdaptationResolution(defaultResolution.x, defaultResolution.y, 1280, 720, true);
                    break;
                case 2:
                    resolutionSize = defaultResolution;
                    break;
            }

        }
    }
}