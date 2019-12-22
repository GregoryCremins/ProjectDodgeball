using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVariableController : MonoBehaviour
{
    [System.Serializable]
    public class Player
    {
        public int hasBall;
        public int energy;
        public int powerStat;
        public int enduranceStat;
        public int agilityStat;
        public bool eliminated;
        public bool actionTaken;
        public bool moved;

        public Player(int myPower, int myEndurance, int myAgility)
        {
            hasBall = -1;
            energy = 100;
            powerStat = myPower;
            agilityStat = myAgility;
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
    }

    //public int[] hasBall = new int[] { -1, -1, -1 };

    public List<Player> myTeam;

    // Start is called before the first frame update
    void Start()
    {
        myTeam.Add(new Player(50, 50, 50));
        myTeam.Add(new Player(50, 50, 50));
        myTeam.Add(new Player(50, 50, 50));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addToEnergy(int playerNumber, int addValue)
    {
        
    }

    public int GetThrowPower(int playerNumber)
    {
        return myTeam[playerNumber].powerStat;
    }


    
}
