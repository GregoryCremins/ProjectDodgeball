using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyMeterController : MonoBehaviour
{
    public GameObject myPlayerStatsObject;
    public int myPlayerNumber;
    public PlayerVariableController myVariableController;
    public Image myBarFill;
    public Slider mySlider;
    private bool filledStats = false;
    // Start is called before the first frame update
    void Start()
    {
        myVariableController = myPlayerStatsObject.GetComponent<PlayerVariableController>();
        mySlider = gameObject.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(myPlayerNumber != -1 && myVariableController.myTeam.Count >= myPlayerNumber && myVariableController.myTeam.Count > 0 && filledStats == false)
        {
            mySlider.maxValue = myVariableController.myTeam[myPlayerNumber].enduranceStat;
            //Debug.Log(myVariableController.myTeam[myPlayerNumber].enduranceStat);
            mySlider.value = mySlider.maxValue;
            filledStats = true;
        }
    }

    private void FixedUpdate()
    {
        float myValue = myVariableController.myTeam[myPlayerNumber].energy;
        float myStatValue = myVariableController.myTeam[myPlayerNumber].enduranceStat;
        mySlider.value = myValue;
        if (myValue <= (myStatValue / 10))
        {
            myBarFill.color = Color.red;
        }
        else
        {
            myBarFill.color = Color.green;
        }
    }
}
