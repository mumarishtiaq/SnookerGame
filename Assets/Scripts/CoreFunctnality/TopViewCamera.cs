using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopViewCamera : MonoBehaviour
{
    [SerializeField] private float MapWidth;
    [SerializeField] private float MapHeight;
    [SerializeField] private float XAxis;
    [SerializeField] private float YAxis;
    [SerializeField] private Camera topViewCamera;
    [SerializeField] private GameObject panel;
    private Transform topViewPos;


    private void Awake()
    {
        MyCallBackActions.SetTopViewCameraSettingsAction += SetTopViewCameraSettings;
    }

    private void Start()
    {
        topViewCamera=GetComponent<Camera>();
        topViewPos=GetComponent<Transform>();
    }

    private void Update()
    {
        //CheckResolution();
    }

    void CheckResolution()
    {

        float w = Screen.width;
        float h = Screen.height;
        w = w / MapWidth;
        h = h / MapHeight;
        float width = 1 / w;
        float height = 1 / h;
        float widthscale = 1 / (panel.transform.lossyScale.y);
        float heightscale = 1 / (panel.transform.lossyScale.x);
        width = width / widthscale;
        height = height / heightscale;
        float Xaxis = width / XAxis;
        float Yaxis = height / YAxis;
        topViewCamera.rect = new Rect(1 - Xaxis, 1 - Yaxis, width, height);

        // 1170             660         0.7566          0.75

        //1259              660         0.7547          0.75

    }


    void SetTopViewCameraSettings(bool setCameraStatus)
    {
        //settings for Camera
        transform.position=topViewPos.position;
        transform.rotation=topViewPos.rotation;

        // set status of Camera
        gameObject.SetActive(setCameraStatus);
    }
    
}
