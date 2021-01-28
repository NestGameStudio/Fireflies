using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoOnPoint : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(gameObject.transform.position, 0.7f);
    }
}
