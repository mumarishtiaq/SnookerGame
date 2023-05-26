using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
     public int ID;
    public int Score;
    public Image timerImage;
    public Image disableImage;
    public TextMeshProUGUI scoreText;



    private void Reset()
    {
        ID = Convert.ToInt32(gameObject.name.Substring(gameObject.name.IndexOf("_") + 1, 1));
        timerImage = GameObject.Find("PLayer" + ID + "TimerImage").GetComponent<Image>();
        disableImage = GameObject.Find("PLayer" + ID + "DisableImage").GetComponent<Image>();

        scoreText = ScriptReference.instance.uIController.gameObject.transform.Find("MainParent/Player"+ID).transform.Find("Score").GetComponent<TextMeshProUGUI>();
    }




}
