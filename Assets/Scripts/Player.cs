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

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();    
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
        rigidbody.velocity = transform.up * currentSpeed;
        transform.Rotate(0, 0, currentHorizontalValue * turnSpeed * rigidbody.velocity.magnitude * Time.fixedDeltaTime * (isReversing ? 1 : -1));

    }

    void NewDrivingControlsUpdate()
    {
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

        isReversing = currentSpeed < 0;
        rigidbody.velocity = transform.up * currentSpeed;
        transform.Rotate(0, 0, currentHorizontalValue * turnSpeed * rigidbody.velocity.magnitude * Time.fixedDeltaTime * (isReversing ? 1 : -1));

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


}
