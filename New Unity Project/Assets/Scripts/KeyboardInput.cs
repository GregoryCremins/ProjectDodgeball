using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour
{

    public Controls myControlObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // up input
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            myControlObject.MoveUpOption();
        }
        // down input
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            myControlObject.MoveDownOption();
        }
        // left input
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            myControlObject.MoveLeftOption();
        }

        // Right input
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            myControlObject.MoveRightOption();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            myControlObject.AdvanceOption();
        }


    }
}
