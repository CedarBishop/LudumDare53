using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDestroyer : MonoBehaviour
{
    public Vector3 gizmoSize;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<AICar>())
        {
            collision.gameObject.GetComponent<AICar>().FadeOnDestroy();
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        gizmoSize = transform.localScale;
        Gizmos.color = Color.green;
        Gizmos.DrawCube(transform.position, gizmoSize);
    }
#endif

}
