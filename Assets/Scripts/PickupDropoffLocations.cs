using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupDropoffLocations : MonoBehaviour
{
    public float timeToComplete;
    public float distanceToComplete;
    private float timer;
    private Player player;
    void Start()
    {
        player = FindObjectOfType<Player>();
        bool shouldSetActive = GameManager.instance.currentTrip != null && GameManager.instance.currentTrip.pickupLocation == this;
        gameObject.SetActive(shouldSetActive);
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < distanceToComplete)
        {
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
        else
        {
            timer = 0;
        }
    }
}
