using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speedIntervals;
    public float realWorldToInGameScalar;
    public float turnSpeed;
    
    private float currentHorizontalValue;
    private float currentTargetSpeed;

    private Rigidbody2D rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();    
    }

    void FixedUpdate()
    {
        rigidbody.velocity = transform.up * currentTargetSpeed * realWorldToInGameScalar;
        Vector3.Slerp(transform.position, transform.position + (transform.right * currentHorizontalValue), turnSpeed);
    }

    public void OnIncreaseSpeed()
    {
        currentTargetSpeed += speedIntervals;
    }

    public void OnDecreaseSpeed()
    {
        currentTargetSpeed -= speedIntervals;
    }

    public void OnHorizontal(InputValue value)
    {
        currentHorizontalValue = value.Get<float>();
    }
}
