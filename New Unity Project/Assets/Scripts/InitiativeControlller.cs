using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitiativeControlller : MonoBehaviour
{
    public Image myBarColor;
    public Slider mySlider;
    public float IncrementAmount = .01f;
    public bool flash = false;
    public bool waitForColorChange = false;
    public GameObject playerController;
    private PlayerVariableController myVariableController;
    public bool addedToWaiting;
    public int playerNumber;
    // Start is called before the first frame update
    void Start()
    {
        mySlider = gameObject.GetComponent<Slider>();
        InvokeRepeating("AddToBar", 0, .01f);
        myVariableController = playerController.GetComponent<PlayerVariableController>();
        //Debug.Log(myVariableController.myTeam[playerNumber].enduranceStat);
        IncrementAmount = myVariableController.myTeam[playerNumber].enduranceStat / 10000;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mySlider.value >= 1 && addedToWaiting == false)
        {
            addedToWaiting = true;
            playerController.GetComponent<ActivePlayerController>().AddToWaiting(this.gameObject);
           
        }
        
    }

    private void FixedUpdate()
    {
        if (mySlider.value >= 1)
        {
            if (!waitForColorChange)
            {
                waitForColorChange = true;
                Invoke("ChangeColor",.1f);
            }
        }
        else
        {
            myBarColor.color = new Color(0, 1, 1);
        }
    }

    void AddToBar()
    {
        if (mySlider.value < 1)
        {
            mySlider.value = mySlider.value + IncrementAmount;
        }
    }

    void ChangeColor()
    {
        if (!flash)
        {
            myBarColor.color = new Color(1, 1, 0);
            flash = true;
        }
        else
        {
            myBarColor.color = new Color(0, 1, 1);
            flash = false;
        }
        waitForColorChange = false;
    }

    public void ResetInitiative()
    {
        mySlider.value = 0;
        addedToWaiting = false;
    }
}
