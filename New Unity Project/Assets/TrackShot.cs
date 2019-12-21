using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackShot : MonoBehaviour
{
    public Vector3 target;
    public float ProjectileSpeed = .0001f;

    private Transform myTransform;

    void Awake()
    {
        myTransform = transform;
    }

    void Start()
    {
        // rotate the projectile to aim the target:
        myTransform.LookAt(target);
    }

    // Update is called once per frame
    void Update()
    {
        // distance moved since last frame:
        float amtToMove = ProjectileSpeed * Time.deltaTime;
        // translate projectile in its forward direction:
        myTransform.Translate(Vector3.forward * amtToMove);
    }
}
