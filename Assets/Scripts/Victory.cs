using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

public class Victory : MonoBehaviour
{
    
    [SerializeField] private Button PlayAgainButton;
    [SerializeField] private Text ShowScore;
    [SerializeField] private SpriteRenderer Perfect;
    
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
        ShowScore.text = GameSettingsAndStatusData.NoodleScore + " / " + GameSettingsAndStatusData.NumberOfNoodles + " Noodles Grabbed!";
    }


    public IEnumerator CheckAndDisplay()
    {
        while (FindObjectOfType<NoodleScript>() != null)
        {
            yield return new WaitForSeconds(1);
        }
        Show();
        if (GameSettingsAndStatusData.NoodleScore == GameSettingsAndStatusData.NumberOfNoodles)
        {
            Perfect.gameObject.SetActive(true);
        }
    }
    
}
