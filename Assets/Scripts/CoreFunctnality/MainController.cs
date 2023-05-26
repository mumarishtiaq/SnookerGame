using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor.Build.Content;
using cakeslice;

public class MainController : MonoBehaviour
{
    public Color BallHighlightColor;
    public int currentSelectedBallPoints;
    public bool isBallClickEnabled;
    public BallDetailsScript firstCollidedBall;
    public bool isFirstCollided = false;

    [SerializeField] private float shotSpeed;
    [SerializeField] private Rigidbody CueBallRigidbody;


    [SerializeField] private GameObject CueBall;
    [SerializeField] private GameObject CueStick;


    [SerializeField] private float ShotMaxPower;

    public List<GameObject> PottedBalls = new List<GameObject>() ;

    [SerializeField] private bool isSwitchingRequired=false;

    [SerializeField] GameObject TopViewCamera;
    [SerializeField] GameObject MainCamera;


    





    #region Events

    public static event Delegates.OnResetPositionsDlg onResetPositionsEvent;

    #endregion Events

    private void Awake()
    {
        //myslider = ScriptReference.instance.uIController.shotPowerSlider;
       
    }

    private void OnEnable()
    {
        UIController.OnPowerSliderReleasedEvent += SliderReleased;
        MyCallBackActions.OutlineStatusOfAllBallsAction += SetOutlineStatusOfAll;
        MyCallBackActions.SetStatusOfObjectsAction += SetStatusOfObjects;
        MyCallBackActions.AssignCurrentBallPointsAction += AssignCurrentBallpoints;
        MyCallBackActions.PopulatePottedBallsListAction += PopulatePottedBallsList;
        MyCallBackActions.SetCueStickPositionAction += CueStickPosition;



    }
    void Start()
    {
        CueBallRigidbody = CueBall.GetComponent<Rigidbody>();
        StartCoroutine(SetCueStickPosition(0.1f));
        BallHighlightColor = ScriptReference.instance.outlineEffect.lineColor0;
        MyCallBackActions.SetStatusOfObjectsAction?.Invoke(false);

    }
    int counter=1;

    private void Update()
    {
        
        if (CueBallRigidbody.IsSleeping()==true)
        {         
            if(counter==0)
            {
                ResetPottedBalls();
                if (isSwitchingRequired) { MyCallBackActions.SwitchPLayerAction?.Invoke(0.5f); Debug.Log("Switching player from maincontroller"); }
                else { MyCallBackActions.CurrentPlayerAgainAction?.Invoke(0.5f); }
                //checking if the cue ball hits with the selected ball
                MyCallBackActions.WrongBallCollisionAction?.Invoke(ScriptReference.instance.playerController.currentPlayer, currentSelectedBallPoints, firstCollidedBall);
                if(PottedBalls.Count>=1)
                {
                    MyCallBackActions.WrongBallPottedAction?.Invoke(ScriptReference.instance.playerController.currentPlayer, PottedBalls, currentSelectedBallPoints);
                }
                PottedBalls.Clear();
                currentSelectedBallPoints = 0;
                StartCoroutine(SetCueStickPosition(0.5f));
                counter = 1;   
                
               
            }
        }

        
    }

    void SliderReleased(float sliderValue)
    {
        if (sliderValue == ShotMaxPower) { return; }
        MyCallBackActions.EnableTimerAction?.Invoke(false);
        sliderValue = Mathf.Abs(sliderValue - ShotMaxPower);
        shotSpeed = sliderValue;
        HitShotball();
    }
    public void HitShotball()
    {
        //shotSpeed = int.Parse(SpeedField.text);
        CueBall.transform.rotation = CueStick.transform.rotation;      
        StartCoroutine(HitShotDelay());
        
        
        
        //rb.velocity = Vector3.forward * 2;
        //rb.AddTorque(Vector3.forward * speed);
        //rb.AddForceAtPosition(Vector3.forward * speed, transform.position, ForceMode.Force);
    }
    
    IEnumerator HitShotDelay()
    {
        yield return new WaitForSeconds(0.1f);
        CueBallRigidbody.AddRelativeForce(Vector3.forward * shotSpeed, ForceMode.Force);
        counter = 0;
        ScriptReference.instance.aimingProjection.line.gameObject.SetActive(false);
        ScriptReference.instance.uIController.shotPowerSlider.value = ShotMaxPower;
        yield return new WaitForSeconds(0.5f);
        ScriptReference.instance.uIController.shotPowerSlider.gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        CueStick.transform.GetChild(0).gameObject.SetActive(false);

    }
    public void ResetPositions()
    {
        onResetPositionsEvent?.Invoke();
        StartCoroutine(SetCueStickPosition(0.1f));
    }
    
