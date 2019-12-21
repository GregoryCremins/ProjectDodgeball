using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivePlayerController : MonoBehaviour
{
    public List<GameObject> waitingPlayerCards;
    public int currentPlayerNumber;
    public GameObject currentPlayerTarget;
    public GameObject currentNamePlate;
    // Start is called before the first frame update
    void Start()
    {
        currentPlayerNumber = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (waitingPlayerCards.Count > 0 && currentNamePlate == null)
        {
            currentNamePlate = waitingPlayerCards[0];
            currentPlayerNumber = currentNamePlate.GetComponent<NamePlateController>().myPlayerNumber;
            currentPlayerTarget = gameObject.GetComponent<PlayerSpawn>().myPlayers[currentPlayerNumber];
            waitingPlayerCards.RemoveAt(0);
            HighlightPlayerTile();
        }
        if (currentPlayerTarget != null)
        {
            currentPlayerTarget.transform.position = new Vector3(100000, 10000);
        }
    }

    public void MovePlayerTowards(Vector3 newPoint)
    {
        //currentPlayerTarget.transform.position = Vector3.MoveTowards(currentPlayerTarget.transform.position, newPoint, Time.deltaTime * .01f);
    }

    public void HighlightPlayerTile()
    {
        currentNamePlate.GetComponent<NamePlateController>().Highlight();
    }
    public void UnhighlightPlayerTile()
    {
        currentNamePlate.GetComponent<NamePlateController>().Unhighlight();
    }

    public void AddToWaiting(GameObject nextWaiting)
    {
        waitingPlayerCards.Add(nextWaiting);
    }
    void ResetStuff()
    {
        currentNamePlate = null;
        currentPlayerNumber = -1;
        currentPlayerTarget = null;
    }

    public void EndTurn()
    {
        UnhighlightPlayerTile();
        currentNamePlate.GetComponent<InitiativeControlller>().ResetInitiative();
        ResetStuff();
    }

    public void movePlayer(GameObject gridObject)
    {
        
        Transform t = gridObject.transform;
        Debug.Log(t.position);
        Debug.Log(currentPlayerTarget.transform.position);
        Vector3 localOffset = new Vector3(1f, -2f, -20f);
        Vector3 spawnPosition = t.position + localOffset;
        //currentPlayerTarget.transform.position = spawnPosition;
        gameObject.GetComponent<PlayerSpawn>().activePlayers[currentPlayerNumber].transform.position = spawnPosition;
        //currentPlayerTarget.transform.position = new Vector3(15, 15);
    }
}
