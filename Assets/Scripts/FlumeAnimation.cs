using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using UnityEngine;

public class FlumeAnimation : MonoBehaviour
{
    
    [SerializeField] private Animator farFlumeShutAnimator;
    [SerializeField] private Animator midFlumeShutAnimator;
    [SerializeField] private Animator closeFlumeShutAnimator;

    [SerializeField] private ParticleSystem flumeFlowFar;
    [SerializeField] private ParticleSystem flumeFlowMid;
    [SerializeField] private ParticleSystem flumeFlowClose;
    [SerializeField] private ParticleSystem flumeFallFar;
    [SerializeField] private ParticleSystem flumeFallMid;
    [SerializeField] private ParticleSystem flumeFallClose;

    
    public void RunAnimation(int flumestatus)
    {
        switch (flumestatus)
        {
            case 2:
                flumeFlowFar.Clear();
                flumeFlowFar.Stop();
                flumeFallFar.Stop();
                farFlumeShutAnimator.GetComponent<SpriteRenderer>().enabled = true;
                farFlumeShutAnimator.SetTrigger("FirstShut");
                break;
            
            case 1:
                flumeFlowMid.Clear();
                flumeFlowMid.Stop();
                flumeFallMid.Stop();
                midFlumeShutAnimator.GetComponent<SpriteRenderer>().enabled = true;
                midFlumeShutAnimator.SetTrigger("SecondShut");
                break;
            
            case 0:
                flumeFlowClose.Clear();
                flumeFlowClose.Stop();
                flumeFallClose.Stop();
                closeFlumeShutAnimator.GetComponent<SpriteRenderer>().enabled = true;
                closeFlumeShutAnimator.SetTrigger("LastShut");
                break;
            
            default:
                Debug.Log("Passed a bad Flume Close RunAnimation value"); 
                break;
        }
    }
}
