using Jing.Components;

using UnityEngine;
using UnityEngine.UI;

public class MoveDemo : MonoBehaviour
{

    public Text text;
    public Joystick moveJoystick;
    public Touchpad signTouchpad;
    public Knight knight;
    public CameraController cameraController;

    string _mousePos;
    Vector2 _move;
    Vector2 _sign;
    Vector3 _c2p;
    Vector3 _moveDir;

    void Start()
    {
        Application.targetFrameRate = 60;
        moveJoystick.onStickValueChange = OnMoveValueChange;
        signTouchpad.onValueChangeHandler = OnSignValueChange;
    }

    private void OnSignValueChange(Vector2 value)
    {
        _sign = value;
        cameraController.rotate(new Vector3(value.x, value.y, 0));
    }

    private void OnMoveValueChange(Vector2 value)
    {
        _move = value;        
    }

    /// <summary>
    /// 根据摄像机的朝向来修正相机方向
    /// </summary>
    void ReviseDirByCamera()
    {
        //人物和摄像机之间的向量
        _c2p = knight.transform.position - Camera.main.transform.position;
        _c2p.y = 0;
        //计算出这个向量和正前方的旋转角度
        Quaternion q = Quaternion.FromToRotation(Vector3.forward, _c2p);
        _moveDir = new Vector3(_move.x, 0, _move.y);
        //将移动方向沿着这个角度旋转得到最终人物真正移动的向量
        _c2p = Quaternion.AngleAxis(q.eulerAngles.y, Vector3.up) * _moveDir;
        knight.Move(_c2p);
    }

    void SyncSign()
    {

    }

    void Update()
    {
        ReviseDirByCamera();
        text.text = string.Format("joystick:{0} \n c2p:{1} \n move: {2}", moveJoystick.Value, _c2p, _moveDir);
        //text.text = string.Format("Angle:{0}", knight.transform.eulerAngles);
        //string touch = string.Format("count {0}", Input.touchCount);
        //if (Input.touchCount > 0)
        //{
        //    for (int i = 0; i < Input.touchCount; i++)
        //    {
        //        touch += string.Format("\n idx: {0} id: {1} pos: {2}", i, Input.GetTouch(i).fingerId, Input.GetTouch(i).position);
        //    }
        //}
        //text.text = string.Format("mouse:{0}\n watch:{1} \n move:{2} \n touch:{3}", Input.mousePosition, _sign, _move, touch);
    }
}
