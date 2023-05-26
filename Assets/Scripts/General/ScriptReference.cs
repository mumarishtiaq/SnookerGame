using cakeslice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptReference : MonoBehaviour
{
    public static ScriptReference instance;

    public  ScriptReference scpRef;

    public MainController mainController;

    public CueStickRotation cueStickRotation;

    public UIController uIController;

    public AimingProjection aimingProjection;

    public PlayerController playerController;

    public OutlineEffect outlineEffect;

    public OutlineAnimation outlineAnimation;

    public Timer timer;

    public TopViewCamera topViewCamera;

    public ScoreManager scoreManager;


    private void Reset()
    {

        instance = this;
        scpRef = this;
        mainController = FindObjectOfType<MainController>(true);
        cueStickRotation = FindObjectOfType<CueStickRotation>(true);
        uIController = FindObjectOfType<UIController>(true);
        aimingProjection=FindObjectOfType<AimingProjection>(true);
        playerController = FindObjectOfType<PlayerController>(true);
        timer = FindObjectOfType<Timer>(true);

        outlineEffect = FindObjectOfType<OutlineEffect>(true);
        outlineAnimation = outlineEffect.gameObject.GetComponent<OutlineAnimation>();
        topViewCamera = FindObjectOfType<TopViewCamera>(true);

        scoreManager = FindObjectOfType<ScoreManager>(true);
        

    }

    private void Awake()
    {
        instance = this;
    }
}
