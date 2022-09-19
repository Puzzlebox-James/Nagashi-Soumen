using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Runtime.CompilerServices;

public class ToggleTalker : MonoBehaviour
{
    private ToggleGroup toggleGroupInstance;
    private Toggle currentSelection
    {
        get => toggleGroupInstance.ActiveToggles().FirstOrDefault();
    }

    void Start()
    {
        toggleGroupInstance = GetComponent<ToggleGroup>();
    }

    public void SendData(string setting)
    {
        switch (setting)
        {
            case "SoloOrVS":
                if (currentSelection.name == "VS")
                {
                    GameSettingsAndStatusData.SoloSelected = false;
                }
                else
                {
                    GameSettingsAndStatusData.SoloSelected = true;
                }
                break;

            case "FlowToggle":
                GameSettingsAndStatusData.FlowSelection = currentSelection.name switch
                {
                    "Gentle" => 0,
                    "Predictable" => 1,
                    "Zippy" => 2,
                    "Wild" => 3,
                    _ => GameSettingsAndStatusData.FlowSelection
                };
                // +++ HERE BE MAGIC VARIABLES FOR THE STARTING VALUES OF EACH NOODLE SPEED BASED ON FLOW SELECTION +++ //
                GameSettingsAndStatusData.NoodleSpeedBottom = GameSettingsAndStatusData.FlowSelection switch
                {
                    0 => 2,
                    1 => 4,
                    2 => 6,
                    3 => 1,
                    _ => GameSettingsAndStatusData.NoodleSpeedBottom
                };
                GameSettingsAndStatusData.NoodleSpeedTop = GameSettingsAndStatusData.FlowSelection switch
                {
                    0 => 3,
                    1 => 6,
                    2 => 8,
                    3 => 8,
                    _ => GameSettingsAndStatusData.NoodleSpeedTop
                };
                break;
            
            case "NumberOfNoodles":
                GameSettingsAndStatusData.NumberOfNoodles = currentSelection.name switch
                {
                    "Peckish" => 20,
                    "JustHungry" => 30,
                    "Starving" => 50,
                    "Glutton" => 1000,
                    _ => GameSettingsAndStatusData.NumberOfNoodles
                };
                break;
            
            case "MissesAllowed":
                GameSettingsAndStatusData.MissesAllowed = currentSelection.name switch
                {
                    "Lavish" => 20,
                    "Excess" => 8,
                    "Frugal" => 3,
                    "None" => 0,
                    _ => GameSettingsAndStatusData.MissesAllowed
                };
                break;
            
        }
    }
}
