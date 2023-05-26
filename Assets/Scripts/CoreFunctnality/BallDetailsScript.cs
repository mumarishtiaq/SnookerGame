using cakeslice;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class BallDetailsScript : MonoBehaviour
{
    public BallScriptable ball;
    public Rigidbody myRB;
    public OutlineShader outlineShader;
    
    [SerializeField] private Color myColor;
    [SerializeField] private string scriptableObjectPath;

    public float radius;
    public Vector3 center;
    [SerializeField] private LayerMask ballLayer;
    [SerializeField] private OverlapHandling overlapHandling;
    




    private void Awake()
    {
        MainController.onResetPositionsEvent += ResetBallPosition;
        //MyCallBackActions.OutlineStatusAction += SetOutlineStatus;
       
    }

    private void Reset()
    {
        myRB = GetComponent<Rigidbody>();
        if (GetComponent<OutlineShader>()) { outlineShader = GetComponent<OutlineShader>(); }     
        myColor = GetComponent<MeshRenderer>().material.color;

        GetBallScriptableObject();


        transform.localPosition = ball.ResetPosition;
        center=transform.localPosition;
        radius = 0.7f;
        ballLayer = LayerMask.GetMask("ball");
        overlapHandling=transform.parent.GetComponent<OverlapHandling>();

        
    }

    private void Start()
    {
        if (!gameObject.name.Contains("CueBall"))
        {
            outlineShader.enabled = false;
        }
    }



    private void OnMouseDown()
    {
        OnBallClick();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.name.Contains("CueBall"))
        {
            if(collision.gameObject.GetComponent<BallDetailsScript>())
            {
                if(ScriptReference.instance.mainController.isFirstCollided == false)
                {
                    ScriptReference.instance.mainController.firstCollidedBall=collision.gameObject.GetComponent<BallDetailsScript>();
                    ScriptReference.instance.mainController.isFirstCollided = true;
                }
            }
        }

        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(center, radius);
    }

    [ContextMenu("test overlap")]
    public void ResetBallPosition()
    {
        myRB.isKinematic = true;
        overlapHandling.CheckForOverlapSphere(this,center,radius);
        transform.rotation = Quaternion.identity;
        myRB.isKinematic = false;
        myRB.constraints = RigidbodyConstraints.FreezePositionY;

        

    }

    
    private void GetBallScriptableObject()
    {
        scriptableObjectPath = @"Assets\Scripts\ScriptableObjects/"+gameObject.name+ ".asset";
#if UNITY_EDITOR
        ball = AssetDatabase.LoadAssetAtPath<BallScriptable>(scriptableObjectPath);
#endif
    }
        

    void OnBallClick()
    {
        if (gameObject.name.Contains("CueBall")|| !ScriptReference.instance.mainController.isBallClickEnabled) { return; }

        //set cue stick position at cue ball position
        MyCallBackActions.SetCueStickPositionAction?.Invoke();

        //turning off all Highlights
        MyCallBackActions.OutlineStatusOfAllBallsAction?.Invoke(false, false);

        //stop highlight animation
        ScriptReference.instance.outlineAnimation.enabled = false;

        //reset highlight color (outline effect attached on mainCamera)
        ScriptReference.instance.outlineEffect.lineColor0 = ScriptReference.instance.mainController.BallHighlightColor;

        //turn on highlight of this ball
        SetOutlineStatus(true,outlineShader);

        //turn off highlight of this ball after few seconds
        StartCoroutine(TurnOffHighlightAfterFewSeconds());

        //turn on selected ball display
        MyCallBackActions.SelectedBallDisplayAction?.Invoke(myColor, ball.ballpoints);

        //assign current ball points in mainController
        MyCallBackActions.AssignCurrentBallPointsAction?.Invoke(ball.ballpoints);


        //re-enable some extra objects --------i.e shot power slider, cue stick , line projection,selected ball display panel

        MyCallBackActions.SetStatusOfObjectsAction?.Invoke(true);

        //turn off hand link cursor
        MyCallBackActions.isBallInHandAction?.Invoke(false);

        //ball clicking disabled
        ScriptReference.instance.mainController.isBallClickEnabled = false;

        // first collision off
        ScriptReference.instance.mainController.isFirstCollided = false;

        ScriptReference.instance.mainController.firstCollidedBall = null;

        //turn on cue stick rotation
        MyCallBackActions.SetStatusRotationEnabledAction?.Invoke(true);



    }

    void SetOutlineStatus(bool status,OutlineShader outline)
    {
        outline.enabled = status;
    }

   

    IEnumerator TurnOffHighlightAfterFewSeconds()
    {
        yield return new WaitForSeconds(3);
        SetOutlineStatus(false,outlineShader);

        //Display Selected Ball
    }



}
