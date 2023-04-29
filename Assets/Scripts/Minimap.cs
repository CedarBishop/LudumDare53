using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    [SerializeField] Transform player;
    Vector3 newPosition;

    public bool isNorthOriented = true;

    void LateUpdate()
    {
        newPosition = player.position;

        newPosition.z = transform.position.z;

        transform.position = newPosition;

        if (!isNorthOriented)
        {
            transform.rotation = Quaternion.Euler(0, 0, player.eulerAngles.z);
        }
    }
}
