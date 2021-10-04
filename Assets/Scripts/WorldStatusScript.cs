using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldStatusScript: MonoBehaviour
{
    [SerializeField] private int flumeStatus;
    [SerializeField] private Text noodleScoreText;

    private int noodleScore = 0;
    public int FlumeStatus
    {
        get
        {
            return this.flumeStatus;
        }
        set
        {
            //Do checks here to make sure things don't break
            flumeStatus = value;
        }
    }

    public int NoodleScore
    {
        get
        {
            return this.noodleScore;
        }
        set
        {
            //Do Checks here
            noodleScore = value;
            noodleScoreText.text = noodleScore.ToString();
        }
    }
    
}
