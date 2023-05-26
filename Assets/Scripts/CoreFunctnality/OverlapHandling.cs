using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OverlapHandling : MonoBehaviour
{
    [SerializeField] private LayerMask ballLayer;
    [SerializeField] private List<BallDetailsScript> allBalls;
    [SerializeField] private Collider placementBounds;

    private void Reset()
    {
        allBalls = new List<BallDetailsScript>();

        foreach (var item in FindObjectsOfType<BallDetailsScript>())
        {
            allBalls.Add(item);
        }
        allBalls = allBalls.OrderBy(n => n.ball.ballpoints).ToList();
        ballLayer = LayerMask.GetMask("ball");
        placementBounds = GameObject.Find("D PLANNER 00").GetComponent<Collider>();
    }
    public void CheckForOverlapSphere(BallDetailsScript ballDetailScript, Vector3 center,float radius)
    {
        bool sphereOverlap = OverlapChecker(center, radius);
        if (!sphereOverlap)
        {
            Debug.Log("Overlap not detected!");
            ballDetailScript.gameObject.transform.localPosition = ballDetailScript.ball.ResetPosition;

        }
        else
        {
            if (!ballDetailScript.gameObject.name.Contains("CueBall"))
            {


                int myIndex = allBalls.IndexOf(allBalls.Find(item => item.gameObject.name == ballDetailScript.gameObject.name));
                for (int i = myIndex + 1; i < allBalls.Count; i++)
                {
                    sphereOverlap = OverlapChecker(allBalls[i].center, allBalls[i].radius);
                    if (sphereOverlap == false)
                    {
                        ballDetailScript.gameObject.transform.localPosition = allBalls[i].ball.ResetPosition;
                        break;
                    }
                }
                if (sphereOverlap == true) { ballDetailScript.transform.localPosition = new Vector3(2.53800011f, 13.6990499f, 11.3599997f); } //Vector3(2.53800011,13.6990499,11.3599997)
            }
            else
            {
                ResetForCueBall(ballDetailScript);
            }


        }
    }

    private bool OverlapChecker(Vector3 c, float r)
    {
        return Physics.CheckSphere(c, r, ballLayer);
    }
    private void ResetForCueBall(BallDetailsScript ballDetailScript)
    {
        Debug.Log("Resetting Cue ball");
        
        Vector3 randomPosition;
        bool validPosition = false;
        int loopLimit = 0;
        while (!validPosition)
        {
            loopLimit += 1;           
            randomPosition = GetRandomPositionWithinBounds(ballDetailScript);
            validPosition = !CheckCollision(randomPosition);

            if (validPosition)
            {
                ballDetailScript.gameObject.transform.localPosition = randomPosition;
                Debug.Log("found a suitable place to spawn after iterations : "+loopLimit);
            }

            if(loopLimit>300)
            {
                Debug.Log("Loop breaked forcefully after "+loopLimit+ " iterations");
                break;
            }
        }
    }
    private Vector3 GetRandomPositionWithinBounds(BallDetailsScript ballDetailScript)
    {
        Vector3 center = placementBounds.bounds.center;
        Vector3 size = placementBounds.bounds.size;
        Vector3 randomPosition = new Vector3(
           (center.x+ (Random.Range(-size.x / 2f, size.x / 2f))),
            ballDetailScript.gameObject.transform.localPosition.y,
            (center.z + (Random.Range(-size.z / 2f, size.z / 2f)))
        );
        return randomPosition;
    }

    private bool CheckCollision(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, 0.5f,ballLayer);
        foreach (Collider collider in colliders)
        {
            if (collider != placementBounds)
            {
                return true; // Collision detected
            }
        }
        return false; // No collision detected
    }

}
