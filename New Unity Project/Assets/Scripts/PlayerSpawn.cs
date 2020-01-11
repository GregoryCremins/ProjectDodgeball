using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject MyGridObject;
    public GridRenderer myGridScript;
    private bool RenderTeam = false;
    public List<GameObject> myPlayers;
    public List<GameObject> activePlayers;
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
            if(myGridScript.gridRendered == true)
            {
                RenderPlayers();
                RenderTeam = true;
            }

        }
    }

    public void RenderPlayers()
    {
        int yOffset = 0;
        foreach (GameObject g in myPlayers)
        {
            Transform t = myGridScript.myRenderedGrid[0, yOffset].transform;
            //Debug.Log(t.position);
            Vector3 localOffset = new Vector3(1f, -2f, -20f);
            Vector3 spawnPosition = t.position + localOffset;
            GameObject myNewPlayer = Instantiate(g, spawnPosition, transform.rotation);
            activePlayers.Add(myNewPlayer);
            yOffset++;
        }
    }

    public int pickRandomPlayer()
    {
        int myTarget = -1;
        List<GameObject> myPool = new List<GameObject>();
        foreach(GameObject g in activePlayers)
        {
            if(!gameObject.GetComponent<PlayerVariableController>().myTeam[g.GetComponent<Identifiers>().playerID].eliminated)
            {
                myPool.Add(g);
            }
        }
        if (myPool.Count > 0)
        {
            GameObject targetPlayer = myPool[Random.Range(0, myPool.Count)];
            myTarget = targetPlayer.GetComponent<Identifiers>().playerID;
        }

        return myTarget;
    }

    public Transform GetPlayerTransform(int playerNumber)
    {
        Transform target = null;
        foreach(GameObject g in activePlayers)
        {
            if(g.GetComponent<Identifiers>().playerID == playerNumber)
            {
                target = g.transform;
            }
        }
        return target;

    }
}
