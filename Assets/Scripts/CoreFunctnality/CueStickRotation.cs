using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class CueStickRotation : MonoBehaviour
{
    InputManager inputManager;
    private bool rotateAllowed;
    Vector2 rotation;
    [SerializeField] float rotateSpeed = 0.5f;
    [SerializeField] private bool rotationEnabled= false;


    


    private void Awake()
    {
        inputManager = new InputManager();
        inputManager.CueStickRotation.Press.Enable();
        inputManager.CueStickRotation.Axis.Enable();
        inputManager.CueStickRotation.Press.performed += _ => { StartCoroutine(RotateCue()); };
        inputManager.CueStickRotation.Press.canceled  += _ => { rotateAllowed = false; };

        inputManager.CueStickRotation.Axis.performed += context => { rotation = context.ReadValue<Vector2>(); };

        MyCallBackActions.SetStatusRotationEnabledAction += SetRotationEnabled;

        SetRotationEnabled(false);
    

        

        

        
    }

    IEnumerator RotateCue()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (!rotationEnabled) { yield break;  }
            rotateAllowed = true;
            while (rotateAllowed)
            {
                //apply rotation
                rotation *= rotateSpeed;
                transform.Rotate(-Vector3.up, rotation.x, Space.Self);
                //Debug.Log(transform.rotation);
                yield return null;
            }
        }
    }

    private void SetRotationEnabled(bool status)
    {
        rotationEnabled = status;
    }

   
}
