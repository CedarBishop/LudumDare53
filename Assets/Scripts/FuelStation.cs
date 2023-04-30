using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelStation : MonoBehaviour
{
    public float pricePerUnit;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            if (collision.gameObject.GetComponent<Player>().TryRefuel(pricePerUnit))
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
