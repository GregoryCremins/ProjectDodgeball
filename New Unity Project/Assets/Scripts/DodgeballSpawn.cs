using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeballSpawn : MonoBehaviour
{
    public GameObject MyGridObject;
    public GridRenderer myGridScript;
    private bool RenderedBalls = false;
    public List<GameObject> myBalls;
    public List<GameObject> activeBalls;
    // Start is called before the first frame update
    void Start()
    {
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

    public void RenderBalls()
    {
        int yOffset = 0;
        foreach (GameObject g in myBalls)
        {
            Transform t = myGridScript.myRenderedGrid[3, yOffset].transform;
            //Debug.Log(t.position);
            Vector3 localOffset = new Vector3(0f, -2f, -20f);
            Vector3 spawnPosition = t.position + localOffset;
            GameObject myNewPlayer = Instantiate(g, spawnPosition, transform.rotation);
            activeBalls.Add(myNewPlayer);
            yOffset++;
        }
    }
}
