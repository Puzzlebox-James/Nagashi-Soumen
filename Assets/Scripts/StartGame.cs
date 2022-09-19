using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{

    [SerializeField] private ToggleTalker SoloOrVSToggle;
    [SerializeField] private ToggleTalker FlowToggle;
    [SerializeField] private ToggleTalker NumberOfNoodles;
    [SerializeField] private ToggleTalker MissesAllowed;
    
    public void StartButtonPressed()
    {
        SoloOrVSToggle.SendData("SoloOrVS");
        FlowToggle.SendData("FlowToggle");
        NumberOfNoodles.SendData("NumberOfNoodles");
        MissesAllowed.SendData("MissesAllowed");

        if (GameSettingsAndStatusData.NumberOfNoodles < 100)
        {
            GameSettingsAndStatusData.FirstNoodleCloseFlumeScore =  GameSettingsAndStatusData.NumberOfNoodles * (1 / 3.0);
            GameSettingsAndStatusData.SecondNoodleCloseFlumeScore = GameSettingsAndStatusData.NumberOfNoodles * (2 / 3.0);
            
            Debug.Log(GameSettingsAndStatusData.FirstNoodleCloseFlumeScore + "FIRST FLUME CLOSE SCORE");
        } else {
            
            // THIS IS FOR THE GLUTTON SETTING
            GameSettingsAndStatusData.FirstNoodleCloseFlumeScore = 3;
            GameSettingsAndStatusData.SecondNoodleCloseFlumeScore = 6;
            GameSettingsAndStatusData.WinNoodleCloseFlumeScore = 10;
        }

        SceneManager.LoadScene("MainScene");
    }
}


// Checker somewhere that looks at noodle score vs number of noodles, along with flume status to display victory screen.
// Probably on a victory or defeat script. Can be a singleton that lives in Main Scene.