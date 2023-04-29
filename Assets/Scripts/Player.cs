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
    public float maxForwardSpeed;
    public float maxReverseSpeed;

    private bool isReversing;
    
    private float currentHorizontalValue;
    private float currentTargetSpeed;
    private float currentSpeed;

    private Rigidbody2D rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();    
    }

    void FixedUpdate()
    {
        float scaledTargetSpeed = currentTargetSpeed * realWorldToInGameScalar;
        currentSpeed += accelerationSpeed * Time.fixedDeltaTime * (scaledTargetSpeed - currentSpeed > 0 ? 1 : 0);
        currentSpeed -= brakeSpeed * Time.fixedDeltaTime * (scaledTargetSpeed - currentSpeed < 0 ? 1 : 0);

        isReversing = currentSpeed < 0;
        rigidbody.velocity = transform.up * currentSpeed;
        transform.Rotate(0,0,currentHorizontalValue * turnSpeed * rigidbody.velocity.magnitude * Time.fixedDeltaTime * (isReversing? 1: -1));
        //print("target speed: " + scaledTargetSpeed + ", current speed: " + rigidbody.velocity.magnitude);
        print(GetCurrentSpeed());
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

    public float GetCurrentSpeed()
    {
        return currentSpeed / realWorldToInGameScalar;
    }

    public float GetCruiseControlSpeed()
    {
        return currentTargetSpeed;
    }


}
