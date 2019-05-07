using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IL
{
    public class ScreenUtil
    {
        /// <summary>
        /// 适配
        /// </summary>
        /// <param name="sW"></param>
        /// <param name="sH"></param>
        /// <param name="tW"></param>
        /// <param name="tH"></param>
        /// <returns></returns>
        public static Vector2Int AdaptationResolution(int sW, int sH, int tW, int tH)
        {
            //当前宽高比
            float sK = (float)sW / sH;
            //目标宽高比
            float tK = (float)tW / tH;

            Vector2Int fit = new Vector2Int();

            if (sK > tK)
            {
                //以目标宽度矫正分辨率
                fit.x = tW;
                fit.y = (int)(tW / sK);
            }
            else
            {
                //以目标高度矫正分辨率
                fit.x = (int)(tH * sK);
                fit.y = tH;                
            }

            return fit;
        }
    }
}