using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameKit
{
    public class Joystick : MonoBehaviour,
        IPointerDownHandler,
        IBeginDragHandler,
        IDragHandler,
        IEndDragHandler
    {

        [Header("摇杆最大半径(UGUI)")]
        public float maxRadius = 0;
        [Header("摇杆最小半径(UGUI)")]
        public float minRadius = 0;
        [Header("摇杆框")]
        public Transform stickBorder;
        [Header("摇杆")]
        public Transform stick;

        /// <summary>
        /// 绑定的相机
        /// </summary>
        public Camera uiCamera;

        /// <summary>
        /// 触摸的起始位置
        /// </summary>
        Vector2 _touchStartPos;

        /// <summary>
        /// 摇杆起始位置
        /// </summary>
        Vector3 _stickBorderInitPos;

        /// <summary>
        /// 当Stick的值改变时触发
        /// </summary>
        public event Action<Vector2> onValueChange;

        List<KeyCode> _pressedKeyCode = new List<KeyCode>();

        public Vector2 Value { get; private set; }

        bool _isStickMode = false;

        /// <summary>
        /// 触摸ID
        /// </summary>
        int _touchId = -1;

        void Start()
        {
            _stickBorderInitPos = stickBorder.position;
        }

        private void Update()
        {
            if (_isStickMode == false)
            {
                CheckKeyPress(KeyCode.UpArrow);
                CheckKeyPress(KeyCode.DownArrow);
                CheckKeyPress(KeyCode.LeftArrow);
                CheckKeyPress(KeyCode.RightArrow);

                Vector2 tempValue = Vector2.zero;
                if (_pressedKeyCode.Count > 0)
                {
                    stickBorder.gameObject.SetActive(false);
                    switch (_pressedKeyCode[_pressedKeyCode.Count - 1])
                    {
                        case KeyCode.UpArrow:
                            tempValue = Vector2.up;
                            break;
                        case KeyCode.DownArrow:
                            tempValue = Vector2.down;
                            break;
                        case KeyCode.LeftArrow:
                            tempValue = Vector2.left;
                            break;
                        case KeyCode.RightArrow:
                            tempValue = Vector2.right;
                            break;
                    }
                }
                else
                {
                    stickBorder.gameObject.SetActive(true);
                }

                SetValue(tempValue);
            }
        }

        void SetValue(Vector2 value)
        {
            if (Value != value)
            {
                Value = value;
                onValueChange?.Invoke(Value);
            }
        }

        void CheckKeyPress(KeyCode keyCode)
        {
            if (Input.GetKeyDown(keyCode))
            {
                _pressedKeyCode.Add(keyCode);
            }

            if (Input.GetKeyUp(keyCode))
            {
                _pressedKeyCode.Remove(keyCode);
            }
        }

        /// <summary>
        /// 得到指定GameObject下，鼠标相对的localposition坐标
        /// </summary>
        /// <param name="go"></param>
        /// <returns></returns>
        Vector2 GetLocalMousePosition(GameObject go)
        {
            if (null == uiCamera)
            {
                throw new Exception("Joystick need binding a camera");
            }

            Vector3 mousePos = Input.mousePosition;
            if (Input.touchCount > 0)
            {
                bool flag = false;
                //取得绑定的手指的位置
                for (int i = 0; i < Input.touchCount; i++)
                {
                    if (Input.touches[i].fingerId == _touchId)
                    {
                        flag = true;
                        mousePos = Input.touches[i].position;
                        break;
                    }
                }

                if (false == flag)
                {
                    Debug.Log("Fuck");
                }
            }
            Vector2 screenPos = new Vector2(mousePos.x, mousePos.y);
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(go.GetComponent<RectTransform>(), screenPos, uiCamera, out localPoint);

            //Debug.LogFormat("Mouse:{0}  Screen:{1}  LocalPoint:{2}", Input.mousePosition, screenMouse, localPoint);
            return localPoint;
        }

        void ResetStickBorder()
        {
            stickBorder.GetComponent<CanvasGroup>().alpha = 0.2f;
            stickBorder.position = _stickBorderInitPos;
        }

        /// <summary>
        /// 绑定触摸
        /// </summary>
        bool BindTouch()
        {
            if (_touchId == -1)
            {
                //只能绑定一个手指
                for (int i = 0; i < Input.touchCount; i++)
                {
                    Touch touch = Input.touches[i];
                    if (touch.phase == TouchPhase.Began)
                    {
                        _touchId = touch.fingerId;
                        //Debug.LogFormat("绑定TouchId:{0}", _touchId);
                        return true;
                    }
                }
            }
            return false;
        }

        void UnbindTouch()
        {
            //Debug.LogFormat("解绑TouchId:{0}", _touchId);
            _touchId = -1;
        }


        public void OnPointerDown(PointerEventData eventData)
        {
            BindTouch();

            if (eventData.pointerId != _touchId)
            {
                return;
            }

            stickBorder.localPosition = GetLocalMousePosition(gameObject);
            stickBorder.GetComponent<CanvasGroup>().alpha = 0.4f;

        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (eventData.pointerId != _touchId)
            {
                return;
            }

            _isStickMode = true;

            _touchStartPos = GetLocalMousePosition(stickBorder.gameObject);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (eventData.pointerId != _touchId)
            {
                return;
            }

            stick.localPosition = Vector3.zero;
            onValueChange?.Invoke(Vector2.zero);
            _isStickMode = false;

            ResetStickBorder();
            UnbindTouch();
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (eventData.pointerId != _touchId)
            {
                return;
            }

            Vector2 touchNowPos = GetLocalMousePosition(stickBorder.gameObject);

            var moveVector = (touchNowPos - _touchStartPos);
            //Debug.LogFormat("start:{0}   mouse:{1}    moved:{2}", _touchStartPos, Input.mousePosition, moveVector);
            if (moveVector.magnitude > maxRadius)
            {
                var k = moveVector.magnitude / maxRadius;
                moveVector /= k;
            }
            stick.localPosition = moveVector;

            Vector2 value = Vector2.zero;
            if (moveVector.magnitude >= minRadius)
            {
                value = new Vector2(moveVector.x, moveVector.y);
            }

            SetValue(value);
        }
    }
}
