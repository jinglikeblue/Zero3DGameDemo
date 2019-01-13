using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Jing.Components
{
    public class Joystick : MonoBehaviour
    {
        /// <summary>
        /// Stick值改变的委托
        /// </summary>
        /// <param name="value"></param>
        public delegate void OnStickValueChangeHandler(Vector2 value);

        [Header("是否绑定键盘")]
        public bool isBindKeyboard = false;
        [Header("摇杆最大半径")]
        public float maxRadius = 0;
        [Header("摇杆死区(低于这个值会被忽略)")]
        [Range(0,0.9f)]
        public float deadband = 0;
        [Header("摇杆框")]
        [SerializeField]
        Transform stickBorder;
        [Header("摇杆")]
        [SerializeField]
        Transform stick;

        /// <summary>
        /// 触摸的起始位置
        /// </summary>
        Vector3 _touchStartPos;

        /// <summary>
        /// 摇杆起始位置
        /// </summary>
        Vector3 _stickBorderInitPos;

        /// <summary>
        /// 当Stick的值改变时触发
        /// </summary>
        public OnStickValueChangeHandler onStickValueChange;

        List<KeyCode> _pressedKeyCode = new List<KeyCode>();

        Vector2 _lastValue;

        /// <summary>
        /// 摇杆的值
        /// </summary>
        public Vector2 Value
        {
            get { return _lastValue; }
        }

        bool _isStickMode = false;

        /// <summary>
        /// 当前的触摸
        /// </summary>
        int _touchId = -1;

        public int TouchId
        {
            get { return _touchId; }
        }

        void Start()
        {
           
        }

        private void OnGUI()
        {

        }

        private void FixedUpdate()
        {
            if (isBindKeyboard && _isStickMode == false)
            {
                Vector2 value = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
                stick.localPosition = value * maxRadius;
                SetValue(value);
            }
        }

        public void OnPointerDown(BaseEventData e)
        {
            _stickBorderInitPos = stickBorder.localPosition;
            BindTouch();
            stickBorder.position = GetTouchPos();

            stickBorder.GetComponent<CanvasGroup>().alpha = 0.4f;
        }

        public void OnBeginDrag(BaseEventData e)
        {
            _isStickMode = true;
            _touchStartPos = GetTouchPos();
            _touchStartPos.z = 0;
        }

        public void OnDrag(BaseEventData e)
        {
            Vector3 mousePos = GetTouchPos();
            mousePos.z = 0;

            var moveVector = (mousePos - _touchStartPos);

            if (moveVector.magnitude > maxRadius)
            {
                var k = moveVector.magnitude / maxRadius;
                moveVector /= k;
            }
            //Debug.LogFormat("start:{0}   mouse:{1}    moved:{2}", _touchStartPos, Input.mousePosition, moveVector);
            stick.localPosition = moveVector;

            Vector2 value = new Vector2(moveVector.x, moveVector.y);
            value /= maxRadius;
            if(value.magnitude < deadband)
            {
                value = Vector2.zero;
            }
            //Vector2? value;
            //if (moveVector.magnitude < minRadius)
            //{
            //    value = Vector2.zero;
            //}
            //else
            //{
            //    value = new Vector2(moveVector.x, moveVector.y);
            //    value /= maxRadius;
            //}

            SetValue(value);
        }

        void SetValue(Vector2 value)
        {
            if (_lastValue != value)
            {
                _lastValue = value;
                if (onStickValueChange != null)
                {
                    onStickValueChange(_lastValue);
                }
            }
        }

        /// <summary>
        /// 绑定触摸
        /// </summary>
        void BindTouch()
        {            
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                if (touch.phase == TouchPhase.Began)
                {
                    _touchId = touch.fingerId;
                    break;
                }
            }
        }

        /// <summary>
        /// 解绑触摸
        /// </summary>
        void UnbindTouch()
        {
            _touchId = -1;
        }

        Vector3 GetTouchPos()
        {
            if (_touchId > -1)
            {
                for (int i = 0; i < Input.touchCount; i++)
                {
                    Touch touch = Input.GetTouch(i);
                    if (touch.fingerId == _touchId)
                    {
                        return touch.position;                        
                    }
                }

                //如果上面没有返回，则这个touchId作废，
                UnbindTouch();
            }
            return Input.mousePosition;
        }



        public void OnEndDrag(BaseEventData e)
        {
            stick.localPosition = Vector3.zero;
            SetValue(Vector2.zero);
            _isStickMode = false;

            ResetStickBorder();
            UnbindTouch();
        }

        void ResetStickBorder()
        {
            stickBorder.GetComponent<CanvasGroup>().alpha = 0.2f;
            stickBorder.localPosition = _stickBorderInitPos;
        }
    }
}
