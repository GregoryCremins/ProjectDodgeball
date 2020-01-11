using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{

    [System.Serializable]
    public class Ball
    {
        public bool held;
        public int playerHolding;
        public int enemyHolding;
        public int xPosn;
        public int yPosn;

        public Ball(int newX, int newY)
        {
            held = false;
            playerHolding = -1;
            enemyHolding = -1;
            xPosn = newX;
            yPosn = newY;
        }

        public void setXCoord(int newX)
        {
            xPosn = newX;
        }
        public void setYCoord(int newY)
        {
            yPosn = newY;
        }

    }
    public GameObject playerControllerObject;
    public GameObject MyGridObject;
    public GridRenderer myGridScript;
    private bool RenderedBalls = false;
    public List<GameObject> myBalls;
    public List<GameObject> myActiveBalls;
    //Using vector 2's for coordinate system
    public List<Ball> dodgeBalls;

    // Start is called before the first frame update
    void Start()
    {
        dodgeBalls = new List<Ball>(3);
        myGridScript = MyGridObject.GetComponent<GridRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        while (RenderedBalls != true)
        {
            if (myGridScript.gridRendered == true)
            {
                RenderBalls();
                RenderedBalls = true;
            }

        }
    }
    private void FixedUpdate()
    {
        for(int i = 0; i < dodgeBalls.Count; i++)
        {
            if(dodgeBalls[i].held == false && !myBalls[i].activeSelf)
            {
                DropBall(i);
            }
        }
    }

    public void RenderBalls()
    {
        int yOffset = 2;
        int index = 0;
        foreach (GameObject g in myBalls)
        {
            dodgeBalls.Add(new Ball(3, index));
            Transform t = myGridScript.myRenderedGrid[3, yOffset].transform;
            Vector3 localOffset = new Vector3(0f, -2f, -20f);
            Vector3 spawnPosition = t.position + localOffset;
            GameObject myNewBall = Instantiate(g, spawnPosition, transform.rotation);
            myActiveBalls.Add(myNewBall);
            yOffset--;
            index++;
        }
    }

    public void PickUpBall(int playerNumber, int ballNumber)
    {
        
        dodgeBalls[ballNumber].held = true;
        dodgeBalls[ballNumber].playerHolding = playerNumber;
        myActiveBalls[ballNumber].SetActive(false);
    }

    public void EnemyPickUpBall(int myAINumber, int ballNumber)
    {
        dodgeBalls[ballNumber].held = true;
        dodgeBalls[ballNumber].enemyHolding = myAINumber;
        myActiveBalls[ballNumber].SetActive(false);
    }
    public void DropBall(int ballNumber)
    {
        dodgeBalls[ballNumber].held = false;
        dodgeBalls[ballNumber].playerHolding = -1;
        Transform t = myGridScript.myRenderedGrid[dodgeBalls[ballNumber].xPosn, dodgeBalls[ballNumber].yPosn].transform;
        //Debug.Log(t.position);
        Vector3 localOffset = new Vector3(0f, -2f, -20f);
        Vector3 renderPosition = t.position + localOffset;
        myActiveBalls[ballNumber].SetActive(true);
    }

    public void DropBall(int ballNumber, int xPosn, int yPosn)
    {
        Debug.Log("HA: " + xPosn);
        Debug.Log("HA: " + yPosn);
        dodgeBalls[ballNumber].held = false;
        dodgeBalls[ballNumber].playerHolding = -1;
        dodgeBalls[ballNumber].setXCoord(xPosn);
        dodgeBalls[ballNumber].setYCoord(yPosn);
        Transform t = myGridScript.myRenderedGrid[xPosn,yPosn].transform;
        //Debug.Log(t.position);
        Vector3 localOffset = new Vector3(1f, -2f, -20f);
        Vector3 renderPosition = t.position + localOffset;
        myActiveBalls[ballNumber].transform.position = renderPosition;
        myActiveBalls[ballNumber].SetActive(true);
    }
}
