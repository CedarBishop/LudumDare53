using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationTracker : MonoBehaviour
{
    private void OnEnable()
    {
        EnablePin();       
    }
    private void OnDisable()
    {
        DisablePin();
    }

    private void EnablePin()
    {
        FindObjectOfType<PinHolder>().AddObjectiveMarker(this);
    }
    private void DisablePin()
    {
        FindObjectOfType<PinHolder>().RemoveObjectiveMarker(this);
    }
}
