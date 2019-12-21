using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [System.Serializable]
    public class Enemy
    {
        public int dodgeStat;
        public int powerStat;
        public int enduranceStat;
        public int xPosn;
        public int yPosn;
        public int hasBall;

        public Enemy(int newX, int newY)
        {
            dodgeStat = 50;
            powerStat = 50;
            enduranceStat = 50;
            xPosn = newX;
            yPosn = newY;
            hasBall = -1;
        }

        public Enemy(int newX, int newY, int newDod, int newPow, int newEnd)
        {
            dodgeStat = newDod;
            powerStat = newPow;
            enduranceStat = newEnd;
            xPosn = newX;
            yPosn = newY;
            hasBall = -1;
        }

        public void setXCoord(int newX)
        {
            xPosn = newX;
        }
        public void setYCoord(int newY)
        {
            yPosn = newY;
        }
        public void getBall(int myNewBall)
        {
            hasBall = myNewBall;
        }

    }
    public GameObject MyGridObject;
    public GridRenderer myGridScript;
    private bool RenderTeam = false;
    public List<GameObject> myEnemies;
    public List<GameObject> activeEnemies;
    public List<Enemy> enemyList;

    // Start is called before the first frame update
    void Start()
    {
        myGridScript = MyGridObject.GetComponent<GridRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        while (RenderTeam != true)
        {
            if (myGridScript.gridRendered == true)
            {
                RenderEnemies();
                RenderTeam = true;
            }

        }
    }

    public void RenderEnemies()
    {
        int yOffset = 0;
        foreach (GameObject g in myEnemies)
        {
            Transform t = myGridScript.myRenderedGrid[5, yOffset].transform;
            //Debug.Log(t.position);
            Vector3 localOffset = new Vector3(1f, -2f, -20f);
            Vector3 spawnPosition = t.position + localOffset;
            GameObject myNewEnemy = Instantiate(g, spawnPosition, transform.rotation);
            activeEnemies.Add(myNewEnemy);
            enemyList.Add(new Enemy(5, yOffset));
            yOffset++;
        }


    }
}
