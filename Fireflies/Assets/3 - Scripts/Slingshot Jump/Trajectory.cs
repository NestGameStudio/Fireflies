using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    LineRenderer lr;
    public int Points;


    public GameObject Player;

    private float collisionCheckRadius = 0.5f;

    public float TimeOfSimulation;

    private bool oneCheck = true;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.startColor = Color.white;
    }

    public void SimulateArc()
    {
        float simulateForDuration = 5f;//simulate for 5 secs in the furture
        float simulationStep = 0.1f;//Will add a point every 0.1 secs.

        int steps = (int)(simulateForDuration / simulationStep);//50 in this example
        List<Vector2> lineRendererPoints = new List<Vector2>();
        Vector2 calculatedPosition;
        Vector2 directionVector = Player.GetComponentInChildren<SlingshotController>().impulseVector;//You plug you own direction here this is just an example
        Vector2 launchPosition = gameObject.transform.position;//Position where you launch from

        //Debug.Log("launchSpeed = " + Player.GetComponentInChildren<SlingshotController>().impulseVector.magnitude);
        float launchSpeed = Player.GetComponentInChildren<SlingshotController>().impulseVector.magnitude * Setup.Instance.ImpulseForce * 3.5f;//Example speed per secs.
        //float launchSpeed = Player.GetComponentInChildren<SlingshotController>().impulse.magnitude * 3.5f;

        //float launchSpeed = Player.GetComponentInChildren<SlingshotController>().impulse.magnitude * 4 - GetComponent<Rigidbody2D>().mass;

        if (Player.GetComponentInChildren<SlingshotController>().impulseVector.magnitude > Setup.Instance.LineMinRadius)
        {

            enterArc();

            for (int i = 0; i < steps; ++i)
            {
                calculatedPosition = launchPosition + (directionVector * ((launchSpeed) * i * simulationStep));
                //Calculate gravity
                calculatedPosition.y += Physics2D.gravity.y * (i * simulationStep) * (i * simulationStep);
                lineRendererPoints.Add(calculatedPosition);
                //if (CheckForCollision(calculatedPosition))//if you hit something
                //{
                //    break;//stop adding positions
                //}

                lr.positionCount = steps;

                lr.SetPosition(i, calculatedPosition);

                //Vector2 positionLerp = Vector2.Lerp(lr.GetPosition(i), calculatedPosition, Time.deltaTime);

                //lr.SetPosition(i, positionLerp);

                if (CheckForCollision(lr.GetPosition(i)))//if you hit something
                {
                    deletePositionsAboveX(i, steps);
                    break;//stop adding positions
                }

            }

            //Assign all the positions to the line renderer.

        }
        else
        {
            exitArc();
        }

    }

    public void enterArc()
    {
        if (lr.enabled == false)
        {
            lr.enabled = true;
        }
    }
    public void exitArc()
    {
        if (lr.enabled)
        {
            //lr.positionCount = 0;
            lr.enabled = false;
            //oneCheck = true;
        }
    }
    void deletePositionsAboveX(int x, int steps)
    {

        lr.positionCount = lr.positionCount - Mathf.Abs(x - steps);
        
    }
    private bool CheckForCollision(Vector2 position)
    {

        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 0;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        //layerMask = ~layerMask;

        Collider2D[] hits = Physics2D.OverlapCircleAll(position, collisionCheckRadius,layerMask);
        if (hits.Length > 0)
        {
            //We hit something 
            //check if its a wall or seomthing
            //if its a valid hit then return true

            foreach(Collider2D hit in hits)
            {
                if(hit.gameObject.tag == "Plataforma_Recarregavel" || hit.gameObject.tag == "Plataforma_NaoRecarregavel" || hit.gameObject.tag == "Plataforma_Quebravel" || hit.gameObject.tag == "Plataforma_Quebravel_Fake" || hit.gameObject.tag == "PlatRec_Curva")
                {

                    return true;
                }

                else
                {
                    return false;
                }
            }
            
        }
        return false;
    }

}