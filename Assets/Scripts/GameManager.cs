using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem.LowLevel;
using static GameManager;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Initialize,
        Play,
        Pause
    }


    public static GameManager instance = null;

    public LevelManager levelManager;

    public Trip currentTrip = null;

    public Action<Trip> AcceptedFareEvent;
    public Action<Trip> StartedFareEvent;
    public Action<Trip> FinishedFareEvent;

    public float startingMoney;
    public float ratingPointLossPerInterval;
    public float ratingPointLessIntervalTime;
    public float ratingPointLossPerCrash;

    private float currentMoneyAmount;
    private float averageRating;
    private int numOfTripsCompleted;

    // Game State
    private GameState gameState = GameState.Initialize;


    #region Getter/Setter
    public GameState myGameState
    { 
        get 
        { 
            return gameState; 
        } 
        set 
        { 
            gameState = value; 
        } 
    }

    #endregion

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

        //SetGameState(GameState.Initialize);
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

    private void SetGameState(GameState gameState)
    {
        if(gameState == GameState.Initialize)
        {
            PauseGame();
            UIManager.instance.SetUIState(UIState.Title);
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void SelectTrip (Trip trip)
    {
        currentTrip = trip;
        currentTrip.pickupLocation.gameObject.SetActive(true);
        AcceptedFareEvent?.Invoke(currentTrip);
        print(currentTrip.expectedTime);
    }

    public void PickedUpPassenger()
    {
        currentTrip.pickupLocation.gameObject.SetActive(false);
        currentTrip.dropoffLocation.gameObject.SetActive(true);
        currentTrip.time = 0;
        currentTrip.startedFare = true;
        SoundManager.instance.PlayShotSound(SoundManager.AudioType.DropOff);
        StartedFareEvent?.Invoke(currentTrip);
    }

    public void DroppedOffPasenger()
    {
        currentTrip.dropoffLocation.gameObject.SetActive(false);
        currentTrip.startedFare = false;

        // calculate rating
        if (currentTrip.time > currentTrip.expectedTime)
        {
            currentTrip.rating -= Mathf.FloorToInt((currentTrip.time - currentTrip.expectedTime) / ratingPointLessIntervalTime) * ratingPointLossPerInterval;
        }        

        // calculate fare of this trip
        float normalizedRating = currentTrip.rating / 5.0f;
        currentTrip.fare *= normalizedRating;
        SetCurrentMoney(currentMoneyAmount + (currentTrip.fare >= 0 ? currentTrip.fare : 0));

        //calculate new average rating
        if (numOfTripsCompleted <= 0)
        {
            SetAverageRating(currentTrip.rating);
        }
        else
        {
            SetAverageRating((currentTrip.rating + (averageRating * numOfTripsCompleted)) / (numOfTripsCompleted + 1));
        }        

        numOfTripsCompleted++;
        SoundManager.instance.PlayShotSound(SoundManager.AudioType.DropOff);
        FinishedFareEvent?.Invoke(currentTrip);
        currentTrip = null;
    }

    public void PlayerCrashedCar()
    {
        if (currentTrip != null)
        {
            if (currentTrip.startedFare)
            {
                currentTrip.rating -= ratingPointLossPerCrash;
            }
        }
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
        fare = GetDistance() * 5;
        rating = 5.0f;
        expectedTime = GetDistance() * 2;        
    }

    public float GetDistance()
    {
        return Vector3.Distance(pickupLocation.transform.position, dropoffLocation.transform.position);
    }


    public PickupDropoffLocations pickupLocation;
    public PickupDropoffLocations dropoffLocation;
    public float rating;
    public float fare;
    public float time;
    public float expectedTime;
    public bool startedFare = false;
    public string passengerName;
}
