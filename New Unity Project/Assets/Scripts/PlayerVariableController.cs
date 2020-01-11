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
        public Player(int myPower, float myEndurance, int myAgility)
        {
            hasBall = -1;
            energy = myEndurance;
            enduranceStat = myEndurance;
            powerStat = myPower;
            agilityStat = myAgility;
            defenseActionChosen = "None";
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

    // Start is called before the first frame update
    void Start()
    {
        myTeam.Add(new Player(50, 35, 50));
        myTeam.Add(new Player(50, 20, 50));
        myTeam.Add(new Player(50, 60, 50));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void subtractFromEnergy(int playerNumber, int addValue)
    {
        if(myTeam[playerNumber].energy <= addValue)
        {
            myTeam[playerNumber].energy = 0;
        }
        else
        {
            myTeam[playerNumber].energy = myTeam[playerNumber].energy - addValue;
            Debug.Log(myTeam[playerNumber].energy);
        }
    }

    public int checkIfEnoughEnergy(int playerNumber, int valueToCheck)
    {
        if (myTeam[playerNumber].energy > (valueToCheck * movementConstant))
        {
            return valueToCheck * movementConstant;
        }
        else
        {
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




}
