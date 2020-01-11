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
    public PlayerVariableController myStats;
    // Start is called before the first frame update
    void Start()
    {
        currentPlayerNumber = -1;
        myStats = gameObject.GetComponent<PlayerVariableController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (waitingPlayerCards.Count > 0 && currentNamePlate == null)
        {
            
            currentNamePlate = waitingPlayerCards[0];
            currentPlayerNumber = currentNamePlate.GetComponent<NamePlateController>().myPlayerNumber;
            gameObject.GetComponent<PlayerVariableController>().AddEnergy(currentPlayerNumber);
            gameObject.GetComponent<PlayerVariableController>().myTeam[currentPlayerNumber].resetActions();
            currentPlayerTarget = gameObject.GetComponent<PlayerSpawn>().myPlayers[currentPlayerNumber];
            waitingPlayerCards.RemoveAt(0);
            HighlightPlayerTile();
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

    public void movePlayer(GameObject gridObject, float energyLoss)
    {
            Transform t = gridObject.transform;
            Vector3 localOffset = new Vector3(1f, -2f, -20f);
            Vector3 spawnPosition = t.position + localOffset;
            gameObject.GetComponent<PlayerSpawn>().activePlayers[currentPlayerNumber].transform.position = spawnPosition;
            gameObject.GetComponent<PlayerVariableController>().subtractFromEnergy(currentPlayerNumber, energyLoss);
            gameObject.GetComponent<PlayerVariableController>().myTeam[currentPlayerNumber].moved = true;
    }

    public void PrepCatch()
    {
        if (currentPlayerNumber >= 0)
        {
            gameObject.GetComponent<PlayerVariableController>().myTeam[currentPlayerNumber].ChooseCatch();

            UnhighlightPlayerTile();
            currentNamePlate.GetComponent<InitiativeControlller>().ResetInitiative();
            ResetStuff();
        }
    }
    public void PrepDodge()
    {
        gameObject.GetComponent<PlayerVariableController>().myTeam[currentPlayerNumber].ChooseCatch();

        UnhighlightPlayerTile();
        currentNamePlate.GetComponent<InitiativeControlller>().ResetInitiative();
        ResetStuff();
    }
}
