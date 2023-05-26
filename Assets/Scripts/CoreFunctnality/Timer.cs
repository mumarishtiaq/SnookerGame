using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
    public bool isTimerStart;
    public Image TimerImage;

    [SerializeField] private float timerValue;
    [SerializeField] private float timerCurrent;

    [SerializeField] TMP_Text textToDisplay;
    [SerializeField] float perSecFill;

    

    [SerializeField] Sprite redSprite;
    [SerializeField] Sprite greenSprite;

    private void Awake()
    {
        MyCallBackActions.EnableTimerAction += EnableTimer;
        MyCallBackActions.ResetTimerSpriteAction += ResetTimerSprite;
    }
    private void Start()
    {
        timerCurrent = timerValue;
        perSecFill = 1 / timerValue;
        isTimerStart = false;
    }

    void Update()
    {
        if (isTimerStart == false) { return; }

        if (timerCurrent > 0.01f)
        {
            timerCurrent -= Time.deltaTime;
            TimeCalculater(timerCurrent);
        }
        else
        {
            if(isTimerStart)
            {
                MyCallBackActions.TimerRunsOutAction?.Invoke(ScriptReference.instance.playerController.currentPlayer, ScriptReference.instance.mainController.currentSelectedBallPoints);
                MyCallBackActions.SwitchPLayerAction?.Invoke(0.5f);
                Debug.Log("Switching player from time");
            }
        }
    }






    void TimeCalculater(float timeToDIsplay)
    {
        Debug.Log("Time Running");
        float Seconds = Mathf.FloorToInt(timeToDIsplay % 60);
        textToDisplay.text = Seconds.ToString();

        if (timeToDIsplay <= 8)
        {
            textToDisplay.color = Color.red;
            TimerImage.sprite = redSprite;
        }
        SetImageFill(timeToDIsplay);
    }


    void SetImageFill(float time)
    {
        TimerImage.fillAmount = time*perSecFill;
    }

    public void EnableTimer(bool status)
    {
        if (status)
        {
            StartCoroutine(EnableTimerCouroutine());
        }
        else
        {
            isTimerStart = status;
        }
        Debug.Log("status "+ status);
    }
   

    

    IEnumerator EnableTimerCouroutine()
    {
        timerCurrent = timerValue;
        yield return new WaitForSeconds(1);
        isTimerStart = true;
    }

    void ResetTimerSprite(Image playerSprite)
    {
        playerSprite.sprite = greenSprite;
    }
}
