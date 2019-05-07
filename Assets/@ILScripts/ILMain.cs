using DG.Tweening;
using IL.Zero;
using Knight;
using UnityEngine;
using Zero;

namespace IL
{
    public class ILMain
    {
        public static void Main()
        {
            Global.Ins.defaultResolution.x = Screen.width;
            Global.Ins.defaultResolution.y = Screen.height;

            DOTween.defaultEaseType = Ease.Linear;
            DOTween.Init();                    
            UIPanelMgr.Ins.Init(GameObject.Find("UIPanel").transform);
            StageMgr.Ins.Init(GameObject.Find("Stage").transform);
            UIWinMgr.Ins.Init(GameObject.Find("UIWin").transform);         
            RegistViews();                              

            if (Debug.isDebugBuild)
            {
                GUIDeviceInfo.Show();
            }

            UIPanelMgr.Ins.Switch<MenuPanel>();
        }        

        static void RegistViews()
        {
            ViewFactory.Register<MenuPanel>(AssetBundleName.PREFABS, "MenuPanel");
            ViewFactory.Register<GamePanel>(AssetBundleName.PREFABS, "GamePanel");
            ViewFactory.Register<GameStage>(AssetBundleName.PREFABS, "GameStage");
        }
    }
}