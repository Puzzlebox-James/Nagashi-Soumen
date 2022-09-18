using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public static class GameSettingsAndStatusData
{
    // ================= Start Settings ================== //

    private static bool soloSelected;
    public static bool SoloSelected
    {
        get => soloSelected;
        set => soloSelected = value;
    }

    // Speed Difficulty, drives noodle speed top and bottom (toggle talker)
    private static int flowSelection;
    public static int FlowSelection
    {
        get => flowSelection;
        set
        {
            if (value < 0 || value > 4)
            {
                value = 2;
            }
            flowSelection = value;
        }
    }
    
    // Amount of noodles missed allowed.
    private static int missesAllowed = 3;
    public static int MissesAllowed
    {
        get => missesAllowed;
        set => missesAllowed = value;
    }

    // =============== Noodles ================ //
    
    // Number of Noodles
    private static int numberOfNoodles = 30;
    public static int NumberOfNoodles
    {
        get => numberOfNoodles;
        set
        {
            if (value == 0)
            {
                value = 1;
            }
            numberOfNoodles = value;
        }
    }
    
    // Noodle min and max speed.
    private static float noodleSpeedTop = 4;
    public static float NoodleSpeedTop
    {
        get => noodleSpeedTop;
        set
        {
            if (value > 15)
                value = 15;
            noodleSpeedTop = value;
        }
    }
    private static float noodleSpeedBottom = 2;
    public static float NoodleSpeedBottom
    {
        get => noodleSpeedBottom;
        set
        {
            if (value < 1)
                value = 2;
            noodleSpeedBottom = value;
        }
    }

    // Total amount of noodles grabbed. Score.
    private static int noodleScore;
    public static int NoodleScore
    {
        get => noodleScore;
        set => noodleScore = value;
    }
    
    
    // ================ Flumes ================ //
    
    // Amount of noodles required to close flumes.
    private static double firstNoodleCloseFlumeScore;
    public static double FirstNoodleCloseFlumeScore
    {
        get => firstNoodleCloseFlumeScore;
        set => firstNoodleCloseFlumeScore = value;
    }
    private static double secondNoodleCloseFlumeScore;
    public static double SecondNoodleCloseFlumeScore
    {
        get => secondNoodleCloseFlumeScore;
        set => secondNoodleCloseFlumeScore = value;
    }
    private static double winNoodleCloseFlumeScore;
    public static double WinNoodleCloseFlumeScore
    {
        get => winNoodleCloseFlumeScore;
        set => winNoodleCloseFlumeScore = value;
    }
    
    // Current state of open/closed flumes.
    private static int flumeStatus = 3;
    public static int FlumeStatus
    {
        get => flumeStatus;
        set
        {
            flumeStatus = value;
        }
    }
}