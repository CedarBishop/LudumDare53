using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationTracker : MonoBehaviour
{    void Start()
    {
        FindObjectOfType<PinHolder>().AddObjectiveMarker(this);
    }
}
