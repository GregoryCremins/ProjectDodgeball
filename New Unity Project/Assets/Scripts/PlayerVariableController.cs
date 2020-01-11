using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVariableController : MonoBehaviour
{
    [System.Serializable]
    public class Player
    {
        public int hasBall;
        public float energy;
        public int powerStat;
        public float enduranceStat;
        public int agilityStat;
        public bool eliminated;
        public bool actionTaken;
        public string defenseActionChosen;
        public bool defenseActionUsed;
        public bool moved;
        public Vector3 globalLocation;
        public int myPlayerNumber;
        public Player(int myPower, float myEndurance, int myAgility, int myPN)
        {
            hasBall = -1;
            energy = myEndurance;
            enduranceStat = myEndurance;
            powerStat = myPower;
            agilityStat = myAgility;
            defenseActionChosen = "None";
            myPlayerNumber = myPN;
        }
        
        public int CheckIfHasBall()
        {
            return hasBall;
        }

        public void resetActions()
        {
            actionTaken = false;
            moved = false;
        }
        public void ChooseCatch()
        {
            defenseActionChosen = "Catch";
        }
        public void ChooseDodge()
        {
            defenseActionChosen = "Dodge";
        }
    }

    //public int[] hasBall = new int[] { -1, -1, -1 };

    public List<Player> myTeam;
    public int movementConstant = 20;
    public int walkSpeed = 5;
    public bool initialSetup = false;
    // Start is called before the first frame update
    void Start()
    {
        myTeam.Add(new Player(50, 35, 50, 0));
        myTeam.Add(new Player(50, 20, 50, 1));
        myTeam.Add(new Player(50, 60, 50, 2));
        

    }

    // Update is called once per frame
    void Update()
    {
    }
    private void FixedUpdate()
    {
        if(!initialSetup && gameObject.GetComponent<PlayerSpawn>().activePlayers.Count > 0)
        {
            myTeam[0].globalLocation = gameObject.GetComponent<PlayerSpawn>().activePlayers[0].transform.position;
            myTeam[1].globalLocation = gameObject.GetComponent<PlayerSpawn>().activePlayers[1].transform.position;
            myTeam[2].globalLocation = gameObject.GetComponent<PlayerSpawn>().activePlayers[2].transform.position;
            initialSetup = true;
        }
        //Debug.Log(gameObject.GetComponent<PlayerSpawn>().activePlayers.Count);
        foreach(Player p in myTeam)
        {
            if (gameObject.GetComponent<PlayerSpawn>().activePlayers.Count > 0 && !p.eliminated && Vector3.Distance(gameObject.GetComponent<PlayerSpawn>().activePlayers[p.myPlayerNumber].transform.position, myTeam[p.myPlayerNumber].globalLocation) > .05f)
            {
                float step = walkSpeed* Time.deltaTime;
                gameObject.GetComponent<PlayerSpawn>().activePlayers[p.myPlayerNumber].transform.position = Vector3.MoveTowards(gameObject.GetComponent<PlayerSpawn>().activePlayers[p.myPlayerNumber].transform.position, myTeam[p.myPlayerNumber].globalLocation, step);
                if(Vector3.Distance(gameObject.GetComponent<PlayerSpawn>().activePlayers[p.myPlayerNumber].transform.position, myTeam[p.myPlayerNumber].globalLocation) <.05f)
                {
                    gameObject.GetComponent<PlayerSpawn>().activePlayers[p.myPlayerNumber].transform.position = myTeam[p.myPlayerNumber].globalLocation;
                }
            }

            //set hasball flag
            if (gameObject.GetComponent<PlayerSpawn>().activePlayers.Count > 0 && gameObject.GetComponent<PlayerSpawn>().activePlayers[p.myPlayerNumber].GetComponent<AnimationController>() != null)
            {
                if (p.hasBall != -1)
                {
                    gameObject.GetComponent<PlayerSpawn>().activePlayers[p.myPlayerNumber].GetComponent<AnimationController>().SetHasBall(true);
                }
                else
                {
                    gameObject.GetComponent<PlayerSpawn>().activePlayers[p.myPlayerNumber].GetComponent<AnimationController>().SetHasBall(false);
                }
            }
        }
    }

    public void UpdatePlayerGlobalLocation(int currentPlayerNumber, Vector3 spawnPosition)
    {
        myTeam[currentPlayerNumber].globalLocation = spawnPosition;
    }

    public string GetDefenseOption(int playerNumber)
    {
        return myTeam[playerNumber].defenseActionChosen;
    }

    public int GetDefenseStat(string targetDefense, int playerNumber)
    {
        if(targetDefense == "Catch")
        {
            return myTeam[playerNumber].powerStat;
        }
        else if (targetDefense == "Dodge")
        {
            return myTeam[playerNumber].agilityStat;
        }
        else
        {
            return 0;
        }
    }

    public void subtractFromEnergy(int playerNumber, float addValue)
    {
        if(myTeam[playerNumber].energy <= addValue)
        {
            myTeam[playerNumber].energy = 0;
        }
        else
        {
            myTeam[playerNumber].energy = myTeam[playerNumber].energy - addValue;
            //Debug.Log(myTeam[playerNumber].energy);
        }
    }

    public float checkIfEnoughEnergy(int playerNumber, float valueToCheck)
    {
        if (myTeam[playerNumber].energy >= (valueToCheck * movementConstant))
        {
            return valueToCheck * movementConstant;
        }
        else
        {
            Debug.Log("NOT ENOUGH, NEED: " + (valueToCheck * movementConstant) + ", GOT: " + myTeam[playerNumber].energy);
            return 0;
        }
    }

    public int GetThrowPower(int playerNumber)
    {
        return myTeam[playerNumber].powerStat;
    }

    public void AddEnergy(int playerNumber)
    {
        float energyToAdd = 0f;
        if (myTeam[playerNumber].energy < myTeam[playerNumber].enduranceStat)
        {
            energyToAdd = myTeam[playerNumber].enduranceStat / 10;
            if (!myTeam[playerNumber].moved)
            {
                energyToAdd = energyToAdd + myTeam[playerNumber].enduranceStat / 10;
            }
            if (myTeam[playerNumber].defenseActionChosen != "None" && !myTeam[playerNumber].defenseActionUsed)
            {
                energyToAdd = energyToAdd + myTeam[playerNumber].enduranceStat / 20;
            }
            if (!myTeam[playerNumber].moved)
            {
                energyToAdd = energyToAdd + myTeam[playerNumber].enduranceStat / 10;
            }
        }
        

        //Debug.Log("adding energy: " + energyToAdd);

        if (myTeam[playerNumber].energy + energyToAdd >= myTeam[playerNumber].enduranceStat)
        {
            myTeam[playerNumber].energy = myTeam[playerNumber].enduranceStat;
        }
        else
        {
            myTeam[playerNumber].energy = myTeam[playerNumber].energy + energyToAdd;
        }
        myTeam[playerNumber].actionTaken = false;
        myTeam[playerNumber].defenseActionChosen = "None";
        myTeam[playerNumber].defenseActionUsed = false;
        myTeam[playerNumber].moved = false;
}

    public void EliminatePlayer(int playerNumber)
    {

            myTeam[playerNumber].eliminated = true;
        gameObject.GetComponent<PlayerSpawn>().activePlayers[playerNumber].SetActive(false);
    }

    public bool CheckForEnd()
    {
        bool returnVal = true;
        for (int i = 0; i <myTeam.Count; i++)
        {
            if (!myTeam[i].eliminated)
                returnVal = false;
        }
        return returnVal;
    }


}
