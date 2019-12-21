using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVariableController : MonoBehaviour
{

    public float Player1Energy = 100;
    public float Player2Energy = 100;
    public float Player3Energy = 100;

    public int[] hasBall = new int[] { -1, -1, -1 };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addToEnergy(int playerNumber, int addValue)
    {
        if (playerNumber == 1)
        {
            Player1Energy = Player1Energy + addValue;
        }
        if (playerNumber == 2)
        {
            Player2Energy = Player2Energy + addValue;
        }
        if (playerNumber == 3)
        {
            Player3Energy = Player3Energy + addValue;
        }
    }
}
