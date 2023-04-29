using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelTank : MonoBehaviour
{
    public float maxFuelLevel;
    private float currentFuelLevel;

    void Start()
    {
        currentFuelLevel = maxFuelLevel;
    }

    public void SetFuelLevel(float value)
    {
        currentFuelLevel = value;
        if (currentFuelLevel > maxFuelLevel)
        {
            currentFuelLevel = maxFuelLevel;
        }
    }

    public float GetFuelLevel()
    {
        return currentFuelLevel;
    }
}
