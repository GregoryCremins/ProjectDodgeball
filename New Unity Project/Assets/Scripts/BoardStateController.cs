﻿using System.Collections;
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
                ballNumberHere = -1;
                enemyNumberHere = -1;
            }
            if(whatsHere == "Enemy")
            {
                occupied = true;
                enemyNumberHere = value;
                ballNumberHere = -1;
                playerNumberHere = -1;
            }
            if(whatsHere == "Ball")
            {
                ballNumberHere = value;
                playerNumberHere = -1;
                enemyNumberHere = -1;
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
    public GameObject ballProjectilePrefab;
    public bool grabbedGrid;
    public AudioSource pickUpBallSound;
    public AudioSource catchSound;

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
        for (int x = 0; x < 3; x++)
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
        for (int x = 0; x < 3; x++)
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

    public int GetCurrentPlayerNumber()
    {
       return playerControllerObject.GetComponent<ActivePlayerController>().currentPlayerNumber;
    }
    //enemy Positionals
    public Vector3 getPositionOfEnemy(int enemyNumber)
    {
        for (int x = 3; x < 6; x++)
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
        for (int x = 3; x < 6; x++)
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
        for (int x = 3; x < 6; x++)
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


    public (int,int) FindClosestBallToEnemy(int enemyNumber)
    {
        int closestX = -1;
        int closestY = -1;
        int closestDistance = 100000;
        int myX = -1;
        int myY = -1;
        for (int x = 3; x < 6; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                if (myGrid[x, y].enemyNumberHere == enemyNumber)
                {
                    myX = x;
                    myY = y;
                }
            }
        }
        //Debug.Log("FIND CLOSEST BALL");
        //now that we have enemies position, determine closest ball.
        for (int x = 3; x < 6; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                if (myGrid[x, y].ballNumberHere != -1)
                {
                    if (Vector2.Distance(new Vector2(x,y), new Vector2(myX, myY)) < closestDistance && myGrid[x,y].occupied == false)
                    {
                        closestX = x;
                        closestY = y;
                    }
                }
            }
        }
        return (closestX, closestY);

    }

    public void SetEnemyDefense(int enemyNumber, string defenseChoice)
    {
        myEnemyControllerObject.GetComponent<EnemyController>().SetDefense(enemyNumber, defenseChoice);
    }
    public int PickRandomAlivePlayer()
    {
        int target = playerControllerObject.GetComponent<PlayerSpawn>().pickRandomPlayer();

            return target;

    }

    public void MoveEnemy(int xCoord, int yCoord, int enemyNumber)
    {
        myGrid[getGridXOfEnemy(enemyNumber), getGridYOfEnemy(enemyNumber)].enemyNumberHere = -1;
        myGrid[xCoord, yCoord].enemyNumberHere= enemyNumber;
        myEnemyControllerObject.GetComponent<EnemyController>().MoveEnemy(enemyNumber, xCoord, yCoord);

    }

    public void EnemyPickUpBall(int myAINumber)
    {
        int targetX = getGridXOfEnemy(myAINumber);
        int targetY = getGridYOfEnemy(myAINumber);
        if (myGrid[targetX,targetY].ballNumberHere != -1)
        {
            //pick up ball
            pickUpBallSound.Play();
            int ballNumber = myGrid[targetX, targetY].ballNumberHere;
            myEnemyControllerObject.GetComponent<EnemyController>().enemyList[myAINumber].getBall(ballNumber);
            ballControllerObject.GetComponent<BallController>().EnemyPickUpBall(myAINumber, ballNumber);
            myGrid[targetX, targetY].ballNumberHere = -1;
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

    public void EnemyThrowBall(int myAINumber, int playerTarget)
    {
        //determine if we hit player
        ShootBallAtPlayer(myAINumber, playerTarget);
        CalculateHitOnPlayer(myAINumber, playerTarget);
        

    }
    public void EmptySpace(int x, int y)
    {
        myGrid[x,y].occupied = false;
        myGrid[x, y].ballNumberHere = -1;
        myGrid[x, y].playerNumberHere = -1;
        myGrid[x, y].enemyNumberHere = -1;
    }

    public float CheckIfMovePlayer(float moveDistance)
    {
        return playerControllerObject.GetComponent<PlayerVariableController>().checkIfEnoughEnergy(GetCurrentPlayerNumber(),moveDistance);
    }
    public void MovePlayer(float energyLoss)
    {
        int newX = myMovementReticle.GetComponent<Movable>().xCoord;
        int newY = myMovementReticle.GetComponent<Movable>().yCoord;
        //Debug.Log(playerControllerObject.GetComponent<ActivePlayerController>().currentPlayerNumber);
        EmptySpace(getGridXOfPlayer(playerControllerObject.GetComponent<ActivePlayerController>().currentPlayerNumber), getGridYOfPlayer(playerControllerObject.GetComponent<ActivePlayerController>().currentPlayerNumber));
        playerControllerObject.GetComponent<ActivePlayerController>().movePlayer(gridRenderSystem.GetComponent<GridRenderer>().myRenderedGrid[newX,newY],energyLoss);
        myGrid[newX, newY].occupied = true;
        myGrid[newX, newY].playerNumberHere = playerControllerObject.GetComponent<ActivePlayerController>().currentPlayerNumber;        
    }

    public void checkBallPickup (int playerNumber)
    {
        int holdingBall = playerControllerObject.GetComponent<PlayerVariableController>().myTeam[playerNumber].CheckIfHasBall();
        if (holdingBall < 0)
        {
            int myCurrentX = getGridXOfPlayer(playerNumber);
            int myCurrentY = getGridYOfPlayer(playerNumber);
            //Debug.Log(myGrid[myCurrentX, myCurrentY].ballNumberHere);
            if (myGrid[myCurrentX,myCurrentY].ballNumberHere >= 0)
            {
                pickUpBallSound.Play();
                int ballNumber = myGrid[myCurrentX, myCurrentY].ballNumberHere;
                playerControllerObject.GetComponent<PlayerVariableController>().myTeam[playerNumber].hasBall = ballNumber;
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

    public void dropBall(int ballNumber, int xPosn, int yPosn)
    {
        //Debug.Log(xPosn);
        //Debug.Log(xPosn);
        myGrid[xPosn, yPosn].ballNumberHere = ballNumber;
        playerControllerObject.GetComponent<PlayerVariableController>().myTeam[playerControllerObject.GetComponent<ActivePlayerController>().currentPlayerNumber].hasBall = -1;
        ballControllerObject.GetComponent<BallController>().DropBall(ballNumber, xPosn,yPosn);
    }

    public void ShootBall(Vector3 endpoint)
    {

        Transform startPoint = playerControllerObject.GetComponent<ActivePlayerController>().gameObject.GetComponent<PlayerSpawn>().activePlayers[playerControllerObject.GetComponent<ActivePlayerController>().currentPlayerNumber].transform;
        GameObject bullet = Instantiate(ballProjectilePrefab, startPoint.transform.position,Quaternion.identity);
        bullet.transform.position = new Vector3(bullet.transform.position.x, bullet.transform.position.y);
        endpoint = new Vector3(endpoint.x, endpoint.y);
        bullet.GetComponent<TrackShot>().target = endpoint;
    }

    public void ShootBallAtPlayer(int enemyNumber, int playerNumber)
    {
        //Debug.Log("SHOOT THE J: " + enemyNumber);
        //Debug.Log("AT: " + playerNumber);
        Transform startpoint = myEnemyControllerObject.GetComponent<EnemyController>().GetEnemyTransform(enemyNumber);
        //Debug.Log(startpoint);
        Vector3 endPoint = playerControllerObject.GetComponent<PlayerSpawn>().GetPlayerTransform(playerNumber).position;
        GameObject bullet = Instantiate(ballProjectilePrefab, startpoint.position, Quaternion.identity);
        bullet.transform.position = new Vector3(bullet.transform.position.x, bullet.transform.position.y);
        //Debug.Log(bullet.GetComponent<TrackShot>().ProjectileSpeed);
        bullet.GetComponent<TrackShot>().ProjectileSpeed = 250f;
        bullet.GetComponent<TrackShot>().target = endPoint;

    }
    public void CalculateHitOnEnemy(int playerNumber, int enemyNumber)
    {
        int throwPower = playerControllerObject.GetComponent<PlayerVariableController>().GetThrowPower(playerNumber);
        string enemyDefense = myEnemyControllerObject.GetComponent<EnemyController>().GetDefenseOption(enemyNumber);
        float enemyDefenseValue = myEnemyControllerObject.GetComponent<EnemyController>().GetDefenseStat(enemyDefense, enemyNumber);

        //holy equation:
        int toHitVal = throwPower * Random.Range(10, 20);
        float toDefendVal = enemyDefenseValue * Random.Range(5, 10);
        Debug.Log("To Hit: " + toHitVal + " vs. ToDefend:  " + toDefendVal);
        //hit
        if (toHitVal > toDefendVal)
        {
            Hit(playerControllerObject.GetComponent<PlayerVariableController>().myTeam[playerNumber].hasBall,enemyNumber);
        }
        //Defend
        else
        {
            Miss(playerControllerObject.GetComponent<PlayerVariableController>().myTeam[playerNumber].hasBall, enemyNumber);
        }
    }
    public void CalculateHitOnPlayer(int enemyNumber, int playerNumber)
    {
        float throwPower = myEnemyControllerObject.GetComponent<EnemyController>().GetPowerStat(enemyNumber);
        string targetDefense = playerControllerObject.GetComponent<PlayerVariableController>().GetDefenseOption(playerNumber);
        int targetDefenseValue = playerControllerObject.GetComponent<PlayerVariableController>().GetDefenseStat(targetDefense, playerNumber);

        //holy equation:
        float toHitVal = throwPower * Random.Range(10, 20);
        int toDefendVal = targetDefenseValue * Random.Range(5, 10);
        Debug.Log("To Hit: " + toHitVal + " vs. ToDefend:  " + toDefendVal);
        //hit
        if (toHitVal > toDefendVal)
        {
            Debug.Log("Enemy: " + enemyNumber);
            Debug.Log("player: " + playerNumber);
            HitPlayer(myEnemyControllerObject.GetComponent<EnemyController>().enemyList[enemyNumber].hasBall, playerNumber);
        }
        //Defend
        else
        {
            MissPlayer(myEnemyControllerObject.GetComponent<EnemyController>().enemyList[enemyNumber].hasBall, playerNumber);
        }
    }

    public void Hit(int ballNumber, int enemyNumber)
    {
        //deactivate enemy
        myEnemyControllerObject.GetComponent<EnemyController>().EliminateEnemy(enemyNumber);

        //drop the ball
        dropBall(ballNumber, myEnemyControllerObject.GetComponent<EnemyController>().enemyList[enemyNumber].xPosn, myEnemyControllerObject.GetComponent<EnemyController>().enemyList[enemyNumber].yPosn);

        //check for end game
        if( myEnemyControllerObject.GetComponent<EnemyController>().CheckForEnd())
        {
            Debug.Log("GAME OVER, YA WIN YA SHREK");
        }
    }
    public void HitPlayer(int ballNumber, int playerNumber)
    {
        //drop the ball
        dropBall(ballNumber, getGridXOfPlayer(playerNumber), getGridYOfPlayer(playerNumber));

        //deactivate player
        playerControllerObject.GetComponent<PlayerVariableController>().EliminatePlayer(playerNumber);

        

        //check for end game
        if (playerControllerObject.GetComponent<PlayerVariableController>().CheckForEnd())
        {
            Debug.Log("GAME OVER, YA LOSE YA SHREK");
        }
    }

    public void Miss(int ballNumber, int enemyNumber)
    {

    }
    public void MissPlayer(int ballNumber, int enemyNumber)
    {

    }

    public void PlayPlayerAnimation(int playerNumber, string playerFlag)
    {
        playerControllerObject.GetComponent<PlayerVariableController>().SetAnimationFlag(playerNumber, playerFlag);
    }
}
