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

        SceneManager.LoadScene("MainScene");
    }
}
