using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject selectedBallDisplayPanel;
    public Slider shotPowerSlider;
    public TextMeshProUGUI player1ScoreTxt;
    public TextMeshProUGUI player2ScoreTxt;

    [SerializeField] Image selectedBallDisplayImage;
    [SerializeField] TextMeshProUGUI selectedBallDisplayText;
    [SerializeField] Button BeginMatchButton;
    



    #region Events

    public static event Delegates.OnPowerSliderReleasedDlg OnPowerSliderReleasedEvent;
    #endregion Events
    private void Reset()
    {
        shotPowerSlider = transform.Find("ShotPowerSlider").GetComponent<Slider>();
        selectedBallDisplayPanel = transform.Find("SelectedBallDisplayPanel").gameObject;
        selectedBallDisplayImage = selectedBallDisplayPanel.transform.Find("Image").GetComponent<Image>();
        selectedBallDisplayText = selectedBallDisplayPanel.transform.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        BeginMatchButton = transform.Find("BeginMatchButton").GetComponent<Button>();

        player1ScoreTxt = transform.Find("MainParent/Player1").transform.Find("Score").GetComponent<TextMeshProUGUI>();
        player2ScoreTxt = transform.Find("MainParent/Player2").transform.Find("Score").GetComponent<TextMeshProUGUI>();
        

        
    }

    private void Start()
    {
        selectedBallDisplayPanel.SetActive(false);
        LeanTween.scale(BeginMatchButton.gameObject, Vector3.one, 0.6f).setEaseOutBack();

        shotPowerSlider.gameObject.SetActive(false);

    }

    private void Awake()
    {
        MyCallBackActions.SelectedBallDisplayAction += TurnOnSelectedBallDisplay;
    }


    public void OnSliderReleased()
    {      
        OnPowerSliderReleasedEvent?.Invoke(shotPowerSlider.value);

    }

    void TurnOnSelectedBallDisplay(Color ballColor,int BallPoints)
    {
        selectedBallDisplayImage.color = ballColor;
        selectedBallDisplayText.text = BallPoints.ToString();

        selectedBallDisplayPanel.SetActive(true);
    }

   /* public  Func<int, TextMeshProUGUI> AssignScoreTextToPlayersAction=(id);
    {
        
        if (id == 1)
        {
            return player1ScoreTxt;
        }
        return player2ScoreTxt;
    }*/


}
