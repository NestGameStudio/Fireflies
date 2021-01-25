using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAlongVelocity : MonoBehaviour
{
    public float minVelocity = 0.01f;
    public new Rigidbody2D rigidbody;

    private float t = 0;

    private void Update()
    {
        if (rigidbody == null)
            return;

        if (rigidbody.velocity.magnitude < minVelocity)
        {
            //transform.eulerAngles = Vector3.zero;

            transform.rotation = Damp(transform.rotation, Quaternion.Euler(0, 0, 0), 0.1f, t);
            t = Time.deltaTime * 3;
        }
        else
        {

            //var rotation = transform.eulerAngles;

            var angle = Vector2.SignedAngle(Vector2.up, rigidbody.velocity);

            //rotation.z = angle;

            //rotation.z = Damp(transform.rotation.z,angle,0.1f,t);

            Quaternion rotation = transform.rotation;

            //rotation = Damp(transform.rotation,angle ,0.1f,t);

            rotation = Damp(transform.rotation, Quaternion.Euler(0, 0, angle),0.1f, t) ;

            t = Time.deltaTime*3;

            transform.rotation = rotation;
        }
    }
    /*
    public static float Damp(float source, float target, float smoothing, float dt)
    {
        return Mathf.LerpAngle(source, target, 1 - Mathf.Pow(smoothing, dt));
    }
    */
    public static Quaternion Damp(Quaternion source, Quaternion target, float smoothing, float dt)
    {
        return Quaternion.Lerp(source, target, 1 - Mathf.Pow(smoothing, dt));
    }
}