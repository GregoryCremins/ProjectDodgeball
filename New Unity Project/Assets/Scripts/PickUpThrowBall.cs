using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PickUpThrowBall : MonoBehaviour,Actionable
{
    public BoardStateController myBoardState;
    public GameObject myBoardStateObject;
    public string currentOption = "PickUp";
    public GameObject myTMP;
    public TextMeshProUGUI myText;
    public void performAction()
    {
        if(currentOption == "PickUp")
        {
            myBoardState.checkBallPickup(myBoardState.playerControllerObject.GetComponent<ActivePlayerController>().currentPlayerNumber);
        }
        if(currentOption == "Throw")
        {
           
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        myBoardState = myBoardStateObject.GetComponent<BoardStateController>();
        myText = myTMP.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (myBoardState.playerControllerObject.GetComponent<PlayerVariableController>().hasBall[myBoardState.playerControllerObject.GetComponent<ActivePlayerController>().currentPlayerNumber] >= 0)
        {
            myText.text = "Throw";
            currentOption = "Throw";
        }
        else
        {
            myText.text = "Pick Up";
            currentOption = "Pick Up";
        }
    }
}
