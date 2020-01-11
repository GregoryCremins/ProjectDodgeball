using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchChoice : MonoBehaviour, Actionable
{
    public GameObject PlayerControllerObject;

    public void performAction()
    {

        if (PlayerControllerObject.GetComponent<ActivePlayerController>().currentPlayerTarget != null)
        {
            PlayerControllerObject.GetComponent<ActivePlayerController>().PrepCatch();
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
