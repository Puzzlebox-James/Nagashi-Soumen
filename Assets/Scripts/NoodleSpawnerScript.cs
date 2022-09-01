using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class NoodleSpawnerScript : MonoBehaviour
{
    [SerializeField] private float minTimeBetweenNoodles;
    [SerializeField] private float maxTimeBetweenNoodles;
    [SerializeField] private Transform[] spawnLocations;
    [SerializeField] private GameObject[] noodlePrefab;


    private void Start()
    {
        StartCoroutine(TimedSpawns());
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Instantiate(noodlePrefab[Random.Range(0,noodlePrefab.Length)], spawnLocations[Random.Range(0, spawnLocations.Length)].position, quaternion.identity);
        }
    }

    IEnumerator TimedSpawns()
    {
        while (true)
        {
            Instantiate(noodlePrefab[Random.Range(0,noodlePrefab.Length)], spawnLocations[Random.Range(0, spawnLocations.Length)].position, quaternion.identity);
            yield return new WaitForSeconds(Random.Range(minTimeBetweenNoodles, maxTimeBetweenNoodles));
        }
    }
}
