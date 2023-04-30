using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FuelUI : MonoBehaviour
{
    public TextMeshProUGUI fuelText;

    private FuelTank fuelTank;

    private void Start()
    {
        fuelTank = FindObjectOfType<FuelTank>();    
    }

    private void Update()
    {
        fuelText.text = "Fuel: " + fuelTank.GetFuelLevel().ToString("F1") + "/" + fuelTank.maxFuelLevel;
    }
}
