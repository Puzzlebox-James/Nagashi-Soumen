using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;

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
       CheckAndDeployFlumeClose();
    }

    // Runs every frame per Updates call
    // If there is a player input button down event and if we aren't already moving, find out which way we're going.
    // Run a check method to see if we can go that way, where we are and where we need to go, then start the move and go there.
    private void CheckAndDeployVert()
    {
        if (Input.GetButtonDown("Vertical") != true) return;
        if (Input.GetAxis("Vertical") > 0)
        {
            if (!OpenUp()) return;
            if (transform.position == pointOfInteractionMid.position)
            {
                isMoving = StartCoroutine(Move(pointOfInteractionFar.position));
            }
            if(transform.position == pointOfInteractionClose.position)
            {
                Debug.Log("yep");
                isMoving = StartCoroutine(Move(pointOfInteractionMid.position));
            }
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            if (!OpenDown()) return;
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
        var flumesOpen = worldStatusCheck.GetComponent<WorldStatusScript>().FlumeStatus;
        var noodleScore = worldStatusCheck.GetComponent<WorldStatusScript>().NoodleScore;

        switch (flumesOpen)
        {
            case 3:
                if (noodleScore > 1 && Input.GetKeyDown(KeyCode.E)) //break this out into settable int in the inspector.
                {
                    FlumeClose(3);
                }
                break;
            case 2:
                if (noodleScore > 2 && Input.GetKeyDown(KeyCode.E))
                {
                    FlumeClose(2);
                }
                break;
            case 1:
                //Victory?
                break;
            default:
                Debug.Log("Busted case checking on flumes closing");
                break;
        }
    }

    private void FlumeClose(int flumecase)
    {
        switch (flumecase)
        {
            case 3:
                worldStatusCheck.GetComponent<WorldStatusScript>().FlumeStatus = 2;
                
                if (!(transform.position == pointOfInteractionMid.position || transform.position == pointOfInteractionClose.position))
                {
                    if (isMoving != null)
                    {
                        StopCoroutine(isMoving); // Covers the edge case where the player closes flume they are currently over.
                    }
                    isMoving = StartCoroutine(Move(pointOfInteractionMid.position));
                }

                //Run an animation / put a thing over the top flume. Disable the particles.
                break;
            
            case 2:
                worldStatusCheck.GetComponent<WorldStatusScript>().FlumeStatus = 1;
                
                if (transform.position != pointOfInteractionClose.position)
                {
                    if (isMoving != null)
                    {
                        StopCoroutine(isMoving); // Covers the edge case where the player closes flume they are currently over.
                    }
                    isMoving = StartCoroutine(Move(pointOfInteractionClose.position));
                }
                //animations, ect.
                break;
            
            case 1:
                //Run VICTORY!
                break;
            default:
                Debug.Log("Error in flume status or FlumeClose method.");
            break;
        }
    }
    
    
    // Nasty Pair of methods that check to see whether the player can move up or down
    private bool OpenUp()
    {
        var flumeCheck = worldStatusCheck.GetComponent<WorldStatusScript>().FlumeStatus;

        if ((transform.position == pointOfInteractionMid.position || transform.position == pointOfInteractionClose.position) && flumeCheck == 3)
        {
            Debug.Log("OpenupSuccess");
            return true;
        }
        return transform.position == pointOfInteractionClose.position && flumeCheck == 2;
    }
    bool OpenDown()
    {
        var flumeCheck = worldStatusCheck.GetComponent<WorldStatusScript>().FlumeStatus;

        if (transform == (pointOfInteractionMid || pointOfInteractionFar) && flumeCheck == 3)
        {
            return true;
        }
        return transform.position == pointOfInteractionMid.position && flumeCheck == 2;
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
