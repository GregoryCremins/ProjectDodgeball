using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateIndefinately : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("RotateStuff", .01f,.02f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void RotateStuff()
    {
        transform.Rotate(Vector3.forward * -15);
    }
}
