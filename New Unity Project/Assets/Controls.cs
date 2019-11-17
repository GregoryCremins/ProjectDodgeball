using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {

    public GameObject startTarget;
    public GameObject controllerObject;
    public GameObject mouseAndKeyBoardObject;
    // Use this for initialization
    void Start() {
        if (PlayerPrefs.GetInt("ControllerEnabled") == 1)
        {
            //set controller object
        }
		else
        if(PlayerPrefs.GetInt("MouseAndKeyboardEnabled") == 1)
        {
            //set mouseAndKeyboard Controls
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
