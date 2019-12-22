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
        public string defenseOption = "None";
        public int xPosn;
        public int yPosn;
        public int hasBall;
        public int energy;
        public bool eliminated = false;

        public Enemy(int newX, int newY)
        {
            dodgeStat = 50;
            powerStat = 50;
            enduranceStat = 50;
            energy = 100;
            xPosn = newX;
            yPosn = newY;
            hasBall = -1;
        }

        public Enemy(int newX, int newY, int newDod, int newPow, int newEnd)
        {
            dodgeStat = newDod;
            powerStat = newPow;
            enduranceStat = newEnd;
            energy = 100;
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
            //Debug.Log("ENEMY :" + yOffset + " : " + enemyList[yOffset].xPosn);
            yOffset++;
           
        }
    }


    public string GetDefenseOption(int EnemyNumber)
    {
        return enemyList[EnemyNumber].defenseOption;
    }

    public int GetDefenseStat(string defenseName, int enemyNumber)
    {
        if(defenseName == "Catch" || defenseName == "Block")
        {
            return enemyList[enemyNumber].powerStat;
        }
        if(defenseName == "Dodge")
        {
            return enemyList[enemyNumber].dodgeStat;
        }
        else
        {
            if(enemyList[enemyNumber].energy > 50)
            {
                return enemyList[enemyNumber].dodgeStat / 2;
            }
            else
            {
                return 0;
            }
        }
    }

    public void EliminateEnemy(int enemyNumber)
    {
        enemyList[enemyNumber].eliminated = true;
        activeEnemies[enemyNumber].SetActive(false);
    }

    public bool CheckForEnd()
    {
        bool returnVal = true;
        for (int i = 0; i < enemyList.Count; i++)
        {
            if (!enemyList[i].eliminated)
                returnVal = false;
        }
        return returnVal;
    }
}
