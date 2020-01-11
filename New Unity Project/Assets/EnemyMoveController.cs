using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveController : MonoBehaviour
{
    public class AI
    {
        public float maxInitiatve;
        public float currentInitative;
        public float initiativeStep;
        public string aiControl;
        public int myAINumber;

        public AI(int MaxInitiative, int initiativeStep, int myNewAINumber, string aiChoice)
        {
            aiControl = aiChoice;
            maxInitiatve = MaxInitiative;
            currentInitative = 0;
            myAINumber = myNewAINumber;

        }

        public void IncrementInitiative()
        {
            currentInitative = currentInitative + initiativeStep;
        }

        public bool CheckIfReady()
        {
            return maxInitiatve < currentInitative;
        }

        public string MakeAIDecision(BoardStateController myBoardState)
        {
            string instructions = "";
            //find nearest ball;

            (int, int) myClosestBall = myBoardState.FindClosestBallToEnemy(myAINumber);

            //simple AI decisions
            //1) If no ball, get ball
            //2) If no ball available, and not have ball, prep dodge/catch
            //3) If have ball, throw
            if(aiControl == "Simple")
            {
                if(myBoardState.myEnemyControllerObject.GetComponent<EnemyController>().enemyList[myAINumber].hasBall == -1 && myClosestBall != (-1,-1))
                {
                    //get ball
                    instructions = "Move:" + myClosestBall + ";Pickup"; 
                }
                else if (myBoardState.myEnemyControllerObject.GetComponent<EnemyController>().enemyList[myAINumber].hasBall == -1 && myClosestBall == (-1, -1))
                {
                    //prep defense
                    if(myBoardState.myEnemyControllerObject.GetComponent<EnemyController>().enemyList[myAINumber].agilityStat > myBoardState.myEnemyControllerObject.GetComponent<EnemyController>().enemyList[myAINumber].powerStat)
                    {
                        instructions = "Dodge";
                    }
                    else
                    {
                        instructions = "Catch";
                    }
                    
                }
                else if (myBoardState.myEnemyControllerObject.GetComponent<EnemyController>().enemyList[myAINumber].hasBall != -1)
                {
                    instructions = "Throw:"+myBoardState.PickRandomAlivePlayer();
                }
                else
                {
                    instructions = "ERROR";
                }

            }
            else
            {
                return "AI NOT BUILT YET";
            }

            return instructions;


        }

    }

    public List<AI> myPassiveEnemyAIs;
    public List<AI> myWaitingEnemyAIs;
    public bool firstFill = false;
    public bool doingSomething = false;
    public GameObject boardStateObject;
    public BoardStateController myBoardState;

    // Start is called before the first frame update
    void Start()
    {
        myPassiveEnemyAIs = new List<AI>();
        myWaitingEnemyAIs = new List<AI>();
        myBoardState = boardStateObject.GetComponent<BoardStateController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (myPassiveEnemyAIs.Count == 0 && firstFill == false)
        {
            if(gameObject.GetComponent<EnemyController>().enemyList.Count == 3)
            {
                int i = 0;
                foreach (EnemyController.Enemy e in gameObject.GetComponent<EnemyController>().enemyList)
                {
                    myPassiveEnemyAIs.Add(new AI(100, e.agilityStat / 1000, i, "Basic"));
                    i++;
                }
                firstFill = true;
            }
        }
        foreach (AI testAI in myPassiveEnemyAIs)
        {
            testAI.IncrementInitiative();
            if(testAI.CheckIfReady())
            {
                myWaitingEnemyAIs.Add(testAI);
                myPassiveEnemyAIs.Remove(testAI);
            }
        }

        if(myWaitingEnemyAIs.Count > 0 && !doingSomething)
        {
            QueueUpAction(myWaitingEnemyAIs[0]);
            doingSomething = true;
            Invoke("ResetDoSomething",2);
        }
    }
    
    public void ResetDoSomething()
    {
        doingSomething = false;
    }

    public void QueueUpAction(AI myAI)
    {
        string action = myAI.MakeAIDecision(myBoardState);
        string[] instructions = action.Split(';');
        foreach (string instruction in instructions)
        {
            if(instruction.Contains("Move"))
            {
                int moveX = int.Parse(instruction.Substring(5, 1));
                int moveY = int.Parse(instruction.Substring(7, 1));
                boardStateObject.GetComponent<BoardStateController>().MoveEnemy(moveX, moveY, myAI.myAINumber);
            }
            if(instruction.Contains("Pickup"))
            {
                boardStateObject.GetComponent<BoardStateController>().EnemyPickUpBall(myAI.myAINumber);
            }
            if(instruction.Contains("Throw"))
            {
                int throwTarget = int.Parse(instruction.Split(':')[1]);
                boardStateObject.GetComponent<BoardStateController>().EnemyThrowBall(myAI.myAINumber, throwTarget);
            }
        }
    }


}
