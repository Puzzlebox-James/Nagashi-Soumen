/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldStatusScript: MonoBehaviour
{
    // Changeable score requirements to progress game and eventually win.
    [SerializeField] private int firstNoodleCloseFlumeScore = 1;
    [SerializeField] private int secondNoodleCloseFlumeScore = 2;
    [SerializeField] private int winNoodleCloseFlumeScore = 3;
    
    
    // Serialized so I can look and debug.
    // Hopefully it's 'good practice'? encapsulated properties?
    [SerializeField] private int flumeStatus;
    [SerializeField] private Text noodleScoreText;
    [SerializeField] private int noodleScore = 0;
    
    
    // Encapsulated properties for other scripts to use the close scores.
    public int FirstNoodleCloseFlumeScore => this.firstNoodleCloseFlumeScore;
    public int SecondNoodleCloseFlumeScore => this.secondNoodleCloseFlumeScore;
    public int WinNoodleCloseFlumeScore => this.winNoodleCloseFlumeScore;

    
    public int FlumeStatus
    {
        get => this.flumeStatus;
        set =>
            //Do checks here to make sure things don't break
            flumeStatus = value;

}

    public int NoodleScore
    {
        get => this.noodleScore;
        set
        {
            //Do Checks here
            noodleScore = value;
            noodleScoreText.text = noodleScore.ToString();
        }
    }
    
}
*/
