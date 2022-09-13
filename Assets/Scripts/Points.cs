using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    public Transform farSpawn;
    public Transform midSpawn;
    public Transform closeSpawn;

    public Transform farInteract;
    public Transform midInteract;
    public Transform closeInteract;

    public Transform farFall;
    public Transform midFall;
    public Transform closeFall;
    public Transform farFallWater;
    public Transform midFallWater;
    public Transform closeFallWater;


    public static Points PointsInstance;
    private void Awake()
    {
        PointsInstance = this;
    }
}
