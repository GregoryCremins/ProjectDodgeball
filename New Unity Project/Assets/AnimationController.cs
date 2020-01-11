using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public GameObject myAnimationObject;
    public Animator myAnimator;
    public bool inPosition;
    public bool isEliminated;
    public Vector3 prevPosition;
    public float speed = 20;
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = myAnimationObject.GetComponent<Animator>();
        InvokeRepeating("CheckWalking", .01f, .01f);
        prevPosition = gameObject.GetComponentInParent<Transform>().position;
        // myAnimator.SetBool("Walking", true);

    }

    // Update is called once per frame
    void Update()
    {
     

    }

    private void FixedUpdate()
    {
        if(isEliminated)
        {
            myAnimator.SetBool("Hit", true);
        }
        else
        {
            
                if (myAnimator.GetBool("Throwing") == true && myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Throw"))
                {
                    myAnimator.SetBool("Throwing",false);
                }
                else if (myAnimator.GetBool("Dodge") == true && myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Dodge"))
                {
                    myAnimator.SetBool("Dodge", false);
                }
                else if (myAnimator.GetBool("Pickup") == true && myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Pickup"))
                {
                    myAnimator.SetBool("Pickup" +
                        "", false);
                }
        }


    }

    public void CheckWalking()
    {
        //Debug.Log(prevPosition);
        Vector3 currentPosition = gameObject.GetComponentInParent<Transform>().position;
        Debug.Log(currentPosition);
        Debug.Log(currentPosition == prevPosition);
        if (prevPosition != currentPosition)
        {
            //Vector3 targetPosn = GetComponentInParent<Transform>().position;
            // float step = speed * Time.deltaTime;
            //transform.position = Vector3.MoveTowards(transform.position, targetPosn, step);

            myAnimator.SetBool("Walk", true);
            prevPosition = currentPosition;
        }
        else
        {
            myAnimator.SetBool("Walk", false);
        }
    }

    public void SetHasBall(bool hasBall)
    {
        myAnimator.SetBool("Has Ball", hasBall);
    }


}
