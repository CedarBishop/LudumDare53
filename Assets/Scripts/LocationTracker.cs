using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationTracker : MonoBehaviour
{
    private void Start()
    {
        GameManager.instance.AcceptedFareEvent += OnAcceptedFareEvent;
        GameManager.instance.StartedFareEvent += OnStartedFare;    
        GameManager.instance.FinishedFareEvent += OnFinishedFareEvent;    
    }

    private void OnAcceptedFareEvent(Trip trip)
    {        
        if (trip.pickupLocation.gameObject == gameObject)
        {
            FindObjectOfType<PinHolder>().AddObjectiveMarker(this);
        }        
    }

    private void OnStartedFare(Trip trip)
    {
        FindObjectOfType<PinHolder>().RemoveObjectiveMarker(this);
        if (trip.dropoffLocation.gameObject == gameObject)
        {
            FindObjectOfType<PinHolder>().AddObjectiveMarker(this);
        }        
    }
    
    private void OnFinishedFareEvent(Trip trip)
    {
        FindObjectOfType<PinHolder>().RemoveObjectiveMarker(this);
    }
}
