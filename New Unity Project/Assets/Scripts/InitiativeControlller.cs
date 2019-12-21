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
    // Start is called before the first frame update
    void Start()
    {
        mySlider = gameObject.GetComponent<Slider>();
        InvokeRepeating("AddToBar", 0, .01f);
    }

    // Update is called once per frame
    void Update()
    {
        
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
        if (mySlider.value < 1) ;
        mySlider.value = mySlider.value + IncrementAmount;
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
}
