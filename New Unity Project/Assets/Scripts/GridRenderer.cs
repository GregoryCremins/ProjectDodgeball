using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridRenderer : MonoBehaviour {

    public GameObject myGridSpace;
    public GameObject moveTargetReticle;
    public RectTransform myRTransform;
    private RectTransform tempRectTransform;
    public GameObject[,] myRenderedGrid;
    public bool gridRendered = false;
    public GameObject activeTargetReticle;

    // Use this for initialization
    void Start()
    {
        myRenderedGrid = new GameObject[6,3];
        /* get the size of the image */
        myRTransform = gameObject.GetComponent<RectTransform>();
        /* render the grid */
        float width = myRTransform.rect.width;
        float height = myRTransform.rect.height;
        //Debug.Log(width);
        //Debug.Log(height);
        float heightThird = height / 3;
        float widthDiv6 = width / 6;
        int indexX = 0;
        int indexY = 0;
        /* render grid */
        for (double y = 0; y < height; y = y + heightThird)
        {
            indexX = 0;
            for (double x = (-width / 2); x < (width / 2) - 1; x= x + widthDiv6) 
            {
                GameObject myNewObj =  Instantiate(myGridSpace,gameObject.transform);
                myNewObj.transform.localPosition = new Vector3((float)x,(float)y);
                tempRectTransform = myNewObj.GetComponent<RectTransform>();
                tempRectTransform.sizeDelta = new Vector2(widthDiv6, heightThird);

                /* add grid to my rendered grid*/
                //Debug.Log(indexX + ", " + indexY);
                myRenderedGrid[indexX,indexY] = myNewObj;
                //Debug.Log(myNewObj.transform.position);
                indexX++;
            }
            indexY++;
        }
        activeTargetReticle = Instantiate(moveTargetReticle, gameObject.transform);
        //activeTargetReticle.transform.localPosition = new Vector3(2,0);
        tempRectTransform = activeTargetReticle.GetComponent<RectTransform>();
        tempRectTransform.sizeDelta = new Vector2(widthDiv6, heightThird);
        activeTargetReticle.SetActive(false);

        gridRendered = true;
    }
       
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ActivateReticle()
    {
        activeTargetReticle.SetActive(true);
    }
}
