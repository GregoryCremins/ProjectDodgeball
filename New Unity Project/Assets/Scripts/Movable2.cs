using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable2 : MonoBehaviour
{
    public GameObject controlsObject;
    public GameObject backOption;
    public BoardStateController myBoardState;
    public int xCoord;
    public int yCoord;
    public int targetEnemy;

    // Start is called before the first frame update
    void Start()
    {
       // transform.position = myBoardState.getPositionOfEnemy(0);
        //xCoord = myBoardState.getGridXOfEnemy(0);
        //yCoord = myBoardState.getGridYOfEnemy(0);
        //Debug.Log("X: " + xCoord + " Y: " + yCoord);
    }

    public void Activate()
    {
        transform.position = myBoardState.getPositionOfEnemy(0);
        xCoord = myBoardState.getGridXOfEnemy(0);
        yCoord = myBoardState.getGridYOfEnemy(0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MoveRight()
    {
        if (myBoardState.myEnemyControllerObject.GetComponent<EnemyController>().activeEnemies.Count > 1)
        {
            if (targetEnemy + 1 >= myBoardState.myEnemyControllerObject.GetComponent<EnemyController>().activeEnemies.Count)
            {
                targetEnemy = 0;
            }
            else
            {
                targetEnemy = targetEnemy + 1;
            }
            transform.position = myBoardState.getPositionOfEnemy(targetEnemy);
            xCoord = myBoardState.getGridXOfEnemy(targetEnemy);
            yCoord = myBoardState.getGridYOfEnemy(targetEnemy);
        }
    }
    public void MoveLeft()
    {
        if(myBoardState.myEnemyControllerObject.GetComponent<EnemyController>().activeEnemies.Count > 1)
        {
            if(targetEnemy - 1 < 0)
            {
                targetEnemy = myBoardState.myEnemyControllerObject.GetComponent<EnemyController>().activeEnemies.Count - 1;
            }
            else
            {
                targetEnemy = targetEnemy - 1;
            }
            transform.position = myBoardState.getPositionOfEnemy(targetEnemy);
            xCoord = myBoardState.getGridXOfEnemy(targetEnemy);
            yCoord = myBoardState.getGridYOfEnemy(targetEnemy);
        }
    }

    public void MoveUp()
    {
        
    }

    public void MoveDown()
    {
        
    }

    public void PutPersonHere()
    {
    }
}
