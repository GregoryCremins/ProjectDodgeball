using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NamePlateController : MonoBehaviour
{
    public int myPlayerNumber;
    public Image panelHighlight;
    // Start is called before the first frame update
    void Start()
    {
       // panelHighlight = gameObject.GetComponent<Image>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Highlight()
    {
        panelHighlight.color = new Color(1, 1, 0, .7f);
    }

    public void Unhighlight()
    {
        panelHighlight.color = new Color(1, 1, 1, .4f);
    }
}
