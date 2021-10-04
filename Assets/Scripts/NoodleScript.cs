using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class NoodleScript : MonoBehaviour
{
   [SerializeField] private float noodleSpeed;
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


   private void Update()
   {
      transform.position +=  new Vector3(noodleSpeed,0,0) * Time.deltaTime;
   }
}
