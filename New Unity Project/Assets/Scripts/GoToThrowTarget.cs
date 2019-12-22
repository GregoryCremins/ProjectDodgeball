using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToThrowTarget : MonoBehaviour
{
    public GameObject myBoardStateObject;
    public BoardStateController myBoardState;

    public void PerformAction()
    {

        myBoardState.myEnemyTargetReticle.SetActive(true);
        
        myBoardState.myControlsObject.GetComponent<Controls>().currentTarget = myBoardState.myEnemyTargetReticle;
        myBoardState.myEnemyTargetReticle.GetComponent<Movable2>().backOption = gameObject;
        myBoardState.myEnemyTargetReticle.GetComponent<Movable2>().myBoardState = myBoardState;
        myBoardState.myEnemyTargetReticle.GetComponent<Movable2>().Activate();
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


