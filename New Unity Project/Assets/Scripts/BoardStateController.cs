using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BoardStateController : MonoBehaviour
{
    [System.Serializable]
    public class GameSpace
    {
        public bool occupied;
        public int ballNumberHere;
        public int playerNumberHere;
        public int enemyNumberHere;
        public Vector3 gameSpace;

        public GameSpace()
        {
            occupied = false;
            ballNumberHere = -1;
            playerNumberHere = -1;
            enemyNumberHere = -1;
        }

        public GameSpace(string whatsHere, int value)
        {
            
            if(whatsHere == "Player")
            {
                occupied = true;
                playerNumberHere = value;
            }
            if(whatsHere == "Enemy")
            {
                occupied = true;
                enemyNumberHere = value;
            }
            if(whatsHere == "Ball")
            {
                ballNumberHere = value;
            }
        }

    }

    public GameSpace[,] myGrid;
    public GameObject gridRenderSystem;
    public GameObject playerControllerObject;
    public GameObject ballControllerObject;
    public GameObject myMovementReticle;
    public GameObject myEnemyTargetReticle;
    public GameObject myControlsObject;
    public GameObject myEnemyControllerObject;
    public bool grabbedGrid;

    // Start is called before the first frame update
    void Start()
    {
        myGrid = new GameSpace[6, 3];
        SetupBoardstate();
    }

    // Update is called once per frame
    void Update()
    {
        if(!grabbedGrid && gridRenderSystem.GetComponent<GridRenderer>().gridRendered)
        {
            grabbedGrid = true;
            myMovementReticle = gridRenderSystem.GetComponent<GridRenderer>().activeTargetReticle;
            myEnemyTargetReticle = gridRenderSystem.GetComponent<GridRenderer>().activeEnemyReticle;
            for (int x = 0; x < 6; x++)
            {
                for(int y =0; y < 3; y++)
                {
                    myGrid[x, y].gameSpace = gridRenderSystem.GetComponent<GridRenderer>().myRenderedGrid[x, y].transform.position;
                }
            }

        }
       // myMovementReticle.transform.position = myGrid[0, 0].gameSpace;
    }

    public void SetupBoardstate()
    {
        //player setup
        myGrid[0, 0] = new GameSpace("Player", 0);
        myGrid[0, 1] = new GameSpace("Player", 1);
        myGrid[0, 2] = new GameSpace("Player", 2);

        //enemy setup
        myGrid[5, 0] = new GameSpace("Enemy", 0);
        myGrid[5, 1] = new GameSpace("Enemy", 1);
        myGrid[5, 2] = new GameSpace("Enemy", 2);

        //dodgeball setup
        myGrid[3, 0] = new GameSpace("Ball", 2);
        myGrid[3, 1] = new GameSpace("Ball", 1);
        myGrid[3, 2] = new GameSpace("Ball", 0);
        myGrid[2, 2] = new GameSpace("Ball", 0);
        myGrid[2, 1] = new GameSpace("Ball", 1);
        myGrid[2, 0] = new GameSpace("Ball", 2);

        for(int x = 0; x < 6; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                if (myGrid[x,y] == null)
                {
                    myGrid[x, y] = new GameSpace();
                }
            }
        }
    }
    //player positionals
    public Vector3 getPositionOfPlayer(int playerNumber)
    {
        for (int x = 0; x < 6; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                if (myGrid[x, y].playerNumberHere == playerNumber)
                {
                    return myGrid[x, y].gameSpace;
                }
            }
        }
        return new Vector3(-10000,-10000,-10000);
    }
    public int getGridXOfPlayer(int playerNumber)
    {
        for (int x = 0; x < 6; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                if (myGrid[x, y].playerNumberHere == playerNumber)
                {
                    return x;
                }
            }
        }
        return -1;
    }
    public int getGridYOfPlayer(int playerNumber)
    {
        for (int x = 0; x < 6; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                if (myGrid[x, y].playerNumberHere == playerNumber)
                {
                    return y;
                }
            }
        }
        return -1;
    }

    //enemy Positionals
    public Vector3 getPositionOfEnemy(int enemyNumber)
    {
        for (int x = 0; x < 6; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                if (myGrid[x, y].enemyNumberHere == enemyNumber)
                {
                    return myGrid[x, y].gameSpace;
                }
            }
        }
        return new Vector3(-10000, -10000, -10000);
    }
    public int getGridXOfEnemy(int enemyNumber)
    {
        for (int x = 0; x < 6; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                if (myGrid[x, y].enemyNumberHere == enemyNumber)
                {
                    return x;
                }
            }
        }
        return -1;
    }
    public int getGridYOfEnemy(int enemyNumber)
    {
        for (int x = 0; x < 6; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                if (myGrid[x, y].enemyNumberHere == enemyNumber)
                {
                    return y;
                }
            }
        }
        return -1;
    }

    public void EmptySpace(int x, int y)
    {
        myGrid[x,y].occupied = false;
        myGrid[x, y].ballNumberHere = -1;
        myGrid[x, y].playerNumberHere = -1;
        myGrid[x, y].enemyNumberHere = -1;
    }


    public void MovePlayer()
    {
        int newX = myMovementReticle.GetComponent<Movable>().xCoord;
        int newY = myMovementReticle.GetComponent<Movable>().yCoord;
        Debug.Log(playerControllerObject.GetComponent<ActivePlayerController>().currentPlayerNumber);
        EmptySpace(getGridXOfPlayer(playerControllerObject.GetComponent<ActivePlayerController>().currentPlayerNumber), getGridYOfPlayer(playerControllerObject.GetComponent<ActivePlayerController>().currentPlayerNumber));
        playerControllerObject.GetComponent<ActivePlayerController>().movePlayer(gridRenderSystem.GetComponent<GridRenderer>().myRenderedGrid[newX,newY]);
        myGrid[newX, newY].occupied = true;
        myGrid[newX, newY].playerNumberHere = playerControllerObject.GetComponent<ActivePlayerController>().currentPlayerNumber;
        
    }

    public void checkBallPickup (int playerNumber)
    {
        Debug.Log("JOZE");
        int holdingBall = playerControllerObject.GetComponent<PlayerVariableController>().hasBall[playerNumber];
        if (holdingBall < 0)
        {
            int myCurrentX = getGridXOfPlayer(playerNumber);
            int myCurrentY = getGridYOfPlayer(playerNumber);
            Debug.Log(myGrid[myCurrentX, myCurrentY].ballNumberHere);
            if (myGrid[myCurrentX,myCurrentY].ballNumberHere >= 0)
            {
                int ballNumber = myGrid[myCurrentX, myCurrentY].ballNumberHere;
                playerControllerObject.GetComponent<PlayerVariableController>().hasBall[playerNumber] = ballNumber;
                ballControllerObject.GetComponent<BallController>().PickUpBall(playerNumber, ballNumber);
                for (int x = 0; x < 6; x++)
                {
                    for (int y = 0; y < 3; y++)
                    {
                        if (myGrid[x, y].ballNumberHere == ballNumber)
                        {
                            myGrid[x, y].ballNumberHere = -1;
                        }
                    }
                }

            }
        }
    }
}
