using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class NoodleScript : MonoBehaviour
{

   private Vector3 pointOne;
   private Vector3 pointTwo;
   private Vector3 bezierPoint;
   
   
   [Header("Runtime Check")] // I'm just setting these manually for now
   [SerializeField] private Transform noodleSpawnLocation;
   [SerializeField] private float noodleSpeed;
   [SerializeField] private bool isMissed = false;
   
   public Transform NoodleSpawnLocation
   {
      get
      {
         return this.noodleSpawnLocation;
      }
      set
      {
         noodleSpawnLocation = value;
      }
   }
   public float NoodleSpeed
   {
      get
      {
         return this.noodleSpeed;
      }
      set
      {
         // Do checks here
         noodleSpeed = value;
      }
   }

   
   // Upon the spawn of a (this) noodle do a check to see where we spawned, and set curve ref points accordingly for if we fall.
   private void Start()
   {
      if (noodleSpawnLocation.position == Points.PointsInstance.farSpawn.position)
      {
         pointOne = Points.PointsInstance.farFall.position;
         pointTwo = Points.PointsInstance.farFallWater.position;
         
         bezierPoint = pointOne + (pointTwo - pointOne) / 2 + Vector3.up * 15f;
      }
      else if (noodleSpawnLocation.position == Points.PointsInstance.midSpawn.position)
      {
         pointOne = Points.PointsInstance.midFall.position;
         pointTwo = Points.PointsInstance.midFallWater.position;
         
         bezierPoint = pointOne + (pointTwo - pointOne) / 2 + Vector3.up * 5f;
      }
      else if (noodleSpawnLocation.position == Points.PointsInstance.closeSpawn.position)
      {
         pointOne = Points.PointsInstance.closeFall.position;
         pointTwo = Points.PointsInstance.closeFallWater.position;
         
         bezierPoint = pointOne + (pointTwo - pointOne) / 2 + Vector3.up * 1f;
      }
      
   }


   // Move the noodle left at the noodleSpeed, and fall if we hit the miss trigger.
   private void Update()
   {
      switch (isMissed)
      {
         case false:
            transform.position += new Vector3(noodleSpeed, 0, 0) * Time.deltaTime;
            break;
         case true:
            NoodleFall();
            break;
      }
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.gameObject.GetComponent<MissCheckerScript>() == null) return;
      isMissed = true;
   }

   
   private float count = 0f;
   private void NoodleFall()
   {
      if(count < 1.0f)
      {
         count += 1f * Time.deltaTime;

         Vector3 m1 = Vector3.Lerp(pointOne, bezierPoint, count);
         Vector3 m2 = Vector3.Lerp(bezierPoint, pointTwo, count);
         var movementDirection = Vector3.Lerp(m1, m2, count);
         transform.position = movementDirection;
         
         var toRotation = Quaternion.LookRotation(Vector3.forward, movementDirection);
         transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, .1f);
      }
   }
}
