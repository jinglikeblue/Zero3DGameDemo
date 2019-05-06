using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightModel : MonoBehaviour {

    /// <summary>
    /// 剑
    /// </summary>
    [SerializeField]
    GameObject _swordObj;

    /// <summary>
    /// 盾牌
    /// </summary>
    [SerializeField]
    GameObject _shieldObj;

    public void SetSwordShow(bool show)
    {
        _swordObj.SetActive(show);        
    }

    void Start () {
		
	}
	
	
	void Update () {
		
	}
}
