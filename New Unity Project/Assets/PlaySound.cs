using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioSource mySound;
    private bool playedBeep = false;
    // Start is called before the first frame update
    void Start()
    {
        mySound = gameObject.GetComponent<AudioSource>();
        DontDestroyOnLoad(transform.gameObject);
    }

    public void playSound()
    {
        mySound.Play();
        Debug.Log("PLAYED THE BEEP");
        //playedBeep = true;
       
    }

    // Update is called once per frame
    void Update()
    {
        if(playedBeep == true && !mySound.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
