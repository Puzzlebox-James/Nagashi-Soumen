using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Object = System.Object;

public class MissCheckerScript : MonoBehaviour
{
    [SerializeField] private NoodleSpawnerScript noodleSpawnerScript;
    [SerializeField] private FlumeAnimation flumeAnimation;
    [SerializeField] private GameObject WastedText;
    [SerializeField] private GameObject WastedNoodle;
    [SerializeField] private Button PlayAgainButton;
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.GetComponent<NoodleScript>()) return;
        Destroy(other.gameObject, 1f);
        GameSettingsAndStatusData.MissesAllowed -= 1;

        if (other.GetComponent<NoodleScript>().NoodleSpawnLocation.position == Points.PointsInstance.farSpawn.position)
        {
            Instantiate(WastedNoodle, Points.PointsInstance.farFallWater.position, quaternion.identity);
        }
        if (other.GetComponent<NoodleScript>().NoodleSpawnLocation.position == Points.PointsInstance.midSpawn.position)
        {
            Instantiate(WastedNoodle, Points.PointsInstance.midFallWater.position, quaternion.identity);
        }
        if (other.GetComponent<NoodleScript>().NoodleSpawnLocation.position == Points.PointsInstance.closeSpawn.position)
        {
            Instantiate(WastedNoodle, Points.PointsInstance.closeFallWater.position, quaternion.identity);
        }
        
        
        
        if (GameSettingsAndStatusData.MissesAllowed < 0)
        {
            flumeAnimation.RunAnimation(2);
            flumeAnimation.RunAnimation(1);
            flumeAnimation.RunAnimation(0);

            var noodles = FindObjectsOfType<NoodleScript>();
            foreach (var noodle in noodles)
            {
                Destroy(noodle.gameObject);
            }
            Destroy(noodleSpawnerScript.gameObject);

            PlayAgainButton.gameObject.SetActive(true);
            WastedText.SetActive(true);
        }
    }
}
