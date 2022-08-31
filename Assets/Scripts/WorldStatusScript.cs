using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldStatusScript: MonoBehaviour
{
    [SerializeField] private int flumeStatus;
    [SerializeField] private Text noodleScoreText;

    [SerializeField] int noodleScore = 0;
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
