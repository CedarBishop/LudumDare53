using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public LevelManager levelManager;

    public Trip currentTrip = null;

    public Action<Trip> AcceptedFareEvent;
    public Action<Trip> StartedFareEvent;
    public Action<Trip> FinishedFareEvent;

    public float startingMoney;

    private float currentMoneyAmount;
    private float averageRating;
    private int numOfTripsCompleted;

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

    private void Start()
    {
        SetCurrentMoney(startingMoney);
        SetAverageRating(5.0f);
    }

    private void Update()
    {
        if (currentTrip != null)
        {
            if (currentTrip.startedFare)
            {
                currentTrip.time += Time.deltaTime;
            }
        }
    }

    public void SelectTrip (Trip trip)
    {
        currentTrip = trip;
        currentTrip.pickupLocation.gameObject.SetActive(true);
        AcceptedFareEvent?.Invoke(currentTrip);
    }

    public void PickedUpPassenger()
    {
        currentTrip.pickupLocation.gameObject.SetActive(false);
        currentTrip.dropoffLocation.gameObject.SetActive(true);
        currentTrip.time = 0;
        currentTrip.startedFare = true;
        StartedFareEvent?.Invoke(currentTrip);
    }

    public void DroppedOffPasenger()
    {
        currentTrip.dropoffLocation.gameObject.SetActive(false);
        currentTrip.startedFare = false;
        FinishedFareEvent?.Invoke(currentTrip);
        currentTrip = null;
    }

    public void SetCurrentMoney(float amount)
    {
        currentMoneyAmount = amount;
        // fire event
    }

    public float GetCurrentMoney()
    {
        return currentMoneyAmount;
    }

    public void SetAverageRating(float rating)
    {
        averageRating = rating;
        // fire event
    }

    public float GetAverageRating()
    {
        return averageRating;
    }
}

public class Trip
{
    public Trip (PickupDropoffLocations[] pickupDropoffLocations)
    {
        PickupDropoffLocations potentialPickupLocation = pickupDropoffLocations[UnityEngine.Random.Range(0, pickupDropoffLocations.Length)];
        PickupDropoffLocations potentialDropoffLocation = null;
        do
        {
            potentialDropoffLocation = pickupDropoffLocations[UnityEngine.Random.Range(0, pickupDropoffLocations.Length)];
        } while (potentialDropoffLocation == potentialPickupLocation);
        pickupLocation = potentialPickupLocation;
        dropoffLocation = potentialDropoffLocation;
        passengerName = "John";
        fare = 5;
        // calculate starting fare
    }
    public PickupDropoffLocations pickupLocation;
    public PickupDropoffLocations dropoffLocation;
    public float rating;
    public float fare;
    public float time;
    public bool startedFare = false;
    public string passengerName;
}
