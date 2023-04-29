using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public PickupDropoffLocations[] pickupDropoffLocations;

    public Trip currentTrip = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void SelectTrip (Trip trip)
    {
        currentTrip = trip;
    }
}

public class Trip
{
    public Trip (PickupDropoffLocations[] pickupDropoffLocations)
    {
        PickupDropoffLocations potentialPickupLocation = pickupDropoffLocations[Random.Range(0, pickupDropoffLocations.Length)];
        PickupDropoffLocations potentialDropoffLocation = null;
        do
        {
            potentialDropoffLocation = pickupDropoffLocations[Random.Range(0, pickupDropoffLocations.Length)];
        } while (potentialDropoffLocation == potentialPickupLocation);
        pickupLocation = potentialPickupLocation;
        dropoffLocation = potentialDropoffLocation;

        // calculate starting fare
    }
    PickupDropoffLocations pickupLocation;
    PickupDropoffLocations dropoffLocation;
    public float rating;
    public float fare;
    public float time;
}
