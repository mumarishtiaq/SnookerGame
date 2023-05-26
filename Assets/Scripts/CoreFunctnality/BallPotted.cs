using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPotted : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Balls"))
        {
            BallDetailsScript ball = collision.gameObject.GetComponent<BallDetailsScript>();

           /* rb=collision.gameObject.GetComponent<Rigidbody>();
            Debug.Log("Velocity"+rb.velocity);*/
           
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        rb = other.gameObject.GetComponent<Rigidbody>();
        Debug.Log("Velocity" + rb.velocity);
        rb.velocity = Vector3.zero;
        rb.constraints=RigidbodyConstraints.None;

        
        MyCallBackActions.PopulatePottedBallsListAction?.Invoke(other.gameObject);

       /* if(other.gameObject.GetComponent<BallDetailsScript>().ball.ballpoints==ScriptReference.instance.mainController.currentSelectedBallPoints)
        {
            MyCallBackActions.SetScoreAction?.Invoke(ScriptReference.instance.playerController.currentPlayer, ScriptReference.instance.mainController.currentSelectedBallPoints);
        }*/
    }


}
