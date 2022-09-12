using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Curves : MonoBehaviour
{
    public static Curves CurvesInstance;

    private void Awake()
    {
        CurvesInstance = this;
    }


    public IEnumerator MakeAndRunCurve(Vector3 objectPosition, Transform pointOne, Transform pointTwo, float curvyness)
    {
        var bezierPoint = new Vector3();
        bezierPoint = pointOne.position + (pointTwo.position - pointOne.position) / 2 + Vector3.up * curvyness;

        var count = 0f;

        while (count < 1.0f)
        {
            count += 1.0f * Time.deltaTime;

            Vector3 m1 = Vector3.Lerp(pointOne.position, bezierPoint, count);
            Vector3 m2 = Vector3.Lerp(bezierPoint, pointTwo.position, count);
            objectPosition = Vector3.Lerp(m1, m2, count);
            yield return objectPosition;
        }
    }
}
