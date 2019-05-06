using UnityEngine;
using UnityEngine.EventSystems;

namespace GameKit
{
    public class Touchpad : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        /// <summary>
        /// Touchpad值改变的委托
        /// </summary>
        /// <param name="value"></param>
        public delegate void OnValueChangeHandler(Vector2 value);

        /// <summary>
        /// 当Stick的值改变时触发
        /// </summary>
        public OnValueChangeHandler onValueChangeHandler;

        void Start()
        {

        }


        void Update()
        {

        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            
        }

        public void OnDrag(PointerEventData eventData)
        {
            MoveDelta(eventData.delta);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            
        }

        public void MoveDelta(Vector2 delta)
        {
            if(null != delta)
            {
                if (null != onValueChangeHandler)
                {
                    onValueChangeHandler(delta);
                }
            }
        }
    }
}
