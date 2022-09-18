using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIVisuals : MonoBehaviour
{

    [SerializeField] private SpriteRenderer ShiftKey;
    private Coroutine ShiftKeyIsFadding;
    
    public static UIVisuals UIVisualsInstance;
    private void Awake()
    {
        if (UIVisualsInstance != null)
            Destroy(UIVisualsInstance);
        UIVisualsInstance = this;
    }

    private void Start()
    {
        StartCoroutine(CheckStatus());
    }

    private IEnumerator CheckStatus()
    {
        while (true)
        {
            // Check The NoodleScore against the FlumeStatus and fade in the Shift Button UI Control when appropriate.
            if (GameSettingsAndStatusData.NoodleScore > GameSettingsAndStatusData.FirstNoodleCloseFlumeScore &&
                GameSettingsAndStatusData.FlumeStatus == 3 && ShiftKey.color.a == 0)
            {
                ShiftKeyIsFadding = StartCoroutine(FadeTo(ShiftKey, 1, 5));
            }
            if(GameSettingsAndStatusData.NoodleScore > GameSettingsAndStatusData.SecondNoodleCloseFlumeScore && GameSettingsAndStatusData.FlumeStatus == 2)
            {
                ShiftKeyIsFadding = StartCoroutine(FadeTo(ShiftKey, 1, 5));
            }
            
            yield return new WaitForSeconds(2);
        }
    }

    public void FadeShiftOut()
    {
        StopCoroutine(ShiftKeyIsFadding);
        ShiftKeyIsFadding = StartCoroutine(FadeTo(ShiftKey, 0, 2));
    }
    
    private IEnumerator FadeTo(SpriteRenderer sprite, float alphaValue, float time)
    {
        var alpha = sprite.color.a;
        for (var t = 0.0f; t < 1.0f; t += Time.deltaTime / time)
        {
            var newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, alphaValue, t));
            sprite.color = newColor;
            yield return null;
        }
    }




    public void Restart()
    {
        GameSettingsAndStatusData.NoodleScore = 0;
        SceneManager.LoadScene("TitleScene");
    }
    
}
