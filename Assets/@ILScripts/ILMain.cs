using DG.Tweening;
using IL.Zero;
using Jing;
using System.Text;
using UnityEngine;
using Zero;

namespace IL
{
    public class ILMain
    {
        public static void Main()
        {            
            DOTween.defaultEaseType = Ease.Linear;
            DOTween.Init();
            Application.targetFrameRate = 60;            
            UIPanelMgr.Ins.Init(GameObject.Find("UIPanel").transform);
            StageMgr.Ins.Init(GameObject.Find("Stage").transform);
            UIWinMgr.Ins.Init(GameObject.Find("UIWin").transform);         
            RegistViews();                              

            if (Debug.isDebugBuild)
            {
                GUIDeviceInfo.Show();
            }

            UIPanelMgr.Ins.Switch<GamePanel>();
        }        

        static void RegistViews()
        {
            ViewFactory.Register<GamePanel>(AssetBundleName.PREFABS, "GamePanel");
            ViewFactory.Register<GameStage>(AssetBundleName.PREFABS, "GameStage");
        }
    }
}