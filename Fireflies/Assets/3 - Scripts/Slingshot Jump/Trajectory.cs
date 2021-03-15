using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    LineRenderer lr;
    public float simulateForDuration; //simulate for 5 secs in the furture
    public float simulationStep; //Will add a point every 0.1 secs

    public GameObject Player;

    public float collisionCheckRadius = 0.5f;

    private bool oneCheck = true;

    public GameObject trajectoryHit;

    private bool showHitPoint = false;

    private Vector2 hitpoint;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.startColor = Color.white;
    }

    public void SimulateArc()
    {
        int steps = (int)(simulateForDuration / simulationStep);

        SlingshotController playerSC = Player.GetComponentInChildren<SlingshotController>();
        List<Vector2> lineRendererPoints = new List<Vector2>();
        Vector2 calculatedPosition;
        Vector2 directionVector = playerSC.impulseVector; //You plug you own direction here this is just an example
        Vector2 launchPosition = gameObject.transform.position; //Position where you launch from
        

        float lerpSpeed = playerSC.impulseVector.magnitude.Remap(Setup.Instance.LineMinRadius, Setup.Instance.LineMaxRadius, 5.9f, 4.55f);

        float launchSpeed = playerSC.impulseVector.magnitude * Setup.Instance.ImpulseForce * Setup.Instance.ImpulseForce * lerpSpeed * GetComponent<Rigidbody2D>().mass*5;

        if (playerSC.impulseVector.magnitude > Setup.Instance.LineMinRadius)
        {

            enterArc();

            for (int i = 0; i < steps; ++i)
            {
                calculatedPosition = launchPosition + (directionVector * ((launchSpeed) * i * simulationStep));

                //Calculate gravity
                calculatedPosition.y += Physics2D.gravity.y * (i * simulationStep) * (i * simulationStep);
                lineRendererPoints.Add(calculatedPosition);

                lr.positionCount = steps;
                lr.SetPosition(i, calculatedPosition);

                if (CheckForCollision(lr.GetPosition(i))) //if you hit something
                {
                    deletePositionsAboveX(i, steps);
                    //showHitPoint = true;
                    break;//stop adding positions
                }
                else
                {
                    //showHitPoint = false;
                }

            }

            // A simple 2 color gradient with a fixed alpha of 1.0f.
            //float[] alpha = new float[]{ 1.0f , 0 };
            //float stepsAlpha = lr.positionCount;
//
            //Gradient gradient = new Gradient();
            //gradient.SetKeys(
            //    new GradientColorKey[] { new GradientColorKey(Color.white, 0.0f), new GradientColorKey(Color.white, 1.0f) },
            //    new GradientAlphaKey[] { new GradientAlphaKey(alpha[0], 0), new GradientAlphaKey(stepsAlpha.Remap(playerSC.impulseVector.magnitude, 0, 0.7f, 1f), 1) }
            //    );
            //lr.colorGradient = gradient;

            trajectoryHit.transform.position = Vector2.Lerp(trajectoryHit.transform.position, hitpoint,Time.deltaTime*1000);

            float size = 1 - ( lr.positionCount)/10;

            trajectoryHit.transform.localScale = new Vector3(size,size,size);

            if (showHitPoint)
            {
                trajectoryHit.SetActive(true);
                
            }
            else
            {
                trajectoryHit.SetActive(false);
            }
            
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
            trajectoryHit.SetActive(true);
        }
    }
    public void exitArc()
    {
        if (lr.enabled)
        {
            //lr.positionCount = 0;
            lr.enabled = false;
            trajectoryHit.SetActive(false);
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
            //check if its a wall or something
            //if its a valid hit then return true

            foreach(Collider2D hit in hits)
            {
                if(hit.gameObject.CompareTag("Plataforma_Recarregavel") || hit.gameObject.CompareTag("Plataforma_NaoRecarregavel") 
                || hit.gameObject.CompareTag("Plataforma_Quebravel") || hit.gameObject.CompareTag("Plataforma_Quebravel_Fake") 
                || hit.gameObject.CompareTag("PlatRec_Curva"))
                {
                    showHitPoint = true;
                    hitpoint = position ;
                    return true;
                    
                }

                else
                {
                    showHitPoint = false;
                    return false;
                }
            }
            
        }
        return false;
    }

}