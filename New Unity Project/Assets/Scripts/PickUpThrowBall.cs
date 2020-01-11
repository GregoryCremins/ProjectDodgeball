using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PickUpThrowBall : MonoBehaviour,Actionable
{
    public BoardStateController myBoardState;
    public GameObject myBoardStateObject;
    public string currentOption = "Pick Up";
    public GameObject myTMP;
    public TextMeshProUGUI myText;
    public int currentPlayerNumber;
    public int throwCost = 20;
    public void performAction()
    {
        currentPlayerNumber = myBoardState.playerControllerObject.GetComponent<ActivePlayerController>().currentPlayerNumber;
        if (currentPlayerNumber >= 0 && myBoardState.playerControllerObject.GetComponent<PlayerVariableController>().myTeam[currentPlayerNumber].actionTaken == false)
        {
            if (currentOption == "Pick Up")
            {
                //Debug.Log("PICK IT UP");
                //Debug.Log(myBoardState.playerControllerObject.GetComponent<ActivePlayerController>().currentPlayerNumber);
                myBoardState.checkBallPickup(myBoardState.playerControllerObject.GetComponent<ActivePlayerController>().currentPlayerNumber);
            }
            if (myBoardState.playerControllerObject.GetComponent<PlayerVariableController>().myTeam[currentPlayerNumber].energy > throwCost &&currentOption == "Throw" && currentPlayerNumber >= 0)
            {
                gameObject.GetComponent<GoToThrowTarget>().PerformAction();
            }
            
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
        if (myBoardState.playerControllerObject.GetComponent<ActivePlayerController>().currentPlayerNumber >=0 && myBoardState.playerControllerObject.GetComponent<PlayerVariableController>().myTeam[myBoardState.playerControllerObject.GetComponent<ActivePlayerController>().currentPlayerNumber].hasBall >= 0)
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
