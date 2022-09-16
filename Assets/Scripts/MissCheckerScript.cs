using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissCheckerScript : MonoBehaviour
{
    [SerializeField] private Text TEST;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.GetComponent<NoodleScript>()) return;
        Destroy(other.gameObject, 3f);
        GameSettingsAndStatusData.MissesAllowed -= 1;

        if (GameSettingsAndStatusData.MissesAllowed < 0)
        {
            //TEST = ToString(GameSettingsAndStatusData.MissesAllowed);
        }
    }
}
