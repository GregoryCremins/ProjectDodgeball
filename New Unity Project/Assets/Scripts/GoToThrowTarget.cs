using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToThrowTarget : MonoBehaviour,Actionable
{
    public GameObject myBoardStateObject;
    public BoardStateController myBoardState;

    public void performAction()
    {

        myBoardState.myEnemyTargetReticle.SetActive(true);
        myBoardState.myEnemyTargetReticle.GetComponent<Movable2>().Activate();
        myBoardState.myControlsObject.GetComponent<Controls>().currentTarget = myBoardState.myEnemyTargetReticle;
        myBoardState.myMovementReticle.GetComponent<Movable>().backOption = gameObject;
        myBoardState.myMovementReticle.GetComponent<Movable>().myBoardState = myBoardState;
    }

    // Start is called before the first frame update
    void Start()
    {
        myBoardState = myBoardStateObject.GetComponent<BoardStateController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}


