using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    private void OnEnable()
    {
        MyCallBackActions.SetScoreAction += SetScore;
        MyCallBackActions.TimerRunsOutAction += TimerRunsOut;
        MyCallBackActions.WrongBallCollisionAction += WrongBallCollision;
        MyCallBackActions.WrongBallPottedAction += WrongBallPotted;
    }

    private void OnDisable()
    {
        MyCallBackActions.SetScoreAction -= SetScore;       
        MyCallBackActions.TimerRunsOutAction -= TimerRunsOut;
        MyCallBackActions.WrongBallCollisionAction -= WrongBallCollision;
        MyCallBackActions.WrongBallPottedAction -= WrongBallPotted;
    }

    //if timer runs out, this function will invoke
    private void TimerRunsOut(Player currentPlayer, int currentSelectedBallPoints)
    {
        if (currentSelectedBallPoints < 4) { currentSelectedBallPoints = 4; }
        StartCoroutine(SetScoreCouroutine(currentPlayer, (-currentSelectedBallPoints)));
    }

    //if player hits wrong ball, this function will invoke
    private void WrongBallCollision(Player currentPlayer,int SelectedBallPoints,BallDetailsScript WrongBallCollided)
    {
        //if cue ball doesnt collided with any ball
        if(WrongBallCollided==null)
        {
            NoCueballCollision(currentPlayer,SelectedBallPoints);
            return;
        }
        if (SelectedBallPoints != WrongBallCollided.ball.ballpoints)
        {
            Debug.Log("cue ball doesnt hitted with selected ball ");
            StartCoroutine(SetScoreCouroutine(currentPlayer, (-SelectedBallPoints)));
        }
        else if (SelectedBallPoints == WrongBallCollided.ball.ballpoints)
        {
            Debug.Log("cue ball hitted with selected ball ");
        }
    }

    //if cue ball doesnt collided any ball after the shot hit, this function will invoke
    private void NoCueballCollision(Player currentPlayer, int SelectedBallPoints)
    {
        StartCoroutine(SetScoreCouroutine(currentPlayer, (-SelectedBallPoints)));
        Debug.Log("cue ball doesnt hitted with any ball");
    }

    //if wrong ball potted or cue ball potted, this function will invoke
    private void WrongBallPotted(Player currentPlayer, List<GameObject> pottedBallList, int selectedBallPoints)
    {
        /*if (pottedBallList[0].GetComponent<BallDetailsScript>().ball.ballpoints!=selectedBallPoints)
        {
            StartCoroutine(SetScoreCouroutine(currentPlayer, (-selectedBallPoints)));
        }*/
        int score=0;
        foreach (var item in pottedBallList)
        {
            var ball = item.GetComponent<BallDetailsScript>();
            if(ball.ball.ballpoints==selectedBallPoints)
            {
                score= score+(ball.ball.ballpoints);
            Debug.Log("Score adding");
                

            }
            else
            {
                score = score - (ball.ball.ballpoints);
                Debug.Log("Score subtracting");
            }


        }
            Debug.Log("Score added is : "+score);
            StartCoroutine(SetScoreCouroutine(currentPlayer, (score)));
       
    }




    private IEnumerator SetScoreCouroutine(Player currentPlayer, int score)
    {
        yield return new WaitForSeconds(0.3f);
        SetScore(currentPlayer, score);
    }

    public void SetScore (Player currentPlayer, int score)
    {
        currentPlayer.Score =(currentPlayer.Score+score);
       currentPlayer.scoreText.text= currentPlayer.Score.ToString();
    }


   



    
}
