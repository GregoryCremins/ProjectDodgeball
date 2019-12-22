using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandom : MonoBehaviour
{
    public GameObject dodgeball;
    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {

        Invoke("Enemy1Spawn", Random.Range(-4f, 4f));


    }


    void Enemy1Spawn()
    {
        Instantiate(dodgeball, new Vector3(Random.Range(-9f, 9f), 10, 5), transform.rotation);
    }
}
