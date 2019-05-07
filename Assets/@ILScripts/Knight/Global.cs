using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zero;
using IL.Zero;

namespace Knight
{
    public class Global : ASingleton<Global>
    {
        public int fps = 30;
        public int quality = 0;
        public int resolution = 1;
        public Vector2Int defaultResolution;
        public Vector2Int resolutionSize;
    }
}