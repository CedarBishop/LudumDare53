using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speedIntervals;
    public float realWorldToInGameScalar;
    public float turnSpeed;
    public float accelerationSpeed;
    public float brakeSpeed;
    public float reverseAccelerationSpeed;
    public float maxForwardSpeed;
    public float maxReverseSpeed;

    public bool useNewDrivingControls = false;

    private bool isReversing;
    private bool canReverse;
    
    private float currentHorizontalValue;
    private float currentTargetSpeed;
    private float currentSpeed;

    private float currentSpeedInputValue;

    private Rigidbody2D rigidbody;
    private FuelTank fuelTank;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        fuelTank = GetComponent<FuelTank>();
    }

    void FixedUpdate()
    {
        if (useNewDrivingControls)
        {
            NewDrivingControlsUpdate();
        }
        else
        {
            OldDrivingControlsUpdate();
        }
    }

    void OldDrivingControlsUpdate ()
    {
        float scaledTargetSpeed = currentTargetSpeed * realWorldToInGameScalar;
        currentSpeed += accelerationSpeed * Time.fixedDeltaTime * (scaledTargetSpeed - currentSpeed > 0 ? 1 : 0);
        currentSpeed -= brakeSpeed * Time.fixedDeltaTime * (scaledTargetSpeed - currentSpeed < 0 ? 1 : 0);

        isReversing = currentSpeed < 0;
        if (currentSpeed > maxForwardSpeed)
        {
            currentSpeed = maxForwardSpeed;
        }

        if (currentSpeed < maxReverseSpeed)
        {
            currentSpeed = maxReverseSpeed;
        }

        rigidbody.velocity = transform.up * currentSpeed;
        transform.Rotate(0, 0, currentHorizontalValue * turnSpeed * rigidbody.velocity.magnitude * Time.fixedDeltaTime * (isReversing ? 1 : -1));
        UseFuel();
    }

    void NewDrivingControlsUpdate()
    {
        if (fuelTank.GetFuelLevel() <= 0.0f)
        {
            if (currentSpeed >= 0.0f)
            {
                currentSpeed -= brakeSpeed * Time.fixedDeltaTime;
                rigidbody.velocity = transform.up * currentSpeed;
                transform.Rotate(0, 0, currentHorizontalValue * turnSpeed * rigidbody.velocity.magnitude * Time.fixedDeltaTime * (isReversing ? 1 : -1));
            }
            return;
        }
        if (currentSpeedInputValue > 0.1f)
        {
            currentSpeed += accelerationSpeed * Time.fixedDeltaTime;
        }
        else if (currentSpeedInputValue < -0.1f && currentSpeed >= 0.0f)
        {
            currentSpeed -= brakeSpeed * Time.fixedDeltaTime;
        }
        if (isReversing == false && currentSpeed < 0)
        {
            currentSpeed = 0;
        }

        if (canReverse && currentSpeedInputValue < -0.1 && currentSpeed <= 0.0f)
        {
            currentSpeed -= reverseAccelerationSpeed * Time.fixedDeltaTime;
        }

        if (currentSpeed > maxForwardSpeed * realWorldToInGameScalar)
        {
            currentSpeed = maxForwardSpeed * realWorldToInGameScalar;
        }

        if (currentSpeed < maxReverseSpeed * realWorldToInGameScalar)
        {
            currentSpeed = maxReverseSpeed * realWorldToInGameScalar;
        }

        isReversing = currentSpeed < 0;
        rigidbody.velocity = transform.up * currentSpeed;
        transform.Rotate(0, 0, currentHorizontalValue * turnSpeed * rigidbody.velocity.magnitude * Time.fixedDeltaTime * (isReversing ? 1 : -1));
        UseFuel();
    }

    void UseFuel()
    {
        float fuelUsageAmountThisFrame = fuelTank.fuelUsageScalar * Mathf.Abs(currentSpeed) *Time.fixedDeltaTime;
        fuelTank.SetFuelLevel(fuelTank.GetFuelLevel() - fuelUsageAmountThisFrame);

        if (fuelTank.GetFuelLevel() <= 0.0f)
        {
            // Do empty tank logic
        }
    }

    public void OnIncreaseSpeed()
    {
        currentTargetSpeed += speedIntervals;
        if (currentTargetSpeed > maxForwardSpeed)
        {
            currentTargetSpeed = maxForwardSpeed;
        }
    }

    public void OnDecreaseSpeed()
    {
        currentTargetSpeed -= speedIntervals;
        if (currentTargetSpeed < maxReverseSpeed)
        {
            currentTargetSpeed = maxReverseSpeed;
        }
    }

    public void OnHorizontal(InputValue value)
    {
        currentHorizontalValue = value.Get<float>();
    }

    public void OnSpeedInput(InputValue value)
    {
        currentSpeedInputValue = value.Get<float>();
        if (currentSpeedInputValue > 0 && currentSpeed > 0)
        {
            canReverse = false;
        }
        if (currentSpeedInputValue < 0 && currentSpeed > 0)
        {
            canReverse = false;
        }
        if (currentSpeedInputValue == 0.0f && currentSpeed == 0.0f)
        {
            canReverse = true;
        }
    }

    public void OnTogglePhoneSize()
    {
        FindObjectOfType<PhoneUI>().TogglePhoneSize();
    }

    public float GetCurrentSpeed()
    {
        return currentSpeed / realWorldToInGameScalar;
    }

    public float GetCruiseControlSpeed()
    {
        if (useNewDrivingControls)
        {
            return Mathf.Round(currentSpeed / realWorldToInGameScalar);
        }
        return currentTargetSpeed;
    }

    public bool GetIsReversing()
    {
        return isReversing;
    }

    public float GetMaxForwardSpeed()
    {
        return maxForwardSpeed;
    }

    public float GetMaxReverseSpeed()
    {
        return maxReverseSpeed;
    }

    public bool TryRefuel(float pricePerUnit)
    {
        float currentMoney = GameManager.instance.GetCurrentMoney();
        if (currentMoney <= 0)
        {
            return false;
        }
        //TODO: calculate fuel price and difference and lower from total money
        float priceToFillUpTank = (fuelTank.maxFuelLevel - fuelTank.GetFuelLevel()) * pricePerUnit;
        if (currentMoney >= priceToFillUpTank)
        {
            GameManager.instance.SetCurrentMoney(currentMoney - priceToFillUpTank);
            fuelTank.SetFuelLevel(fuelTank.maxFuelLevel);
        }
        else
        {
            float amountCanFill = currentMoney / pricePerUnit;
            GameManager.instance.SetCurrentMoney(0);
        }
        
        return true;
    }


}
