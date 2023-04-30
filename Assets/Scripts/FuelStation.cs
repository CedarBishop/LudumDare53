using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelStation : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponentInParent<Player>())
        {
            if (collision.gameObject.GetComponentInParent<Player>().TryRefuel())
            {
                print("Refuel complete");
                // refuel success logic
            }
            else
            {
                // refuel fail logic
            }

        }
    }
}
