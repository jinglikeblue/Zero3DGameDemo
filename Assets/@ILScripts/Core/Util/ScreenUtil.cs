using UnityEngine;

namespace IL
{
    public class ScreenUtil
    {
        /// <summary>
        /// 适配分辨率，在保持原始宽高比的情况下，尽量使分辨率与期望分辨率匹配（至少一条边相同）
        /// </summary>
        /// <param name="sW">原始宽度</param>
        /// <param name="sH">原始高度</param>
        /// <param name="tW">适配宽度（期望）</param>
        /// <param name="tH">适配高度（期望）</param>
        /// <param name="isUpper">true：适配后宽高总是大于等于期望值   false：适配后宽高总是小于等于期望值</param>
        /// <returns></returns>
        public static Vector2Int AdaptationResolution(int sW, int sH, int tW, int tH, bool isUpper)
        {
            Vector2Int fit = new Vector2Int();

            //当前宽高比
            float sK = (float)sW / sH;
            //目标宽高比
            float tK = (float)tW / tH;

            //0:直接使用适配宽高   1：使用适配宽度矫正分辨率   2：使用适配高度矫正分辨率
            int fixType = 0;

            if (sK > tK)
            {              
                if(isUpper)
                {
                    fixType = 2;
                }
                else
                {
                    fixType = 1;
                }                
            }
            else if(sK < tK)
            {
                if (isUpper)
                {
                    fixType = 1;
                }
                else
                {
                    fixType = 2;
                }
            }

            switch (fixType)
            {
                case 0:
                    fit.x = tW;
                    fit.y = tH;
                    break;               
                case 1:
                    //以目标宽度矫正分辨率
                    fit.x = tW;
                    fit.y = (int)(tW / sK);
                    break;
                case 2:
                    //以目标高度矫正分辨率
                    fit.x = (int)(tH * sK);
                    fit.y = tH;
                    break;
            }

            return fit;
        }
    }
}