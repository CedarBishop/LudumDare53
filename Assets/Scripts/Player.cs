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
    public float maxForwardSpeed;
    public float maxReverseSpeed;
    
    private float currentHorizontalValue;
    private float currentTargetSpeed;

    private Rigidbody2D rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();    
    }

    void FixedUpdate()
    {
        float scaledTargetSpeed = currentTargetSpeed * realWorldToInGameScalar;
        float currentSpeed = Mathf.Lerp(rigidbody.velocity.magnitude, scaledTargetSpeed, accelerationSpeed * Time.fixedDeltaTime);
        rigidbody.velocity = transform.up * currentSpeed;
        transform.Rotate(0,0,currentHorizontalValue * turnSpeed * rigidbody.velocity.magnitude * Time.fixedDeltaTime * -1);
        print("target speed: " + scaledTargetSpeed + ", current speed: " + rigidbody.velocity.magnitude);
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
}
