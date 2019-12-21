using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject MyGridObject;
    public GridRenderer myGridScript;
    private bool RenderTeam = false;
    public List<GameObject> myEnemies;
    public List<GameObject> activeEnemies;
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
                RenderPlayers();
                RenderTeam = true;
            }

        }
    }

    public void RenderPlayers()
    {
        int yOffset = 0;
        foreach (GameObject g in myEnemies)
        {
            Transform t = myGridScript.myRenderedGrid[5, yOffset].transform;
            Debug.Log(t.position);
            Vector3 localOffset = new Vector3(1f, -2f, -20f);
            Vector3 spawnPosition = t.position + localOffset;
            GameObject myNewEnemy = Instantiate(g, spawnPosition, transform.rotation);
            activeEnemies.Add(myNewEnemy);
            yOffset++;
        }
    }
}
