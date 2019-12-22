using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackShot : MonoBehaviour
{
    public Vector3 target;
    public float ProjectileSpeed = .00000001f;
    public Vector3 prevPosition;

    private Transform myTransform;

    void Awake()
    {
        myTransform = transform;
    }

    void Start()
    {
        // rotate the projectile to aim the target:
        myTransform.LookAt(target);
        prevPosition = myTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        myTransform.LookAt(target);
        myTransform.position = new Vector3(transform.position.x, transform.position.y); 
        // distance moved since last frame:
        float amtToMove = ProjectileSpeed * Time.deltaTime;
        // translate projectile in its forward direction:
        myTransform.Translate(Vector3.forward * amtToMove);
        myTransform.position = new Vector3(myTransform.position.x, myTransform.position.y,5);

        if(Vector3.Distance(myTransform.position,prevPosition) < .05)
        {
            Destroy(gameObject);
        }
        else
        {
            prevPosition = myTransform.position;
        }
    }
}
