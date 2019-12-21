using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controllable : MonoBehaviour
{
    public GameObject upOption;
    public GameObject downOption;
    public GameObject leftOption;
    public GameObject rightOption;
    public GameObject HighlightObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Highlight()
    {
        if(HighlightObject != null)
        {
            HighlightObject.SetActive(true);
        }
    }

    public void UnHighlight()
    {
        if (HighlightObject != null)
        {
            HighlightObject.SetActive(false);
        }
    }


}
