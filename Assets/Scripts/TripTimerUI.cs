using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TripTimerUI : MonoBehaviour
{
    public TextMeshProUGUI timerUI;

    bool tripIsUnderway = false;

    private void Start()
    {
        timerUI.gameObject.SetActive(false);
        GameManager.instance.StartedFareEvent += OnTripStarted;
        GameManager.instance.FinishedFareEvent += OnTripFinished;
    }

    void OnTripStarted(Trip trip)
    {
        timerUI.gameObject.SetActive(true);
        tripIsUnderway = true;
    }

    void OnTripFinished(Trip trip)
    {
        timerUI.gameObject.SetActive(false);
        tripIsUnderway = false;
    }

    private void Update()
    {
        if (tripIsUnderway)
        {
            timerUI.text = "Current Trip Time: " + GameManager.instance.currentTrip.time.ToString("F1");
        }
    }
}
