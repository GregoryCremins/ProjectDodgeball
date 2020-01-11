using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {

    public GameObject startTarget;
    public GameObject currentTarget;
    public GameObject lastMenuTarget;
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
		
        if(lastMenuTarget != currentTarget && currentTarget.GetComponent<Controllable>() != null && currentTarget.GetComponent<SubMenuOption>() == null)
        {
            lastMenuTarget = currentTarget;
        }
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

        if(currentTarget.GetComponent<Movable>() != null)
        {
            currentTarget.GetComponent<Movable>().MoveUp();
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
        if (currentTarget.GetComponent<Movable>() != null)
        {
            currentTarget.GetComponent<Movable>().MoveDown();
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
        if (currentTarget.GetComponent<Movable>() != null)
        {
            currentTarget.GetComponent<Movable>().MoveLeft();
        }
        if (currentTarget.GetComponent<Movable2>() != null)
        {
            currentTarget.GetComponent<Movable2>().MoveLeft();
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
        if (currentTarget.GetComponent<Movable>() != null)
        {
            currentTarget.GetComponent<Movable>().MoveRight();
        }
        if (currentTarget.GetComponent<Movable2>() != null)
        {
            currentTarget.GetComponent<Movable2>().MoveRight();

        }
    }

    public void AdvanceOption()
    {
        //Debug.Log("CURRENT TARGET : " + currentTarget);
        if (currentTarget.GetComponent<Actionable>() != null)
        {
            currentTarget.GetComponent<Actionable>().performAction();
            if (currentTarget.GetComponent<SubMenuOption>() != null)
            {
                //Debug.Log("RESET SHIT");
                currentTarget.GetComponent<Controllable>().UnHighlight();
                lastMenuTarget.GetComponent<SubMenuController>().DeactivateAll();
                currentTarget = lastMenuTarget;
                currentTarget.GetComponent<Controllable>().Highlight();
            }
        }
        else if (currentTarget.GetComponent<Movable>() != null)
        {
                currentTarget.GetComponent<Movable>().PutPersonHere();
        }
        else if (currentTarget.GetComponent<Movable2>() != null)
        {
                currentTarget.GetComponent<Movable2>().ThrowBall();
        }
        else if(currentTarget.GetComponent<SubMenuController>() != null)
        {
            currentTarget.GetComponent<SubMenuController>().ActivateAll();
            GameObject myTestSubject = currentTarget.GetComponent<SubMenuController>().mySubmenuOptions[0];
            myTestSubject.GetComponent<Controllable>().Highlight();
            currentTarget.GetComponent<Controllable>().UnHighlight();
            currentTarget = myTestSubject;
        }
       


    }
    public void BackOption()
    {
        Debug.Log("PRESSING BACK ON " + currentTarget);

        if (currentTarget.GetComponent<SubMenuOption>() != null)
        {
            currentTarget.GetComponent<Controllable>().UnHighlight();
            lastMenuTarget.GetComponent<SubMenuController>().DeactivateAll();
       
            currentTarget = lastMenuTarget;
            currentTarget.GetComponent<Controllable>().Highlight();
        }
        if(currentTarget.GetComponent<Movable>() != null)
        {
            Debug.Log("LAST MENU TARGET: " + lastMenuTarget);
            currentTarget.SetActive(false);
            currentTarget = startTarget;
            currentTarget.GetComponent<Controllable>().Highlight();
        }
        if (currentTarget.GetComponent<Movable2>() != null)
        {
            currentTarget.SetActive(false);
            currentTarget = lastMenuTarget;
            currentTarget.GetComponent<Controllable>().Highlight();
        }
    }
}
