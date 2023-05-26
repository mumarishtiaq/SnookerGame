using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController :MonoBehaviour
{
    [SerializeField] private GameObject Player1;
    [SerializeField] private GameObject Player2;

    [Space]
     public  Player currentPlayer;
    [SerializeField] private Player AnotherPlayer;

    private enum whoWillBeStartingPlayer 
    {
        Player1,
        Player2
    }

    [SerializeField] private whoWillBeStartingPlayer WhoWillBeStartingPlayer;

    private void Reset()
    {
       GetPlayers();
    }

    private void Awake()
    {
        MyCallBackActions.AssignPlayerAtStartAction += AssignPlayerAtStart;
        MyCallBackActions.SwitchPLayerAction += SwitchPlayer;
        MyCallBackActions.CurrentPlayerAgainAction += currentPlayerAgain;
    
    }

    private void Start()
    {
        AssignPlayerAtStart(false);
        MyCallBackActions.SetScoreAction?.Invoke(currentPlayer, 0);
        MyCallBackActions.SetScoreAction?.Invoke(AnotherPlayer, 0);
       
    }

    public void EnablePlayer(bool isStartTimer)
    {
        currentPlayer.timerImage.fillAmount = 1;
        currentPlayer.gameObject.SetActive(true);
        currentPlayer.disableImage.gameObject.SetActive(false);
          
        AnotherPlayer.timerImage.fillAmount = 0;
        AnotherPlayer.gameObject.SetActive(false);
        AnotherPlayer.disableImage.gameObject.SetActive(true);


        ScriptReference.instance.timer.TimerImage=currentPlayer.timerImage;
        MyCallBackActions.ResetTimerSpriteAction?.Invoke(currentPlayer.timerImage);
        MyCallBackActions.EnableTimerAction?.Invoke(isStartTimer);
        Debug.Log("Current player is " + currentPlayer);

    }

    public void SwitchPlayer(float delay)
    {
        StartCoroutine(SwitchPlayerCouroutine(delay));
    }
    IEnumerator SwitchPlayerCouroutine(float delay)
    {
        MyCallBackActions.EnableTimerAction?.Invoke(false);
        yield return new WaitForSeconds(delay);
        Player tempPlayer = currentPlayer;
        currentPlayer = AnotherPlayer;
        AnotherPlayer = tempPlayer;
        Debug.Log("Player switched");
        EnablePlayer(true);
        MyCallBackActions.OutlineStatusOfAllBallsAction?.Invoke(true, true);
    }

    void currentPlayerAgain(float delay)
    {
        StartCoroutine(currentPlayerAgainCouroutine(delay));
    }

    IEnumerator currentPlayerAgainCouroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        EnablePlayer(true);
        MyCallBackActions.OutlineStatusOfAllBallsAction?.Invoke(true, true);
    }


    #region Helping Functions

    void GetPlayers()
    {
        foreach (var player in GetComponentsInChildren<Player>())
        {
            if (player.ID == 1)
            {
                Player1 = player.gameObject;
            }
            else
            {
                Player2 = player.gameObject;
            }
        }
    }

    void AssignPlayerAtStart(bool isStartTimer)
    {      
        if(WhoWillBeStartingPlayer == whoWillBeStartingPlayer.Player1)
        {
            currentPlayer = Player1.GetComponent<Player>();
            AnotherPlayer = Player2.GetComponent<Player>();
        }
        else
        {
            currentPlayer = Player2.GetComponent<Player>();
            AnotherPlayer = Player1.GetComponent<Player>();
        }
            EnablePlayer(isStartTimer);      
    }

    #endregion Helping Functions
}
