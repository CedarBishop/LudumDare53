using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform followTarget;

    void Update()
    {
        transform.position = new Vector3(followTarget.position.x, followTarget.position.y, transform.position.z);
    }
}