    IEnumerator SetCueStickPosition(float delayAmount)
    {
        yield return new WaitForSeconds(delayAmount);
        CueStick.transform.localPosition = CueBall.transform.localPosition;
        Vector3 FirstChildPosition = CueStick.transform.GetChild(0).localPosition;
        CueStick.transform.GetChild(0).localPosition = new Vector3(FirstChildPosition.x, FirstChildPosition.y, -12.034f);
       // CueStick.transform.GetChild(0).gameObject.SetActive(true);
        //MyCallBackActions.SetStatusOfObjectsAction?.Invoke(false);


    }
    private void CueStickPosition() 
    {
        StartCoroutine(SetCueStickPosition(0.001f));
    }

    void  ResetPottedBalls()
    {
        
        if (PottedBalls.Count == 0) 
        {
            isSwitchingRequired = true;
            return ; 
        }
        else
        {
            isSwitchingRequired = true;
            foreach (var item in PottedBalls)
            {
                var ball = item.GetComponent<BallDetailsScript>();
                ball.ResetBallPosition();
                if(ball.ball.ballpoints==currentSelectedBallPoints)
                {
                    isSwitchingRequired = false;
                }
            }
            //PottedBalls.Clear();
        }

       
    }
    private void PopulatePottedBallsList(GameObject ball)
    {
        PottedBalls.Add(ball);
    }


    void SetOutlineStatusOfAll(bool status,bool isStartHighlightAnimation)
    {
        foreach (var item in FindObjectsOfType<OutlineShader>())
        {
            item.enabled= status;
        }

        if(isStartHighlightAnimation)
        {
            ScriptReference.instance.outlineAnimation.enabled= true;
            isBallClickEnabled= true;
        }
    }

   public void BeginMatch(GameObject beginButton)
    {
        StartCoroutine(BeginMatchCouroutine(beginButton));

    }

    IEnumerator BeginMatchCouroutine(GameObject beginButton)
    {
        //turn off Rules Panel/Begin button

        LeanTween.scale(beginButton, Vector3.zero, 0.6f).setEaseInOutBack();
        yield return new WaitForSeconds(0.7f);

        //lean tween camera from top to cue position
        LeanTweenFromTopToCueBall();

        yield return new WaitForSeconds(1);
        ScriptReference.instance.topViewCamera.gameObject.SetActive(false);

        //turm of Top view camera
        MyCallBackActions.SetTopViewCameraSettingsAction?.Invoke(false);

        //after few seconds turn on highlighters
        yield return new WaitForSeconds(0.3f);
        SetOutlineStatusOfAll(true, true);


        //now turn on time for starting player
        yield return new WaitForSeconds(0.3f);
        MyCallBackActions.AssignPlayerAtStartAction?.Invoke(true);

        //ball clicking enabled
        isBallClickEnabled = true;

        //turn on ball in hand action
        MyCallBackActions.isBallInHandAction?.Invoke(true);
    }
    void LeanTweenFromTopToCueBall()
    {      
        LeanTween.move(ScriptReference.instance.topViewCamera.gameObject, MainCamera.transform.position, 1);
        var rotation = new Vector3(25.2664165f, -1.649f, -0.083f);
        LeanTween.rotateLocal(ScriptReference.instance.topViewCamera.gameObject, rotation, 1);
    }


    void SetStatusOfObjects(bool status)
    {
        //foreach (var item in CueStick.GetComponentsInChildren<MeshRenderer>())
        //{
        //    item.enabled = status;
        //}
        CueStick.transform.GetChild(0).gameObject.SetActive(status);

        ScriptReference.instance.uIController.shotPowerSlider.gameObject.SetActive(status);
        ScriptReference.instance.aimingProjection.line.gameObject.SetActive(status);
        ScriptReference.instance.uIController.selectedBallDisplayPanel.gameObject.SetActive(status);
        

   
    }

    private void AssignCurrentBallpoints(int points)
    {
        currentSelectedBallPoints = points;
    }


    public void Test(float posX) 
    {
        CueStick.transform.position = new Vector3(posX,CueStick.transform.position.y,CueStick.transform.position.z);
    }
}
