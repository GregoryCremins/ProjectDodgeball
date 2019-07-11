using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridRenderer : MonoBehaviour {

    public GameObject myGridSpace;
    private GridLayoutGroup myGrid;

    // Use this for initialization
    void Start () {
        /* render the grid */
        myGrid = gameObject.GetComponent<GridLayoutGroup>();
        for(int i = 0; i < 9; i++)
        {
            GameObject newGridSpace = Instantiate(myGridSpace);
            newGridSpace.transform.SetParent(transform);
            
        }

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
