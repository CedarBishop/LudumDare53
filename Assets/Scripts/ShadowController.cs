using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowController : MonoBehaviour
{
    public Vector3 offset;

    public Transform player;
    
    void Update()
    {
        this.transform.position = player.position - offset;
            
    }
}
