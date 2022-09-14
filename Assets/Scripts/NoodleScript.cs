using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class NoodleScript : MonoBehaviour
{
   // =+=+=+=+ Variables to be set! =+=+=+=+ //
   private float farFallCurveHeight   = 4f;
   private float midFallCurveHeight   = 3f;
   private float closeFallCurveHeight = 2f;


   // These are used to calculate the path the noodle falls along if the player missed grabbing it
   private Vector3 pointOne;
   private Vector3 pointTwo;
   private Vector3 bezierPoint;
   
   [Header("Runtime Peak")]
   [SerializeField] private Transform noodleSpawnLocation;
   [SerializeField] private Vector3 noodleSpeed;
   [SerializeField] private bool isMissed = false;

   public Transform NoodleSpawnLocation
   {
      get => this.noodleSpawnLocation;
      set => noodleSpawnLocation = value;
   }

   
   // Upon the spawn of a (this) noodle do a check to see where we spawned, and set curve ref points accordingly for if we fall.
   // Also generate and store the random (in range) speed of the noodle.
   private void Start()
   {
      if (noodleSpawnLocation.position == Points.PointsInstance.farSpawn.position)
      {
         pointOne = Points.PointsInstance.farFall.position;
         pointTwo = Points.PointsInstance.farFallWater.position;
         
         bezierPoint = pointOne + (pointTwo - pointOne) / 2 + Vector3.up * farFallCurveHeight;
      }
      else if (noodleSpawnLocation.position == Points.PointsInstance.midSpawn.position)
      {
         pointOne = Points.PointsInstance.midFall.position;
         pointTwo = Points.PointsInstance.midFallWater.position;
         
         bezierPoint = pointOne + (pointTwo - pointOne) / 2 + Vector3.up * midFallCurveHeight;
      }
      else if (noodleSpawnLocation.position == Points.PointsInstance.closeSpawn.position)
      {
         pointOne = Points.PointsInstance.closeFall.position;
         pointTwo = Points.PointsInstance.closeFallWater.position;
         
         bezierPoint = pointOne + (pointTwo - pointOne) / 2 + Vector3.up * closeFallCurveHeight;
      }
      
      noodleSpeed = new Vector3(Random.Range(GameSettingsAndStatusData.NoodleSpeedBottom, GameSettingsAndStatusData.NoodleSpeedTop), 0, 0);
   }


   // Move the noodle left at the noodleSpeed, and fall if we hit the miss trigger.
   // THIS IS THE UPDATE, ONCE A FRAME HERE!
   private void Update()
   {
      switch (isMissed)
      {
         case false:
            transform.position += noodleSpeed * Time.deltaTime;
            break;
         case true:
            NoodleFall();
            break;
      }
   }

   private float count = 0f;
   private void NoodleFall()
   {
      if(count < 1.0f)
      {
         count += noodleSpeed.x/2f * Time.deltaTime;

         Vector3 m1 = Vector3.Lerp(pointOne, bezierPoint, count);
         Vector3 m2 = Vector3.Lerp(bezierPoint, pointTwo, count);
         var movementDirection = Vector3.Lerp(m1, m2, count);
         transform.position = movementDirection;
         
         var toRotation = Quaternion.LookRotation(Vector3.forward, movementDirection);
         transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, noodleSpeed.x / 15f);
      }
   }
   
   
   // Check to see if we missed!
   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.gameObject.GetComponent<MissCheckerScript>() == null) return;
      isMissed = true;
   }
}
