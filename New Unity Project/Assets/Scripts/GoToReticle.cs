using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToReticle : MonoBehaviour, Actionable
{
    public GameObject myBoardStateObject;
    public BoardStateController myBoardState;



    public void performAction()
    {
        
        if (myBoardState.playerControllerObject.GetComponent<ActivePlayerController>().currentPlayerNumber >= 0 && 
            myBoardState.playerControllerObject.GetComponent<PlayerVariableController>().myTeam[myBoardState.playerControllerObject.GetComponent<ActivePlayerController>().currentPlayerNumber].moved == false)
        {
            myBoardState.myMovementReticle.SetActive(true);
            myBoardState.myControlsObject.GetComponent<Controls>().currentTarget = myBoardState.myMovementReticle;
            myBoardState.myMovementReticle.GetComponent<Movable>().backOption = gameObject;
            myBoardState.myMovementReticle.GetComponent<Movable>().myBoardState = myBoardState;
            //myBoardState.playerControllerObject.GetComponent<PlayerVariableController>().myTeam[myBoardState.playerControllerObject.GetComponent<ActivePlayerController>().currentPlayerNumber].moved = true;

        }


    }

    // Start is called before the first frame update
    void Start()
    {
        myBoardState = myBoardStateObject.GetComponent<BoardStateController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
