using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AimingProjection : MonoBehaviour
{
    #region Commented
    //private UnityEngine.SceneManagement.Scene simulationScene;
    //private PhysicsScene physicsScene;
    //[SerializeField] private Transform balls;
    //void Start()
    //{
    //    CreatePhysicsScene();           
    //}
    //void Update()
    //{

    //}


    //private void CreatePhysicsScene()
    //{
    //    simulationScene = SceneManager.CreateScene("Simulation", new CreateSceneParameters(LocalPhysicsMode.Physics3D));
    //    physicsScene = simulationScene.GetPhysicsScene();

    //    foreach (Transform obj in balls)
    //    {
    //        var ghostObj = Instantiate(obj.gameObject, obj.position, obj.rotation);
    //        ghostObj.GetComponent<Renderer>().enabled = false;
    //        SceneManager.MoveGameObjectToScene(ghostObj, simulationScene);
    //    }
    //}

    //public void SimulateAimingProjection(GameObject ball,Vector3 pos,Vector3 velocity)
    //{
    //    var ghostObj = Instantiate(ball.gameObject, pos, Quaternion.identity);
    //    ghostObj.GetComponent<Renderer>().enabled = false;
    //    SceneManager.MoveGameObjectToScene(ghostObj, simulationScene);
    //}

    #endregion Commented

    [SerializeField] private int numberOfRays, maxRayDist;
    private GameObject CueStickParent;
    public LineRenderer line;
   
    private void Start()
    {
        CueStickParent = ScriptReference.instance.cueStickRotation.gameObject;
        line.positionCount = numberOfRays + 1;
        /*line.SetPosition(0, CueStickParent.transform.position);
        CastRay(CueStickParent.transform.position, CueStickParent.transform.forward);*/
    }

    private void Update()
    {
        line.SetPosition(0,CueStickParent.transform.position);
        CastRay(CueStickParent.transform.position, CueStickParent.transform.forward);
    }
    private void CastRay(Vector3 rayPos,Vector3 rayDir)
    {

        for (int i = 0; i < numberOfRays; i++)
        {
            var ray = new Ray(rayPos, rayDir);
            if (Physics.Raycast(ray, out var rayHit, maxRayDist))
            {
                line.SetPosition(i + 1, rayHit.point);
                Debug.DrawLine(rayPos, rayHit.point, Color.black);
                rayPos = rayHit.point;
                rayDir = Vector3.Reflect(rayPos, rayHit.normal);

            }
            else
            {
                line.SetPosition(i + 1, rayDir* maxRayDist);
                Debug.DrawRay(rayPos, rayDir* maxRayDist, Color.blue);
               // Debug.Log(rayDir);  
                break;
            }
        }
    }
}
