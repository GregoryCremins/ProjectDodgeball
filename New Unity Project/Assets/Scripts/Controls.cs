using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {

    public GameObject startTarget;
    public GameObject currentTarget;
    public GameObject controllerObject;
    public GameObject mouseAndKeyBoardObject;
    // Use this for initialization
    void Start() {
        currentTarget = startTarget;
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

    public void  MoveUpOption()
    {
        if (currentTarget.GetComponent<Controllable>() != null)
        {
            GameObject myTestSubject = currentTarget.GetComponent<Controllable>().upOption;
            if (myTestSubject != null)
            {
                myTestSubject.GetComponent<Controllable>().Highlight();
                currentTarget.GetComponent<Controllable>().UnHighlight();
                currentTarget = myTestSubject;
            }
        }
        
    }
    public void MoveDownOption()
    {
        if(currentTarget.GetComponent<Controllable>() != null)
        {
            GameObject myTestSubject = currentTarget.GetComponent<Controllable>().downOption;
            if (myTestSubject != null)
            {
                myTestSubject.GetComponent<Controllable>().Highlight();
                currentTarget.GetComponent<Controllable>().UnHighlight();
                currentTarget = myTestSubject;
            }
        }
    }
    public void MoveLeftOption()
    {
        if(currentTarget.GetComponent<Controllable>() != null)
        {
            GameObject myTestSubject = currentTarget.GetComponent<Controllable>().leftOption;
            if (myTestSubject != null)
            {
                myTestSubject.GetComponent<Controllable>().Highlight();
                currentTarget.GetComponent<Controllable>().UnHighlight();
                currentTarget = myTestSubject;
            }
        }
    }
    public void MoveRightOption()
    {
        if(currentTarget.GetComponent<Controllable>() != null)
        {
            GameObject myTestSubject = currentTarget.GetComponent<Controllable>().rightOption;
            if (myTestSubject != null)
            {
                myTestSubject.GetComponent<Controllable>().Highlight();
                currentTarget.GetComponent<Controllable>().UnHighlight();
                currentTarget = myTestSubject;
            }
        }
    }

    public void AdvanceOption()
    {
        if (currentTarget.GetComponent<Actionable>() != null)
        {
            currentTarget.GetComponent<Actionable>().performAction();
        }
    }
}
