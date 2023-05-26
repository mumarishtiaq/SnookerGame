using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallInHand : MonoBehaviour
{
    public bool isBallinHand = false;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private LayerMask layerMaskBall;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject handCursor;
    InputManager inputManager;
    bool clickPressed;
    [SerializeField] private Collider placementBounds;


    private void Reset()
    {
        layerMask = LayerMask.GetMask("DPlanner");
        layerMaskBall = LayerMask.GetMask("ball");
        handCursor = GameObject.Find("HandCursor");
        placementBounds = GameObject.Find("D PLANNER 00").GetComponent<Collider>();
    }
    private void Awake()
    {
        inputManager = new InputManager();
        inputManager.BallInHand.ClickPressed.Enable();
        inputManager.BallInHand.ClickPressed.performed += _ => { StartCoroutine(MoveBall()); };
        inputManager.BallInHand.ClickPressed.canceled += _ => { clickPressed= false; };
        MyCallBackActions.isBallInHandAction += SetBallInHandStatus;

        SetBallInHandStatus(false);
    }

   



    private IEnumerator MoveBall()
    {
        Debug.Log("Couroutine");
        if (isBallinHand)
        {
            clickPressed = true;
            while (clickPressed)
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                {
                    Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);
                    Vector3 newPos = new Vector3(hit.point.x, transform.localPosition.y, hit.point.z);
                    transform.localPosition = newPos;
                    Vector3 handPos= transform.position;
                    handCursor.transform.position = handPos;
                    SnapToCollisions();
                }
                else
                {
                    Debug.DrawRay(ray.origin, ray.direction * 100, Color.green);
                }

                Debug.Log("While");
                yield return null;
            }

        }
    }

    private void SnapToCollisions()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1,layerMaskBall); // Adjust the radius as needed

        foreach (Collider collider in colliders)
        {
            if (collider != GetComponent<Collider>()&&placementBounds.bounds.Contains(collider.ClosestPoint(transform.position)))
            {
                transform.position = collider.ClosestPoint(transform.position);
                break;
            }
        }
    }

    void SetBallInHandStatus(bool status)
    {
        handCursor.SetActive(status);
        isBallinHand = status;

        foreach (var item in FindObjectsOfType<BallDetailsScript>())
        {
            if(item.gameObject!=this.gameObject)
            {
                item.myRB.isKinematic = status;
            }
        }
        BallDetailsScript cueBall=gameObject.GetComponent<BallDetailsScript>();
        cueBall.myRB.useGravity = !status;
        cueBall.myRB.isKinematic = false;
    }
}
