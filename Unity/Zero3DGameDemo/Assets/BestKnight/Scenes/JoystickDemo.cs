using System;
using Jing.Components;
using UnityEngine;
using UnityEngine.UI;

public class JoystickDemo : MonoBehaviour {
    public Text text;
    public Joystick moveJoystick;
    public Joystick signJoystick;

    string _mousePos;
    Vector2 _move;
    Vector2 _sign;

	void Start () {
        moveJoystick.onStickValueChange = OnMoveValueChange;
        signJoystick.onStickValueChange = OnSignValueChange;
    }

    private void OnSignValueChange(Vector2 value)
    {
        _sign = value;
    }

    private void OnMoveValueChange(Vector2 value)
    {
        _move = value;        
    }

    void Update () {        
        string touch = string.Format("count {0}", Input.touchCount);
        if(Input.touchCount > 0)
        {
            for(int i = 0; i < Input.touchCount; i++)
            {
                touch += string.Format("\n idx: {0} id: {1} pos: {2}", i, Input.GetTouch(i).fingerId, Input.GetTouch(i).position);
            }
        }
        text.text = string.Format("mouse:{0}\n watch:{1} \n move:{2} \n touch:{3}", Input.mousePosition, _sign, _move, touch);
	}
}
