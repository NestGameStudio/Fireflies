using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TrajetoriaPredicao : MonoBehaviour
{
    LineRenderer lr;

    public float velocity;
    public float angle;
    public int resolution = 10;

    float g;
    float radianAngle;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();

        g = Mathf.Abs(Physics2D.gravity.y);
    }

    private void OnValidate()
    {
        if(lr != null && Application.isPlaying)
        {
            //RenderArc();
        }
    }

    private void Start()
    {
        //RenderArc();
    }
    public void RenderArc()
    {
        lr.SetVertexCount(resolution+1);
        lr.SetPositions(CalculateArcArray());
    }

    Vector3[] CalculateArcArray()
    {
        Vector3[] arcArray = new Vector3[resolution + 1];

        radianAngle = Mathf.Deg2Rad * angle;
        float maxDistance = (velocity * velocity * Mathf.Sin(2 * radianAngle)) / g;

        for (int i = 0; i <= resolution; i++)
        {
            float t = (float)i / (float)resolution;

            arcArray[i] = CalculateArcPoint(t,maxDistance);
        }

        return arcArray;
    }

    Vector3 CalculateArcPoint(float t, float maxDistance)
    {
        float x = t * maxDistance;
        float y = x * Mathf.Tan(radianAngle) - (( g * x * x) / (2 * velocity * velocity * Mathf.Cos(radianAngle) * Mathf.Cos(radianAngle)));

        return new Vector3(x + gameObject.transform.position.x, y + gameObject.transform.position.y);
    }
}
