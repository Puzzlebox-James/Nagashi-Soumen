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

    [SerializeField] private bool canSpawnNoodle = true;

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
    
    // Magic Variable Setting for the increase in noodle speed per call, based on flow difficulty selection.
    public void FlowRateIncrease()
    {
        switch (GameSettingsAndStatusData.FlowSelection)
        {
            case 0: // Gentle
                GameSettingsAndStatusData.NoodleSpeedBottom += 1;
                GameSettingsAndStatusData.NoodleSpeedTop += 2;
                break;
            case 1: // Predictable
                GameSettingsAndStatusData.NoodleSpeedBottom += 2;
                GameSettingsAndStatusData.NoodleSpeedTop += 2;
                break;
            case 2: // Zippy
                GameSettingsAndStatusData.NoodleSpeedBottom += 2;
                GameSettingsAndStatusData.NoodleSpeedTop += 2;
                break;
            case 3: // Wild!
                GameSettingsAndStatusData.NoodleSpeedTop += 2;
                break;
        }
    }
    
    private void Start()
    {
        if (GameSettingsAndStatusData.SoloSelected)
        {
            StartCoroutine(TimedSpawns());
        }

        StartCoroutine(NoodleSpeedupCheck());
    }


    // Used strictly for 'multiplayer', were a player can choose the noodle spawn locations and timing.
    void Update()
    {
        if (GameSettingsAndStatusData.SoloSelected == true) return;
        
        if(Input.GetButtonDown("Fire1") && canSpawnNoodle)
        {
            var noodleSpawn = spawnLocations[0]; // Close Spawn
            var newNoodle = Instantiate(noodlePrefab[Random.Range(0,noodlePrefab.Count)], noodleSpawn.position, quaternion.identity);
            newNoodle.GetComponent<NoodleScript>().NoodleSpawnLocation = noodleSpawn;
            canSpawnNoodle = false;
            StartCoroutine(WaitJustASec());
        }
        if(Input.GetButtonDown("Fire2") && canSpawnNoodle)
        {
            if (spawnLocations.Count != 3) return;
            var noodleSpawn = spawnLocations[2]; // Far Spawn
            var newNoodle = Instantiate(noodlePrefab[Random.Range(0,noodlePrefab.Count)], noodleSpawn.position, quaternion.identity);
            newNoodle.GetComponent<NoodleScript>().NoodleSpawnLocation = noodleSpawn;
            canSpawnNoodle = false;
            StartCoroutine(WaitJustASec());
        }
        if(Input.GetButtonDown("Fire3") && canSpawnNoodle)
        {
            if (spawnLocations.Count != 2) return;
            var noodleSpawn = spawnLocations[1]; // Mid Spawn
            var newNoodle = Instantiate(noodlePrefab[Random.Range(0,noodlePrefab.Count)], noodleSpawn.position, quaternion.identity);
            newNoodle.GetComponent<NoodleScript>().NoodleSpawnLocation = noodleSpawn;
            canSpawnNoodle = false;
            StartCoroutine(WaitJustASec());
        }
    }

    // Make it so the noodle spawning player can't just spam 'em.
    private IEnumerator WaitJustASec()
    {
        yield return new WaitForSeconds(.2f);
        canSpawnNoodle = true;
    }
    
    
    
    // Spawn a random noodle at a random time (within range), on a random flume (within range). Solo Mode.
    IEnumerator TimedSpawns()
    {
        for (var i = 0; i < GameSettingsAndStatusData.NumberOfNoodles; i += 1)
        {
            var randomNoodleSpawn = spawnLocations[Random.Range(0, spawnLocations.Count)];
            
            var newNoodle = Instantiate(noodlePrefab[Random.Range(0,noodlePrefab.Count)], randomNoodleSpawn.position, quaternion.identity);
            newNoodle.GetComponent<NoodleScript>().NoodleSpawnLocation = randomNoodleSpawn;
            yield return new WaitForSeconds(Random.Range(minTimeBetweenNoodles, maxTimeBetweenNoodles));
        }
        
        Victory.VictoryInstance.Show();
    }

    
    // Checks every 2 seconds if we passed a threshold to increase noodle speed.
    IEnumerator NoodleSpeedupCheck()
    {
        var secondActivated = false;
        var firstActivated = false;
        var firstThird =  GameSettingsAndStatusData.NumberOfNoodles * (1 / 3.0);
        var secondThird = GameSettingsAndStatusData.NumberOfNoodles * (2 / 3.0);
        
        while (!secondActivated)
        {
            if (GameSettingsAndStatusData.NoodleScore > firstThird && firstActivated == false)
            {
                FlowRateIncrease();
                firstActivated = true;
                Debug.Log("First Activation");
            }
            if (GameSettingsAndStatusData.NoodleScore > secondThird)
            {
                FlowRateIncrease();
                secondActivated = true;
                Debug.Log("Second Activation");
            }
            
            yield return new WaitForSeconds(2);
        }
    }
}
