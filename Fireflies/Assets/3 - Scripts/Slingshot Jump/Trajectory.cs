using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    LineRenderer lr;
    public int Points;


    public GameObject Player;

    private float collisionCheckRadius = 0.1f;

    public float TimeOfSimulation;

    private bool oneCheck = true;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.startColor = Color.white;
    }

    /*
    public List<Vector2> SimulateArc()
    {
        Debug.Log("simulating arc");
        float simulateForDuration = TimeOfSimulation;
        float simulationStep = 0.1f;//Will add a point every 0.1 secs.

        int steps = (int)(simulateForDuration / simulationStep);
        List<Vector2> lineRendererPoints = new List<Vector2>();
        Vector2 calculatedPosition;
        Vector2 directionVector = Player.GetComponentInChildren<SlingshotController>().impulseVector ;// The direction it should go
        Vector2 launchPosition = transform.position;//Position where you launch from
        float launchSpeed = Player.GetComponentInChildren<SlingshotController>().impulseVector.magnitude;//The initial power applied on the player

        for (int i = 0; i < steps; ++i)
        {
            calculatedPosition = launchPosition + (directionVector * (launchSpeed * i * simulationStep));
            //Calculate gravity
            calculatedPosition.y += Physics2D.gravity.y * (i * simulationStep);
            lineRendererPoints.Add(calculatedPosition);
            /*
            if (CheckForCollision(calculatedPosition))//if you hit something
            {
                break;//stop adding positions
            }
            */
    /*
}

return lineRendererPoints;



}
*/
    public void SimulateArc()
    {
        float simulateForDuration = 5f;//simulate for 5 secs in the furture
        float simulationStep = 0.1f;//Will add a point every 0.1 secs.

        int steps = (int)(simulateForDuration / simulationStep);//50 in this example
        List<Vector2> lineRendererPoints = new List<Vector2>();
        Vector2 calculatedPosition;
        Vector2 directionVector = Player.GetComponentInChildren<SlingshotController>().impulseVector;//You plug you own direction here this is just an example
        Vector2 launchPosition = gameObject.transform.position;//Position where you launch from
        float launchSpeed = Player.GetComponentInChildren<SlingshotController>().impulseVector.magnitude * 2.75f;//Example speed per secs.

        for (int i = 0; i < steps; ++i)
        {
            calculatedPosition = launchPosition + (directionVector * (launchSpeed * i * simulationStep));
            //Calculate gravity
            calculatedPosition.y += Physics2D.gravity.y * (i * simulationStep) * (i * simulationStep);
            lineRendererPoints.Add(calculatedPosition);
            //if (CheckForCollision(calculatedPosition))//if you hit something
            //{
            //    break;//stop adding positions
            //}
            lr.positionCount = steps;
            lr.SetPosition(i, calculatedPosition);
        }

        //Assign all the positions to the line renderer.

        

    }

    /*
    public void EnterArc()
    {
        
        if (oneCheck)
        {
            oneCheck = false;
            
            lr.positionCount = SimulateArc().Count;

            for (int a = 0; a < lr.positionCount; a++)
            {
                lr.SetPosition(a, SimulateArc()[a]);
            }
        //}
    }
*/
    public void enterArc()
    {
        lr.enabled = true;
    }
    public void exitArc()
    {
        //lr.positionCount = 0;
        lr.enabled = false;
        //oneCheck = true;
    }
}