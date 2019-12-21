using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurn : MonoBehaviour, Actionable
{
    public GameObject PlayerControllerObject;

    public void performAction()
    {
        if (PlayerControllerObject.GetComponent<ActivePlayerController>().currentPlayerTarget != null)
        {
            Debug.Log("Ending Turn!");
            PlayerControllerObject.GetComponent<ActivePlayerController>().EndTurn();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
