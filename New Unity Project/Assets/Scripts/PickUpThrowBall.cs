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
    public void performAction()
    {
        Debug.Log("ASASASD");
        if (currentOption == "Pick Up")
        {
            Debug.Log("PICK IT UP");
            myBoardState.checkBallPickup(myBoardState.playerControllerObject.GetComponent<ActivePlayerController>().currentPlayerNumber);
        }
        if(currentOption == "Throw")
        {
            gameObject.GetComponent<GoToThrowTarget>().PerformAction();
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
        if (myBoardState.playerControllerObject.GetComponent<ActivePlayerController>().currentPlayerNumber >=0 && myBoardState.playerControllerObject.GetComponent<PlayerVariableController>().hasBall[myBoardState.playerControllerObject.GetComponent<ActivePlayerController>().currentPlayerNumber] >= 0)
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
