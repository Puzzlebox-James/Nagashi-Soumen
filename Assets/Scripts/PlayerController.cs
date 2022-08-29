using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    // Get references to stuff to use for stuff
    [SerializeField] private Transform pointOfInteractionFar;
    [SerializeField] private Transform pointOfInteractionMid;
    [SerializeField] private Transform pointOfInteractionClose;
    [SerializeField] private GameObject worldStatusCheck;
    [SerializeField] private float moveDuration;


    private Collider2D grabbableNoodleCollider;
    private Coroutine isMoving;


    private void Update()
    {
       CheckAndDeployVert();
       CheckAndDeployGrab();
    }

    // Runs every frame per Updates call
    // If there is a player input button down event and if we aren't already moving, find out which way we're going.
    // Run a check method to see if we can go that way, where we are and where we need to go, then start the move and go there.
    private void CheckAndDeployVert()
    {
        if (Input.GetButtonDown("Vertical") == true && isMoving == null)
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                if (OpenUp())
                {
                    if (transform.position == pointOfInteractionMid.position)
                    {
                        isMoving = StartCoroutine(Move(pointOfInteractionFar.position));
                    }
                    if(transform.position == pointOfInteractionClose.position)
                    {
                        isMoving = StartCoroutine(Move(pointOfInteractionMid.position));
                    }
                }
            }
            else if (Input.GetAxis("Vertical") < 0)
            {
                if (OpenDown())
                {
                    if (transform.position == pointOfInteractionMid.position)
                    {
                        isMoving = StartCoroutine(Move(pointOfInteractionClose.position));
                    }

                    if (transform.position == pointOfInteractionFar.position)
                    {
                        isMoving = StartCoroutine(Move(pointOfInteractionMid.position));
                    }
                }
            }
        }
    }
    

    private void CheckAndDeployGrab()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (grabbableNoodleCollider != null)
            {
                Destroy(grabbableNoodleCollider.gameObject);
                worldStatusCheck.GetComponent<WorldStatusScript>().NoodleScore++;
            }
            
            // DO miss stuff here
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        grabbableNoodleCollider = other;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        grabbableNoodleCollider = null;
    }


    private void CheckAndDeployFlumeClose()
    {
        // do some checking for score to close the flume
    }

    private void FlumeClose()
    {
        int flumesOpen = worldStatusCheck.GetComponent<WorldStatusScript>().FlumeStatus;
        
        if (flumesOpen == 3)
        {
            worldStatusCheck.GetComponent<WorldStatusScript>().FlumeStatus = 2;
        }

        if (flumesOpen == 2)
        {
            worldStatusCheck.GetComponent<WorldStatusScript>().FlumeStatus = 1;
        }

        if (flumesOpen == 1)
        {
            //DO vicotry stuff here? Run a win method or something?
        }
        
    }
    
    
    // Nasty Pair of methods that check to see whether the player can move up or down
    bool OpenUp()
    {
        int flumeCheck = worldStatusCheck.GetComponent<WorldStatusScript>().FlumeStatus;

        if (transform == (pointOfInteractionMid || pointOfInteractionClose) && flumeCheck == 3)
        {
            return true;
        }
        if (transform == pointOfInteractionClose && flumeCheck == 2)
        {
            return true;
        }
        
        return false;
    }
    bool OpenDown()
    {
        int flumeCheck = worldStatusCheck.GetComponent<WorldStatusScript>().FlumeStatus;

        if (transform == (pointOfInteractionMid || pointOfInteractionFar) && flumeCheck == 3)
        {
            return true;
        }
        if (transform == pointOfInteractionMid && flumeCheck == 2)
        {
            return true;
        }
        
        return false;
    }

    //This Move coroutine takes a position Vec3 argument and moves the object it's on to that location.
    //Smooth step doesn't work on Vec3, so use a Lerp with a smoothstep as the interpolation. Makes sure to end at the absolute position
    IEnumerator Move(Vector3 targetPosition)
    {
        Vector3 startPosition = transform.position;
        float lerp = 0;
        float smoothLerp;

        while (lerp < 1 && moveDuration > 0)
        {
            lerp = Mathf.MoveTowards(lerp, 1, Time.deltaTime / moveDuration);
            smoothLerp = Mathf.SmoothStep(0, 1, lerp);
            transform.position = Vector3.Lerp(startPosition, targetPosition, smoothLerp);
            yield return null;
        }

        transform.position = targetPosition;
        isMoving = null;
    }

}
