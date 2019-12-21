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
    

    // Start is called before the first frame update
    void Start()
    {
        transform.position = myBoardState.getPositionOfPlayer(myBoardState.playerControllerObject.GetComponent<ActivePlayerController>().currentPlayerNumber);
        xCoord = myBoardState.getGridXOfPlayer(myBoardState.playerControllerObject.GetComponent<ActivePlayerController>().currentPlayerNumber);
        yCoord = myBoardState.getGridYOfPlayer(myBoardState.playerControllerObject.GetComponent<ActivePlayerController>().currentPlayerNumber);
        Debug.Log("X: " + xCoord + " Y: " + yCoord);
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
        if (!myBoardState.myGrid[xCoord,yCoord].occupied)
        {
            Debug.Log("ITS EMPTY");
            Debug.Log("X: " + xCoord + " Y: " + yCoord);
            myBoardState.MovePlayer();
            myBoardState.myControlsObject.GetComponent<Controls>().currentTarget = myBoardState.myControlsObject.GetComponent<Controls>().startTarget;
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("occupado");
            Debug.Log(myBoardState.myGrid[xCoord, yCoord].playerNumberHere + " ASDASD");
        }
    }
}
