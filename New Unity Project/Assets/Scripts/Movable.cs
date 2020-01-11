using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : MonoBehaviour
{

    public GameObject controlsObject;
    public GameObject backOption;
    public BoardStateController myBoardState;
    public int xCoord;
    public int yCoord;
    public int playerXCoord;
    public int playerYCoord;
    private bool firstFind = false;
    

    // Start is called before the first frame update
    void Start()
    {
        transform.position = myBoardState.getPositionOfPlayer(myBoardState.playerControllerObject.GetComponent<ActivePlayerController>().currentPlayerNumber);
        xCoord = myBoardState.getGridXOfPlayer(myBoardState.playerControllerObject.GetComponent<ActivePlayerController>().currentPlayerNumber);
        yCoord = myBoardState.getGridYOfPlayer(myBoardState.playerControllerObject.GetComponent<ActivePlayerController>().currentPlayerNumber);
        playerXCoord = xCoord;
        playerYCoord = yCoord;
        firstFind = true;
        Debug.Log("X: " + xCoord + " Y: " + yCoord);
    }

    private void OnEnable()
    {
        if (firstFind)
        {
            transform.position = myBoardState.getPositionOfPlayer(myBoardState.playerControllerObject.GetComponent<ActivePlayerController>().currentPlayerNumber);
            xCoord = myBoardState.getGridXOfPlayer(myBoardState.playerControllerObject.GetComponent<ActivePlayerController>().currentPlayerNumber);
            yCoord = myBoardState.getGridYOfPlayer(myBoardState.playerControllerObject.GetComponent<ActivePlayerController>().currentPlayerNumber);
            playerXCoord = xCoord;
            playerYCoord = yCoord;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveRight()
    {
        if (xCoord < 2)
        {
            xCoord = xCoord + 1;
            transform.position = myBoardState.myGrid[xCoord, yCoord].gameSpace;
        }
    }
    public void MoveLeft()
    {
        if (xCoord > 0)
        {
            xCoord = xCoord - 1;
            transform.position = myBoardState.myGrid[xCoord, yCoord].gameSpace;
        }
    }

    public void MoveUp()
    {
        if (yCoord < 2)
        {
            yCoord = yCoord + 1;
            transform.position = myBoardState.myGrid[xCoord, yCoord].gameSpace;
        }
    }

    public void MoveDown()
    {
        if (yCoord > 0)
        {
            yCoord = yCoord - 1;
            transform.position = myBoardState.myGrid[xCoord, yCoord].gameSpace;
        }
    }

    public void PutPersonHere()
    {
        float moveDistance = Vector2.Distance(new Vector2(xCoord, yCoord), new Vector2(myBoardState.getGridXOfPlayer(myBoardState.GetCurrentPlayerNumber()), myBoardState.getGridYOfPlayer(myBoardState.GetCurrentPlayerNumber())));
        //int moveDistance = Mathf.Abs(myBoardState.getGridXOfPlayer(myBoardState.GetCurrentPlayerNumber()) - xCoord) + Mathf.Abs(myBoardState.getGridYOfPlayer(myBoardState.GetCurrentPlayerNumber()) - yCoord);
        float energyLoss = myBoardState.CheckIfMovePlayer(moveDistance);
        if (energyLoss != 0)
        {
            if (!myBoardState.myGrid[xCoord, yCoord].occupied)
            {
                //Debug.Log("ITS EMPTY");
                //Debug.Log("X: " + xCoord + " Y: " + yCoord);
                myBoardState.MovePlayer(energyLoss);
                myBoardState.myControlsObject.GetComponent<Controls>().currentTarget = myBoardState.myControlsObject.GetComponent<Controls>().startTarget;
                myBoardState.playerControllerObject.GetComponent<PlayerVariableController>().myTeam[myBoardState.playerControllerObject.GetComponent<ActivePlayerController>().currentPlayerNumber].moved = true;
                gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("occupado");
                Debug.Log(myBoardState.myGrid[xCoord, yCoord].playerNumberHere + " ASDASD");
            }
        }
        else
        {
            Debug.Log("NOT ENOUGH ENERGY");
        }
    }
}
