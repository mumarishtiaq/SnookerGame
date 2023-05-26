using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MyCallBackActions
{
    public static Action<float> SwitchPLayerAction;
    public static Action<bool> EnableTimerAction;
    public static Action<Image> ResetTimerSpriteAction;
    public static Action<float> CurrentPlayerAgainAction;
    public static Action<bool,bool> OutlineStatusOfAllBallsAction;

    public static Action<Color,int> SelectedBallDisplayAction;

    public static Action<bool> SetTopViewCameraSettingsAction;

    public static Action<bool> AssignPlayerAtStartAction;

    
    public static Action<bool> SetStatusOfObjectsAction;

    public static Action<Player,int> SetScoreAction;
   
    
    public static Action<int> AssignCurrentBallPointsAction;

    public static Action<GameObject> PopulatePottedBallsListAction;
    
    
    
    public static Action<Player,int> TimerRunsOutAction;
    public static Action<Player,int,BallDetailsScript> WrongBallCollisionAction;
    public static Action<Player,List<GameObject>,int> WrongBallPottedAction;

    public static Action<bool> SetStatusRotationEnabledAction;
    public static Action<bool> isBallInHandAction;
    public static Action SetCueStickPositionAction;





    



 



   

}
