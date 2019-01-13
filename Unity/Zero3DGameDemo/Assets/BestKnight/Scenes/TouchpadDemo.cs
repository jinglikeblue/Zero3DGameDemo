using Jing.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchpadDemo : MonoBehaviour {

    public Text text;
    public Touchpad touchpad;

	void Start () {
        touchpad.onValueChangeHandler += onValueChangeHandler;
	}

    private void onValueChangeHandler(Vector2 value)
    {
        text.text = value.ToString();
    }

    void Update () {
		
	}
}
