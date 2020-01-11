using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMenuController : MonoBehaviour
{

    public List<GameObject> mySubmenuOptions;
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject g in mySubmenuOptions)
        {
            g.SetActive(false);
        }
    }

   public void DeactivateAll()
    {
        foreach (GameObject g in mySubmenuOptions)
        {
            g.SetActive(false);
        }
    }

   public void ActivateAll()
    {
        foreach (GameObject g in mySubmenuOptions)
        {
            g.SetActive(true);
        }
    }
// Update is called once per frame
    void Update()
    {

    }

    
}
