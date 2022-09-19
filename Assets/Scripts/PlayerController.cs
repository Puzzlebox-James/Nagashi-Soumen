using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{

    // Get references and set gameplay settings
    [Header("These all require setting")]
    [SerializeField] private Transform pointOfInteractionFar;
    [SerializeField] private Transform pointOfInteractionMid;
    [SerializeField] private Transform pointOfInteractionClose;
    [SerializeField] private GameObject flumeAnimationGameObject;
    [SerializeField] private GameObject NoodleSpawner;
    
    [SerializeField] private float moveDuration;

    [SerializeField] private Animator playerAnimator;
    [SerializeField] private AnimationClip noodleGrab;
    [SerializeField] private AnimationClip noodleMiss;

    // private temp storage vars
    private Collider2D grabbableNoodleCollider;
    private List<Collider2D> grabbableNoodleColliderOverflow = new List<Collider2D>();
    private Coroutine isMoving;
    private Coroutine isGrabbing;
    

    private void Update()
    {
       CheckAndDeployVert();
       CheckAndDeployGrab();
       CheckAndDeployFlumeClose();
    }
    
    // ================================================= UP AND DOWN MOVEMENT =============================================== //
    // Runs every frame per Updates call
    // If there is a player input button down event (when we are at a point of interaction), find out which way we're going.
    // Run a check method to see if we can go that way, where we are and where we need to go, then start the move and go there.
    private void CheckAndDeployVert()
    {
        if (Input.GetButtonDown("Vertical") != true) return;
        if (isGrabbing != null) return;
        if (Input.GetAxis("Vertical") > 0)
        {
            if (!OpenUp()) return;
            if (transform.position == pointOfInteractionMid.position)
            {
                isMoving = StartCoroutine(Move(pointOfInteractionFar.position));
            }
            if(transform.position == pointOfInteractionClose.position)
            {
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

    // Nasty Pair of methods that check to see whether the player can move up or down
    private bool OpenUp()
    {
        var flumeCheck = GameSettingsAndStatusData.FlumeStatus;

        if ((transform.position == pointOfInteractionMid.position || transform.position == pointOfInteractionClose.position) && flumeCheck == 3)
        {
            return true;
        }
        return transform.position == pointOfInteractionClose.position && flumeCheck == 2;
    }
    bool OpenDown()
    {
        var flumeCheck = GameSettingsAndStatusData.FlumeStatus;

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
    
    
    // =================================================== NOODLE GRABBING ================================================ //
    // Runs every frame per Updates call. Check to see if we pressed 'grab', then check to see if there was a noodle(s).
    // If there was, destroy it, notify score and animation. If there wasn't DO MISS
    private void CheckAndDeployGrab()
    {
        if (!Input.GetButtonDown("Jump")) return;
        if (grabbableNoodleCollider != null && isGrabbing == null)
        {
            Destroy(grabbableNoodleCollider.gameObject);
            GameSettingsAndStatusData.NoodleScore++;
            
            while (grabbableNoodleColliderOverflow.Count > 0)
            {
                var extraNood = grabbableNoodleColliderOverflow[0].gameObject;
                grabbableNoodleColliderOverflow.RemoveAt(0);
                Destroy(extraNood);
                GameSettingsAndStatusData.NoodleScore++;
            }

            playerAnimator.SetTrigger("Grab");
            //isGrabbing = StartCoroutine(WaitForAnimation(noodleGrab.length));
            
        } else if(isGrabbing == null) {
            playerAnimator.SetTrigger("GrabMiss");
            isGrabbing = StartCoroutine(WaitForAnimation(noodleMiss.length));
        }
    }

    private IEnumerator WaitForAnimation(float length)
    {
        yield return new WaitForSeconds(length);
        isGrabbing = null;
    }
    
    
    // Store noodle grabbable status.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (grabbableNoodleCollider !=null)
            grabbableNoodleColliderOverflow.Add(other);
        else
        {
            grabbableNoodleCollider = other;
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        grabbableNoodleCollider = null;
    }


    // =============================================== FLUME CLOSING AND GAME ADVANCEMENT ============================================= //
    // Runs every frame per Updates call. Get information about the world status to check and close sequential flumes if acceptable or WIN
    private void CheckAndDeployFlumeClose()
    {
        if (!Input.GetButtonDown("Submit")) return;
        var flumesOpen = GameSettingsAndStatusData.FlumeStatus;
        var noodleScore = GameSettingsAndStatusData.NoodleScore;
        var firstNoodleCloseScore = GameSettingsAndStatusData.FirstNoodleCloseFlumeScore;
        var secondNoodleCloseScore = GameSettingsAndStatusData.SecondNoodleCloseFlumeScore;
        var winNoodleCloseScore = GameSettingsAndStatusData.WinNoodleCloseFlumeScore;

        switch (flumesOpen)
        {
            case 3:
                if (noodleScore > firstNoodleCloseScore)
                {
                    FlumeClose(3);
                    NoodleSpawner.GetComponent<NoodleSpawnerScript>().NoodleSpawnReduction(2);
                    NoodleSpawner.GetComponent<NoodleSpawnerScript>().FlowRateIncrease();
                }
                break;
            case 2:
                if (noodleScore > secondNoodleCloseScore)
                {
                    FlumeClose(2);
                    NoodleSpawner.GetComponent<NoodleSpawnerScript>().NoodleSpawnReduction(1);
                    NoodleSpawner.GetComponent<NoodleSpawnerScript>().FlowRateIncrease();
                }
                break;
            case 1:
                if (noodleScore >= winNoodleCloseScore)
                {
                    // TIME TO WIN
                    FlumeClose(1);
                }

                break;
            default:
                Debug.Log("Busted case checking on flumes closing");
                break;
        }
    }

    // Actually does the closing of flumes by updating the world status, while checking and handling edge cases.
    private void FlumeClose(int flumecase)
    {
        switch (flumecase)
        {
            case 3:
                GameSettingsAndStatusData.FlumeStatus = 2;
                flumeAnimationGameObject.GetComponent<FlumeAnimation>().RunAnimation(2);
                UIVisuals.UIVisualsInstance.FadeShiftOut();
                
                if (!(transform.position == pointOfInteractionMid.position || transform.position == pointOfInteractionClose.position))
                {
                    if (isMoving != null)
                    {
                        StopCoroutine(isMoving); // Covers the edge case where the player closes flume they are currently over.
                    }
                    isMoving = StartCoroutine(Move(pointOfInteractionMid.position));
                }
                break;
            
            case 2:
                GameSettingsAndStatusData.FlumeStatus = 1;
                flumeAnimationGameObject.GetComponent<FlumeAnimation>().RunAnimation(1);
                UIVisuals.UIVisualsInstance.FadeShiftOut();
                
                if (transform.position != pointOfInteractionClose.position)
                {
                    if (isMoving != null)
                    {
                        StopCoroutine(isMoving); // Covers the edge case where the player closes flume they are currently over.
                    }
                    isMoving = StartCoroutine(Move(pointOfInteractionClose.position));
                }
                break;
            
            case 1:
                // VICTORY! STOP THE SPAWNING!
                flumeAnimationGameObject.GetComponent<FlumeAnimation>().RunAnimation(0);
                Destroy(NoodleSpawner);
                Victory.VictoryInstance.Show();
                break;
            default:
                Debug.Log("Error in flume status or FlumeClose method.");
            break;
        }
    }
}
