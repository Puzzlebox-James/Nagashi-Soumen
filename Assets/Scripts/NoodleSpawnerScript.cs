using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class NoodleSpawnerScript : MonoBehaviour
{
    // Gameplay 'settings' regarding noodles.
    [SerializeField] private float minTimeBetweenNoodles;
    [SerializeField] private float maxTimeBetweenNoodles;
    
    [SerializeField] private List<Transform> spawnLocations;
    [SerializeField] private List<GameObject> noodlePrefab;




    public void NoodleSpawnReduction(int flumestatus)
    {
        switch (flumestatus)
        {
            case 2:
                spawnLocations.RemoveAt(2);
                break;
            case 1:
                spawnLocations.RemoveAt(1);
                break;
            default:
                break;
        }
    }


    private void Start()
    {
        StartCoroutine(TimedSpawns());
    }

    // Bust this out into it's own method (possibly script class) to handle the multiplayer option.
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            var randomNoodleSpawn = spawnLocations[Random.Range(0, spawnLocations.Count)];
            var newNoodle = Instantiate(noodlePrefab[Random.Range(0,noodlePrefab.Count)], randomNoodleSpawn.position, quaternion.identity);
            newNoodle.GetComponent<NoodleScript>().NoodleSpawnLocation = randomNoodleSpawn;
        }
    }

    // Spawn a random noodle at a random time (within range), on a random flume (within range)
    IEnumerator TimedSpawns()
    {
        while (true)
        {
            var randomNoodleSpawn = spawnLocations[Random.Range(0, spawnLocations.Count)];
            
            var newNoodle = Instantiate(noodlePrefab[Random.Range(0,noodlePrefab.Count)], randomNoodleSpawn.position, quaternion.identity);
            newNoodle.GetComponent<NoodleScript>().NoodleSpawnLocation = randomNoodleSpawn;
            yield return new WaitForSeconds(Random.Range(minTimeBetweenNoodles, maxTimeBetweenNoodles));
            
        }
    }
}
