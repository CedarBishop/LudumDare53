using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupDropoffLocations : MonoBehaviour
{
    public float timeToComplete;
    private float timer;
    void Start()
    {
        bool shouldSetActive = GameManager.instance.currentTrip != null && GameManager.instance.currentTrip.pickupLocation == this;
        gameObject.SetActive(shouldSetActive);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        timer = 0;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (GameManager.instance.currentTrip == null)
        {
            return;
        }
        timer += Time.fixedDeltaTime;
        if (timer >= timeToComplete)
        {
            if (GameManager.instance.currentTrip.pickupLocation.gameObject == gameObject)
            {
                GameManager.instance.PickedUpPassenger();
            }
            if (GameManager.instance.currentTrip.dropoffLocation.gameObject == gameObject)
            {
                GameManager.instance.DroppedOffPasenger();
            }
        }
    }
}
