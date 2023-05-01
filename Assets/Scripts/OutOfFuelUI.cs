using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OutOfFuelUI : MonoBehaviour
{
    public TextMeshProUGUI outOfFuelText;
    void Start()
    {
        outOfFuelText.gameObject.SetActive(false);
        Player.OutOfFuelAction += OnOutOfFuel;
    }

    void OnOutOfFuel()
    {
        outOfFuelText.gameObject.SetActive(true);
    }
}
