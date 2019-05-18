using IL.Zero;
using UnityEngine;

namespace Knight
{
    class BlackKnight : AView
    {
        public Canvas canvas;

        protected override void OnInit()
        {
            base.OnInit();
            canvas = GetChildComponent<Canvas>("Canvas");
        }
    }
}
