using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class Victory : MonoBehaviour
{
    
    [SerializeField] private Button PlayAgainButton;
    [SerializeField] private Text ShowScore;
    
    public static Victory VictoryInstance;
    private void Awake()
    {
        if (VictoryInstance != null)
            Destroy(VictoryInstance);
        VictoryInstance = this;
    }

    
    

    public void Show()
    {
        PlayAgainButton.gameObject.SetActive(true);
        ShowScore.gameObject.SetActive(true);
        ShowScore.text = GameSettingsAndStatusData.NoodleScore + " / " + GameSettingsAndStatusData.NumberOfNoodles + "Blah blah";
        if (GameSettingsAndStatusData.NoodleScore == GameSettingsAndStatusData.NumberOfNoodles)
        {
            // Show the PERFECT!
            // Actually this is always fail because the noodle isn't grabbed when we fire this...
        }
    }
    
    
}
