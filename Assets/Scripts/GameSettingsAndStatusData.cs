using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public static class GameSettingsAndStatusData
{
    // =============== Noodles ================ //
    
    // Noodle min and max speed.
    private static float noodleSpeedTop = 10;
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
    
    // Amount of noodles missed allowed.
    private static int missesAllowed = 3;
    public static int MissesAllowed
    {
        get => missesAllowed;
        set => missesAllowed = value;
    }
    
    // Total amount of noodles grabbed. Score.
    private static int noodleScore;
    public static int NoodleScore
    {
        get => noodleScore;
        set => noodleScore = value;
    }
    
    // For multiplayer.
    private static int numberOfNoodles = 30;
    public static int NumberOfNoodles
    {
        get => numberOfNoodles;
        set => numberOfNoodles = value;
    }
    
    
    // ================ Flumes ================ //
    
    // Amount of noodles required to close flumes.
    private static int firstNoodleCloseFlumeScore;
    public static int FirstNoodleCloseFlumeScore
    {
        get => firstNoodleCloseFlumeScore;
        set => firstNoodleCloseFlumeScore = value;
    }
    private static int secondNoodleCloseFlumeScore;
    public static int SecondNoodleCloseFlumeScore
    {
        get => secondNoodleCloseFlumeScore;
        set => secondNoodleCloseFlumeScore = value;
    }
    private static int winNoodleCloseFlumeScore;
    public static int WinNoodleCloseFlumeScore
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
