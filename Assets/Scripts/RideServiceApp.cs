using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RideServiceApp : MonoBehaviour
{
    public GameObject rideOfferParent;
    public GameObject rideRewardParent;

    public TextMeshProUGUI passengerNameText;
    public TextMeshProUGUI farePriceText;

    public TextMeshProUGUI endPassengerText;
    public TextMeshProUGUI endFareText;
    public TextMeshProUGUI endRatingText;

    private Trip currentTripOption;

    void Start()
    {
        StartCoroutine("CoCreateNewTripChoice");
        GameManager.instance.FinishedFareEvent += OnFinishedFare;
    }

    public void AcceptRide()
    {
        rideOfferParent.SetActive(false);
        GameManager.instance.SelectTrip(currentTripOption);
    }

    public void DeclineRide()
    {
        StartCoroutine("CoCreateNewTripChoice");
    }

    public void DismissReward()
    {
        StartCoroutine("CoCreateNewTripChoice");
    }

    void OnFinishedFare(Trip trip)
    {
        rideRewardParent.SetActive(true);
        endPassengerText.text = "Completed ride for " + trip.passengerName;
        endFareText.text = "$" + trip.fare.ToString("F1");
        endRatingText.text = "5 Stars";
    }

    IEnumerator CoCreateNewTripChoice()
    {
        rideOfferParent.SetActive(false);
        rideRewardParent.SetActive(false);
        yield return new WaitForSeconds(1);
        CreateNewTripChoice();
        rideOfferParent.SetActive(true);
    }

    void CreateNewTripChoice()
    {
        currentTripOption = new Trip(GameManager.instance.levelManager.pickupDropoffLocations);
        passengerNameText.text = currentTripOption.passengerName;
        farePriceText.text = "$" + currentTripOption.fare.ToString("F1");
    }
}